using Amazon.CDK;
using Amazon.CDK.AWS.EC2;
using Amazon.CDK.AWS.IAM;
using Amazon.CDK.AWS.KMS;
using Constructs;

public class MarriottDevWeb2022Stack : Stack
{
    public MarriottDevWeb2022Stack(Construct scope, string id, IStackProps props = null) : base(scope, id, props)
    {
        // VPC configuration remains the same
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

        // KMS Key configuration
        var kmsKey = new Key(this, "KmsKey", new KeyProps
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

        kmsKey.GrantEncryptDecrypt(iamRole);

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
                        KmsKeyId = kmsKey.KeyId
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
                new CfnTag { Key = "Name", Value = "MarriottWeb" },
                new CfnTag { Key = "Environment", Value = "Development" },
                new CfnTag { Key = "Application", Value = "MarriottWeb" }
            }
        });
        instance.ApplyRemovalPolicy(RemovalPolicy.DESTROY);

        // Output the instance ID
        new CfnOutput(this, "InstanceId", new CfnOutputProps
        {
            Value = instance.Ref,
            Description = "Instance ID"
        });
    }
}
