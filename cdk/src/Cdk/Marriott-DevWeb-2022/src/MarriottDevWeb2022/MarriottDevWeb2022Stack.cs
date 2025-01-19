using Amazon.CDK;
using Amazon.CDK.AWS.EC2;
using Amazon.CDK.AWS.IAM;
using Amazon.CDK.AWS.KMS;
using Constructs;

public class MarriottDevWeb2022Stack : Stack
{
    public MarriottDevWeb2022Stack(Construct scope, string id, IStackProps props = null) : base(scope, id, props)
    {
        // VPC with cleanup configuration
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

        // Add removal policy to VPC
        vpc.ApplyRemovalPolicy(RemovalPolicy.DESTROY);

        // Security Group with cleanup configuration
        var securityGroup = new SecurityGroup(this, "SecurityGroup", new SecurityGroupProps
        {
            Vpc = vpc,
            Description = "Ritz-Webserver Security Group",
            AllowAllOutbound = false,
            SecurityGroupName = "marriott-web-sg"
        });

        // Apply removal policy to security group
        securityGroup.ApplyRemovalPolicy(RemovalPolicy.DESTROY);

        securityGroup.AddIngressRule(
            Peer.Ipv4("10.0.0.0/8"), 
            Port.Tcp(80), 
            "Allow HTTP traffic from internal network"
        );
        securityGroup.AddIngressRule(
            Peer.Ipv4("10.0.0.0/8"), 
            Port.Tcp(443), 
            "Allow HTTPS traffic from internal network"
        );

        // KMS Key with cleanup configuration
        var kmsKey = new Key(this, "KmsKey", new KeyProps
        {
            EnableKeyRotation = true,
            Description = "KMS key for EBS volume encryption",
            Enabled = true,
            RemovalPolicy = RemovalPolicy.DESTROY,
            PendingWindow = Duration.Days(7)
        });

        // IAM Role with cleanup configuration
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

        // Instance Profile with cleanup configuration
        var instanceProfile = new CfnInstanceProfile(this, "InstanceProfile", new CfnInstanceProfileProps
        {
            Roles = new[] { iamRole.RoleName }
        });
        instanceProfile.ApplyRemovalPolicy(RemovalPolicy.DESTROY);

        // Instance with cleanup configuration
        var instance = new Amazon.CDK.AWS.EC2.CfnInstance(this, "Instance", new Amazon.CDK.AWS.EC2.CfnInstanceProps
        {
            InstanceType = InstanceType.Of(InstanceClass.T3, InstanceSize.LARGE).ToString(),
            ImageId = MachineImage.LatestAmazonLinux2().GetImage(this).ImageId,
            SubnetId = vpc.PrivateSubnets[0].SubnetId,
            SecurityGroupIds = new[] { securityGroup.SecurityGroupId },
            IamInstanceProfile = instanceProfile.Ref,
            BlockDeviceMappings = new[] {
                new Amazon.CDK.AWS.EC2.CfnInstance.BlockDeviceMappingProperty
                {
                    DeviceName = "/dev/sda1",
                    Ebs = new Amazon.CDK.AWS.EC2.CfnInstance.EbsProperty
                    {
                        VolumeSize = 100,
                        VolumeType = "gp3",
                        Encrypted = true,
                        DeleteOnTermination = true,
                        Iops = 3000
                    }
                },
                new Amazon.CDK.AWS.EC2.CfnInstance.BlockDeviceMappingProperty
                {
                    DeviceName = "xvdb",
                    Ebs = new Amazon.CDK.AWS.EC2.CfnInstance.EbsProperty
                    {
                        VolumeSize = 400,
                        VolumeType = "gp3",
                        Iops = 3000,
                        Encrypted = true,
                        KmsKeyId = kmsKey.KeyId,
                        DeleteOnTermination = true
                    }
                },
                new Amazon.CDK.AWS.EC2.CfnInstance.BlockDeviceMappingProperty
                {
                    DeviceName = "xvdf",
                    Ebs = new Amazon.CDK.AWS.EC2.CfnInstance.EbsProperty
                    {
                        VolumeSize = 100,
                        VolumeType = "gp3",
                        Iops = 3000,
                        Encrypted = true,
                        DeleteOnTermination = true
                    }
                }
            }
        });
        instance.ApplyRemovalPolicy(RemovalPolicy.DESTROY);

        // Add tags
        Amazon.CDK.Tags.Of(instance).Add("Environment", "Development");
        Amazon.CDK.Tags.Of(instance).Add("Application", "MarriottWeb");
    }
}
