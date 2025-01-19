using Amazon.CDK;
using Amazon.CDK.AWS.EC2;
using Constructs;
using System.Collections.Generic;

namespace MicroTestStack
{
    public class MicroTestStackProps : StackProps
    {
    }

    public class MicroTestStack : Stack
    {
        public MicroTestStack(Construct scope, string id, MicroTestStackProps props = null) : base(scope, id, props)
        {

            // Resources
            var ec2vpc00vpc84c84de100xG4ez = new CfnVPC(this, "EC2VPC00vpc84c84de100xG4EZ", new CfnVPCProps
            {
                CidrBlock = "10.0.0.0/16",
                EnableDnsSupport = true,
                InstanceTenancy = "default",
                EnableDnsHostnames = false,
                Tags = new []
                {
                    new CfnTag
                    {
                        Value = "Energy-Pathways",
                        Key = "Name",
                    },
                },
            });
            var ec2SecurityGroup00sg064aa0cc1827a0c9800kufx8 = new CfnSecurityGroup(this, "EC2SecurityGroup00sg064aa0cc1827a0c9800kufx8", new CfnSecurityGroupProps
            {
                GroupDescription = "launch-wizard-2 created 2024-09-17T18:43:17.575Z",
                GroupName = "launch-wizard-2",
                VpcId = ec2vpc00vpc84c84de100xG4ez.Ref,
                SecurityGroupIngress = new []
                {
                    new CfnSecurityGroup.IngressProperty
                    {
                        CidrIp = "0.0.0.0/0",
                        IpProtocol = "tcp",
                        FromPort = 3389,
                        ToPort = 3389,
                    },
                },
                SecurityGroupEgress = new []
                {
                    new CfnSecurityGroup.EgressProperty
                    {
                        CidrIp = "0.0.0.0/0",
                        IpProtocol = "-1",
                        FromPort = -1,
                        ToPort = -1,
                    },
                },
            });
            var ec2Subnet00subnet92f52ee50083zeD = new CfnSubnet(this, "EC2Subnet00subnet92f52ee50083zeD", new CfnSubnetProps
            {
                VpcId = ec2vpc00vpc84c84de100xG4ez.Ref,
                MapPublicIpOnLaunch = false,
                EnableDns64 = false,
                AvailabilityZone = "us-east-1b",
                PrivateDnsNameOptionsOnLaunch = new Dictionary<string, object>
                {
                    { "EnableResourceNameDnsARecord", false},
                    { "HostnameType", "ip-name"},
                    { "EnableResourceNameDnsAAAARecord", false},
                },
                CidrBlock = "10.0.0.0/24",
                Ipv6Native = false,
                Tags = new []
                {
                    new CfnTag
                    {
                        Value = "Energy-Pathways",
                        Key = "Name",
                    },
                },
            });
            var ec2NetworkInterface00eni0beda7e3c0dd2fd3900x67En = new CfnNetworkInterface(this, "EC2NetworkInterface00eni0beda7e3c0dd2fd3900x67EN", new CfnNetworkInterfaceProps
            {
                Description = "",
                PrivateIpAddresses = new []
                {
                    new CfnNetworkInterface.PrivateIpAddressSpecificationProperty
                    {
                        PrivateIpAddress = "10.0.0.182",
                        Primary = true,
                    },
                },
                SecondaryPrivateIpAddressCount = 0,
                Ipv6PrefixCount = 0,
                Ipv4Prefixes = new string[] 
                {
                },
                Ipv4PrefixCount = 0,
                GroupSet = new string[]
                {
                    ec2SecurityGroup00sg064aa0cc1827a0c9800kufx8.Ref,
                },
                Ipv6Prefixes = new string[]
                {
                },
                SubnetId = ec2Subnet00subnet92f52ee50083zeD.Ref,
                SourceDestCheck = true,
                InterfaceType = "interface",
                Tags = new ICfnTag[]
                {
                },
            });
            var ec2Instance00i0e449eb71450f565f00sT0qy = new CfnInstance(this, "EC2Instance00i0e449eb71450f565f00sT0qy", new CfnInstanceProps
            {
                Tenancy = "default",
                InstanceInitiatedShutdownBehavior = "stop",
                BlockDeviceMappings = new []
                {
                    new CfnInstance.BlockDeviceMappingProperty
                    {
                        Ebs = new CfnInstance.EbsProperty
                        {
                            SnapshotId = "snap-0012ba513189c41ad",
                            VolumeType = "gp2",
                            VolumeSize = 30,
                            Encrypted = false,
                            DeleteOnTermination = true,
                        },
                        DeviceName = "/dev/sda1",
                    },
                },
                AvailabilityZone = "us-east-1b",
                PrivateDnsNameOptions = new CfnInstance.PrivateDnsNameOptionsProperty
                {
                    EnableResourceNameDnsARecord = false,
                    HostnameType = "ip-name",
                    EnableResourceNameDnsAAAARecord = false,
                },
                EbsOptimized = false,
                DisableApiTermination = true,
                KeyName = "Micro Test",
                SourceDestCheck = true,
                PlacementGroupName = "",
                NetworkInterfaces = new []
                {
                    new CfnInstance.NetworkInterfaceProperty
                    {
                        NetworkInterfaceId = ec2NetworkInterface00eni0beda7e3c0dd2fd3900x67En.Ref,
                        DeviceIndex = "0",
                    },
                },
                ImageId = "ami-0790368b78dc061cb",
                InstanceType = "t2.micro",
                Monitoring = false,
                Tags = new []
                {
                    new CfnTag
                    {
                        Value = "Micro Test",
                        Key = "Name",
                    },
                },
                CreditSpecification = new CfnInstance.CreditSpecificationProperty
                {
                    CpuCredits = "standard",
                },
            });
        }
    }
}
