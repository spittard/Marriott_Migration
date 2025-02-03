using Amazon.CDK;
using Amazon.CDK.AWS.EC2;
using Amazon.CDK.AWS.IAM;
using Amazon.CDK.AWS.KMS;
using Amazon.CDK.AWS.SSM;
using Constructs;

public class MarriottDevWeb2022Stack : Stack
{
    public MarriottDevWeb2022Stack(Construct scope, string id, IStackProps props = null) : base(scope, id, props)
    {
        const string KEY_PAIR_NAME = "marriott-web-key";

        // VPC configuration
        var vpc = new Vpc(this, "Vpc", new VpcProps
        {
            IpAddresses = IpAddresses.Cidr("10.0.0.0/16"),
            EnableDnsHostnames = true,
            EnableDnsSupport = true,
            MaxAzs = 2,
            SubnetConfiguration = new[] {
                new SubnetConfiguration {
                    Name = "Public",
                    SubnetType = SubnetType.PUBLIC,
                    CidrMask = 24
                },
                new SubnetConfiguration {
                    Name = "Private",
                    SubnetType = SubnetType.PRIVATE_WITH_EGRESS,
                    CidrMask = 24
                }
            }
        });
        vpc.ApplyRemovalPolicy(RemovalPolicy.DESTROY);

        // Create a KMS key for the key pair encryption
        var keyPairKmsKey = new Key(this, "KeyPairKmsKey", new KeyProps
        {
            EnableKeyRotation = true,
            Description = "KMS key for EC2 key pair encryption",
            Enabled = true,
            RemovalPolicy = RemovalPolicy.DESTROY,
            PendingWindow = Duration.Days(7)
        });

        // Create the key pair with automatic storage in Parameter Store
        var keyPair = new CfnKeyPair(this, "MarriottWebKeyPair", new CfnKeyPairProps
        {
            KeyName = KEY_PAIR_NAME,
            KeyType = "rsa",
            KeyFormat = "pem",
            Tags = new[] {
                new CfnTag { Key = "Name", Value = "MarriottWebKeyPair" }
            }
        });
        keyPair.ApplyRemovalPolicy(RemovalPolicy.DESTROY);

        // Security Group configuration
        var securityGroup = new SecurityGroup(this, "SecurityGroup", new SecurityGroupProps
        {
            Vpc = vpc,
            Description = "Ritz-Webserver Security Group",
            AllowAllOutbound = true,
            SecurityGroupName = "marriott-web-sg"
        });
        securityGroup.ApplyRemovalPolicy(RemovalPolicy.DESTROY);

        // Security group rules
        securityGroup.AddIngressRule(
            Peer.AnyIpv4(),
            Port.Tcp(3389),
            "Allow RDP from anywhere"
        );
        securityGroup.AddIngressRule(
            Peer.AnyIpv4(), 
            Port.Tcp(80), 
            "Allow HTTP traffic"
        );
        securityGroup.AddIngressRule(
            Peer.AnyIpv4(), 
            Port.Tcp(443), 
            "Allow HTTPS traffic"
        );

        // KMS Key configuration for EBS
        var ebsKmsKey = new Key(this, "EbsKmsKey", new KeyProps
        {
            EnableKeyRotation = true,
            Description = "KMS key for EBS volume encryption",
            Enabled = true,
            RemovalPolicy = RemovalPolicy.DESTROY,
            PendingWindow = Duration.Days(7)
        });

        // IAM Role configuration
        var iamRole = new Role(this, "IamRole", new RoleProps
        {
            AssumedBy = new ServicePrincipal("ec2.amazonaws.com"),
            Description = "EC2 instance role for SessionManager",
            RoleName = "marriott-web-role",
            ManagedPolicies = new[] {
                ManagedPolicy.FromAwsManagedPolicyName("AmazonSSMManagedInstanceCore")
            }
        });
        iamRole.ApplyRemovalPolicy(RemovalPolicy.DESTROY);

        ebsKmsKey.GrantEncryptDecrypt(iamRole);

        // Instance Profile configuration
        var instanceProfile = new CfnInstanceProfile(this, "InstanceProfile", new CfnInstanceProfileProps
        {
            Roles = new[] { iamRole.RoleName }
        });
        instanceProfile.ApplyRemovalPolicy(RemovalPolicy.DESTROY);

        // Windows Server 2022 AMI lookup
        var windowsAmi = MachineImage.LatestWindows(WindowsVersion.WINDOWS_SERVER_2022_ENGLISH_FULL_BASE);

        // Get the first public subnet
        var publicSubnet = vpc.PublicSubnets[0];

        // EC2 Instance configuration
        var instance = new CfnInstance(this, "Instance", new CfnInstanceProps
        {
            ImageId = windowsAmi.GetImage(this).ImageId,
            InstanceType = InstanceType.Of(InstanceClass.T3, InstanceSize.LARGE).ToString(),
            SubnetId = publicSubnet.SubnetId,
            SecurityGroupIds = new[] { securityGroup.SecurityGroupId },
            IamInstanceProfile = instanceProfile.Ref,
            KeyName = keyPair.KeyName,
            BlockDeviceMappings = new[]
            {
                new CfnInstance.BlockDeviceMappingProperty
                {
                    DeviceName = "/dev/sda1",
                    Ebs = new CfnInstance.EbsProperty
                    {
                        VolumeSize = 100,
                        VolumeType = "gp3",
                        Encrypted = true,
                        DeleteOnTermination = true,
                        Iops = 3000
                    }
                },
                new CfnInstance.BlockDeviceMappingProperty
                {
                    DeviceName = "xvdb",
                    Ebs = new CfnInstance.EbsProperty
                    {
                        VolumeSize = 400,
                        VolumeType = "gp3",
                        Encrypted = true,
                        DeleteOnTermination = true,
                        Iops = 3000,
                        KmsKeyId = ebsKmsKey.KeyId
                    }
                },
                new CfnInstance.BlockDeviceMappingProperty
                {
                    DeviceName = "xvdf",
                    Ebs = new CfnInstance.EbsProperty
                    {
                        VolumeSize = 100,
                        VolumeType = "gp3",
                        Encrypted = true,
                        DeleteOnTermination = true,
                        Iops = 3000
                    }
                }
            },
            Tags = new[]
            {
                new CfnTag { Key = "Name", Value = "MarriottDevWeb2022" },
                new CfnTag { Key = "Environment", Value = "Development" },
                new CfnTag { Key = "Application", Value = "MarriottWeb" }
            }
        });
        instance.ApplyRemovalPolicy(RemovalPolicy.DESTROY);

        // Output the instance ID and key pair info
        new CfnOutput(this, "InstanceId", new CfnOutputProps
        {
            Value = instance.Ref,
            Description = "Instance ID"
        });

        new CfnOutput(this, "KeyPairName", new CfnOutputProps
        {
            Value = keyPair.KeyName,
            Description = "Name of the key pair for RDP access"
        });

        new CfnOutput(this, "KeyPairId", new CfnOutputProps
        {
            Value = keyPair.AttrKeyPairId,
            Description = "Key Pair ID for retrieving the private key"
        });

        new CfnOutput(this, "PrivateKeyCommand", new CfnOutputProps
        {
            Value = $"aws ssm get-parameter --name /ec2/keypair/{keyPair.AttrKeyPairId} --with-decryption --query Parameter.Value --output text > {KEY_PAIR_NAME}.pem",
            Description = "Command to retrieve the private key"
        });

        // Note: These outputs use Fn.GetAtt since we're using CfnInstance
        new CfnOutput(this, "PublicIP", new CfnOutputProps
        {
            Value = Token.AsString(instance.GetAtt("PublicIp")),
            Description = "Public IP address of the instance"
        });

        new CfnOutput(this, "PublicDNS", new CfnOutputProps
        {
            Value = Token.AsString(instance.GetAtt("PublicDnsName")),
            Description = "Public DNS name of the instance"
        });
    }
}
