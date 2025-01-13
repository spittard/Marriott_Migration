using Amazon.CDK;
using Amazon.CDK.AWS.Athena;
using Amazon.CDK.AWS.CloudFront;
using Constructs;
using System.Collections.Generic;

namespace MicroTest2Stack
{
    public class MicroTest2StackProps : StackProps
    {
    }

    public class MicroTest2Stack : Stack
    {
        public MicroTest2Stack(Construct scope, string id, MicroTest2StackProps props = null) : base(scope, id, props)
        {

            // Resources
            var athenaWorkGroup00primary0045ReR = new CfnWorkGroup(this, "AthenaWorkGroup00primary0045ReR", new CfnWorkGroupProps
            {
                WorkGroupConfiguration = new CfnWorkGroup.WorkGroupConfigurationProperty
                {
                    RequesterPaysEnabled = false,
                    EnforceWorkGroupConfiguration = false,
                    EngineVersion = new CfnWorkGroup.EngineVersionProperty
                    {
                        SelectedEngineVersion = "AUTO",
                    },
                    PublishCloudWatchMetricsEnabled = true,
                    ResultConfiguration = new CfnWorkGroup.ResultConfigurationProperty
                    {
                    },
                },
                State = "ENABLED",
                Tags = new []
                {
                },
                Name = "primary",
            });
            var cloudFrontCachePolicy000862726205a94f769dedb50ca2e3a84f00PsbQm = new CfnCachePolicy(this, "CloudFrontCachePolicy000862726205a94f769dedb50ca2e3a84f00PsbQm", new CfnCachePolicyProps
            {
                CachePolicyConfig = new CfnCachePolicy.CachePolicyConfigProperty
                {
                    Comment = "Policy for Elemental MediaPackage Origin",
                    MinTTL = 0,
                    MaxTTL = 31536000,
                    ParametersInCacheKeyAndForwardedToOrigin = new CfnCachePolicy.ParametersInCacheKeyAndForwardedToOriginProperty
                    {
                        QueryStringsConfig = new CfnCachePolicy.QueryStringsConfigProperty
                        {
                            QueryStrings = new []
                            {
                                "aws.manifestfilter",
                                "start",
                                "end",
                                "m",
                            },
                            QueryStringBehavior = "whitelist",
                        },
                        EnableAcceptEncodingBrotli = false,
                        HeadersConfig = new CfnCachePolicy.HeadersConfigProperty
                        {
                            HeaderBehavior = "whitelist",
                            Headers = new []
                            {
                                "origin",
                            },
                        },
                        CookiesConfig = new CfnCachePolicy.CookiesConfigProperty
                        {
                            CookieBehavior = "none",
                        },
                        EnableAcceptEncodingGzip = true,
                    },
                    DefaultTTL = 86400,
                    Name = "Managed-Elemental-MediaPackage",
                },
            });
            var cloudFrontCachePolicy001c6db51aa33f469a8245dae26771f53000MSiuX = new CfnCachePolicy(this, "CloudFrontCachePolicy001c6db51aa33f469a8245dae26771f53000MSiuX", new CfnCachePolicyProps
            {
                CachePolicyConfig = new CfnCachePolicy.CachePolicyConfigProperty
                {
                    Comment = "Amplify cache policy for image optimization",
                    MinTTL = 0,
                    MaxTTL = 31536000,
                    ParametersInCacheKeyAndForwardedToOrigin = new CfnCachePolicy.ParametersInCacheKeyAndForwardedToOriginProperty
                    {
                        QueryStringsConfig = new CfnCachePolicy.QueryStringsConfigProperty
                        {
                            QueryStringBehavior = "all",
                        },
                        EnableAcceptEncodingBrotli = true,
                        HeadersConfig = new CfnCachePolicy.HeadersConfigProperty
                        {
                            HeaderBehavior = "whitelist",
                            Headers = new []
                            {
                                "Authorization",
                                "Accept",
                                "Host",
                            },
                        },
                        CookiesConfig = new CfnCachePolicy.CookiesConfigProperty
                        {
                            CookieBehavior = "none",
                        },
                        EnableAcceptEncodingGzip = true,
                    },
                    DefaultTTL = 0,
                    Name = "Managed-Amplify-ImageOptimization",
                },
            });
            var cloudFrontCachePolicy002e54312d136d493c8eb9b001f22f67d200Gu5ei = new CfnCachePolicy(this, "CloudFrontCachePolicy002e54312d136d493c8eb9b001f22f67d200Gu5ei", new CfnCachePolicyProps
            {
                CachePolicyConfig = new CfnCachePolicy.CachePolicyConfigProperty
                {
                    Comment = "Policy for Amplify Origin",
                    MinTTL = 2,
                    MaxTTL = 600,
                    ParametersInCacheKeyAndForwardedToOrigin = new CfnCachePolicy.ParametersInCacheKeyAndForwardedToOriginProperty
                    {
                        QueryStringsConfig = new CfnCachePolicy.QueryStringsConfigProperty
                        {
                            QueryStringBehavior = "all",
                        },
                        EnableAcceptEncodingBrotli = true,
                        HeadersConfig = new CfnCachePolicy.HeadersConfigProperty
                        {
                            HeaderBehavior = "whitelist",
                            Headers = new []
                            {
                                "Authorization",
                                "CloudFront-Viewer-Country",
                                "Host",
                            },
                        },
                        CookiesConfig = new CfnCachePolicy.CookiesConfigProperty
                        {
                            CookieBehavior = "all",
                        },
                        EnableAcceptEncodingGzip = true,
                    },
                    DefaultTTL = 2,
                    Name = "Managed-Amplify",
                },
            });
            var cloudFrontCachePolicy004135ea2d6df844a39df34b5a84be39ad004Bv6y = new CfnCachePolicy(this, "CloudFrontCachePolicy004135ea2d6df844a39df34b5a84be39ad004BV6y", new CfnCachePolicyProps
            {
                CachePolicyConfig = new CfnCachePolicy.CachePolicyConfigProperty
                {
                    Comment = "Policy with caching disabled",
                    MinTTL = 0,
                    MaxTTL = 0,
                    ParametersInCacheKeyAndForwardedToOrigin = new CfnCachePolicy.ParametersInCacheKeyAndForwardedToOriginProperty
                    {
                        QueryStringsConfig = new CfnCachePolicy.QueryStringsConfigProperty
                        {
                            QueryStringBehavior = "none",
                        },
                        EnableAcceptEncodingBrotli = false,
                        HeadersConfig = new CfnCachePolicy.HeadersConfigProperty
                        {
                            HeaderBehavior = "none",
                        },
                        CookiesConfig = new CfnCachePolicy.CookiesConfigProperty
                        {
                            CookieBehavior = "none",
                        },
                        EnableAcceptEncodingGzip = false,
                    },
                    DefaultTTL = 0,
                    Name = "Managed-CachingDisabled",
                },
            });
            var cloudFrontCachePolicy004cc15a8ad71548a482b8cc0b614638fe00cGp9C = new CfnCachePolicy(this, "CloudFrontCachePolicy004cc15a8ad71548a482b8cc0b614638fe00cGp9C", new CfnCachePolicyProps
            {
                CachePolicyConfig = new CfnCachePolicy.CachePolicyConfigProperty
                {
                    Comment = "Policy for origins that return Cache-Control headers and serve different content based on values present in the query string.",
                    MinTTL = 0,
                    MaxTTL = 31536000,
                    ParametersInCacheKeyAndForwardedToOrigin = new CfnCachePolicy.ParametersInCacheKeyAndForwardedToOriginProperty
                    {
                        QueryStringsConfig = new CfnCachePolicy.QueryStringsConfigProperty
                        {
                            QueryStringBehavior = "all",
                        },
                        EnableAcceptEncodingBrotli = true,
                        HeadersConfig = new CfnCachePolicy.HeadersConfigProperty
                        {
                            HeaderBehavior = "whitelist",
                            Headers = new []
                            {
                                "x-method-override",
                                "origin",
                                "host",
                                "x-http-method",
                                "x-http-method-override",
                            },
                        },
                        CookiesConfig = new CfnCachePolicy.CookiesConfigProperty
                        {
                            CookieBehavior = "all",
                        },
                        EnableAcceptEncodingGzip = true,
                    },
                    DefaultTTL = 0,
                    Name = "UseOriginCacheControlHeaders-QueryStrings",
                },
            });
            var cloudFrontCachePolicy004d1d2f1d3a7149ad9e087ea5d843a55600VEpcO = new CfnCachePolicy(this, "CloudFrontCachePolicy004d1d2f1d3a7149ad9e087ea5d843a55600VEpcO", new CfnCachePolicyProps
            {
                CachePolicyConfig = new CfnCachePolicy.CachePolicyConfigProperty
                {
                    Comment = "Default Amplify cache policy",
                    MinTTL = 0,
                    MaxTTL = 31536000,
                    ParametersInCacheKeyAndForwardedToOrigin = new CfnCachePolicy.ParametersInCacheKeyAndForwardedToOriginProperty
                    {
                        QueryStringsConfig = new CfnCachePolicy.QueryStringsConfigProperty
                        {
                            QueryStringBehavior = "all",
                        },
                        EnableAcceptEncodingBrotli = true,
                        HeadersConfig = new CfnCachePolicy.HeadersConfigProperty
                        {
                            HeaderBehavior = "whitelist",
                            Headers = new []
                            {
                                "Authorization",
                                "Accept",
                                "CloudFront-Viewer-Country",
                                "Host",
                            },
                        },
                        CookiesConfig = new CfnCachePolicy.CookiesConfigProperty
                        {
                            CookieBehavior = "all",
                        },
                        EnableAcceptEncodingGzip = true,
                    },
                    DefaultTTL = 0,
                    Name = "Managed-Amplify-Default",
                },
            });
            var cloudFrontCachePolicy006067e9a8bceb47d4b6ef20492190542600yReDq = new CfnCachePolicy(this, "CloudFrontCachePolicy006067e9a8bceb47d4b6ef20492190542600yReDQ", new CfnCachePolicyProps
            {
                CachePolicyConfig = new CfnCachePolicy.CachePolicyConfigProperty
                {
                    Comment = "Policy for origins that return Cache-Control headers and serve different content based on values present in the query string.",
                    MinTTL = 0,
                    MaxTTL = 31536000,
                    ParametersInCacheKeyAndForwardedToOrigin = new CfnCachePolicy.ParametersInCacheKeyAndForwardedToOriginProperty
                    {
                        QueryStringsConfig = new CfnCachePolicy.QueryStringsConfigProperty
                        {
                            QueryStringBehavior = "all",
                        },
                        EnableAcceptEncodingBrotli = true,
                        HeadersConfig = new CfnCachePolicy.HeadersConfigProperty
                        {
                            HeaderBehavior = "whitelist",
                            Headers = new []
                            {
                                "x-method-override",
                                "origin",
                                "host",
                                "x-http-method",
                                "x-http-method-override",
                            },
                        },
                        CookiesConfig = new CfnCachePolicy.CookiesConfigProperty
                        {
                            CookieBehavior = "all",
                        },
                        EnableAcceptEncodingGzip = true,
                    },
                    DefaultTTL = 0,
                    Name = "UseOriginCacheControlHeaders-QueryStrings",
                },
            });
            var cloudFrontCachePolicy00658327eaf89d4faba63d7e88639e58f600Ih9bg = new CfnCachePolicy(this, "CloudFrontCachePolicy00658327eaf89d4faba63d7e88639e58f600IH9bg", new CfnCachePolicyProps
            {
                CachePolicyConfig = new CfnCachePolicy.CachePolicyConfigProperty
                {
                    Comment = "Policy with caching enabled. Supports Gzip and Brotli compression.",
                    MinTTL = 1,
                    MaxTTL = 31536000,
                    ParametersInCacheKeyAndForwardedToOrigin = new CfnCachePolicy.ParametersInCacheKeyAndForwardedToOriginProperty
                    {
                        QueryStringsConfig = new CfnCachePolicy.QueryStringsConfigProperty
                        {
                            QueryStringBehavior = "none",
                        },
                        EnableAcceptEncodingBrotli = true,
                        HeadersConfig = new CfnCachePolicy.HeadersConfigProperty
                        {
                            HeaderBehavior = "none",
                        },
                        CookiesConfig = new CfnCachePolicy.CookiesConfigProperty
                        {
                            CookieBehavior = "none",
                        },
                        EnableAcceptEncodingGzip = true,
                    },
                    DefaultTTL = 86400,
                    Name = "Managed-CachingOptimized",
                },
            });
            var cloudFrontCachePolicy007e5fad67ee984ad0b05a394999eefc1a00ambmF = new CfnCachePolicy(this, "CloudFrontCachePolicy007e5fad67ee984ad0b05a394999eefc1a00ambmF", new CfnCachePolicyProps
            {
                CachePolicyConfig = new CfnCachePolicy.CachePolicyConfigProperty
                {
                    Comment = "Amplify cache policy for static content",
                    MinTTL = 0,
                    MaxTTL = 31536000,
                    ParametersInCacheKeyAndForwardedToOrigin = new CfnCachePolicy.ParametersInCacheKeyAndForwardedToOriginProperty
                    {
                        QueryStringsConfig = new CfnCachePolicy.QueryStringsConfigProperty
                        {
                            QueryStringBehavior = "none",
                        },
                        EnableAcceptEncodingBrotli = true,
                        HeadersConfig = new CfnCachePolicy.HeadersConfigProperty
                        {
                            HeaderBehavior = "whitelist",
                            Headers = new []
                            {
                                "Authorization",
                                "Host",
                            },
                        },
                        CookiesConfig = new CfnCachePolicy.CookiesConfigProperty
                        {
                            CookieBehavior = "none",
                        },
                        EnableAcceptEncodingGzip = true,
                    },
                    DefaultTTL = 0,
                    Name = "Managed-Amplify-StaticContent",
                },
            });
            var cloudFrontCachePolicy007f4c0d1486b54b4d9fea06a9007a0d6c00Vxs7k = new CfnCachePolicy(this, "CloudFrontCachePolicy007f4c0d1486b54b4d9fea06a9007a0d6c00VXS7K", new CfnCachePolicyProps
            {
                CachePolicyConfig = new CfnCachePolicy.CachePolicyConfigProperty
                {
                    Comment = "Policy for origins that return Cache-Control headers. Query strings are not included in the cache key.",
                    MinTTL = 0,
                    MaxTTL = 31536000,
                    ParametersInCacheKeyAndForwardedToOrigin = new CfnCachePolicy.ParametersInCacheKeyAndForwardedToOriginProperty
                    {
                        QueryStringsConfig = new CfnCachePolicy.QueryStringsConfigProperty
                        {
                            QueryStringBehavior = "none",
                        },
                        EnableAcceptEncodingBrotli = true,
                        HeadersConfig = new CfnCachePolicy.HeadersConfigProperty
                        {
                            HeaderBehavior = "whitelist",
                            Headers = new []
                            {
                                "x-method-override",
                                "origin",
                                "host",
                                "x-http-method",
                                "x-http-method-override",
                            },
                        },
                        CookiesConfig = new CfnCachePolicy.CookiesConfigProperty
                        {
                            CookieBehavior = "all",
                        },
                        EnableAcceptEncodingGzip = true,
                    },
                    DefaultTTL = 0,
                    Name = "UseOriginCacheControlHeaders",
                },
            });
            var cloudFrontCachePolicy0083da9c7e98b44e11a16804f0df8e2c6500FiVno = new CfnCachePolicy(this, "CloudFrontCachePolicy0083da9c7e98b44e11a16804f0df8e2c6500FIVno", new CfnCachePolicyProps
            {
                CachePolicyConfig = new CfnCachePolicy.CachePolicyConfigProperty
                {
                    Comment = "Policy for origins that return Cache-Control headers. Query strings are not included in the cache key.",
                    MinTTL = 0,
                    MaxTTL = 31536000,
                    ParametersInCacheKeyAndForwardedToOrigin = new CfnCachePolicy.ParametersInCacheKeyAndForwardedToOriginProperty
                    {
                        QueryStringsConfig = new CfnCachePolicy.QueryStringsConfigProperty
                        {
                            QueryStringBehavior = "none",
                        },
                        EnableAcceptEncodingBrotli = true,
                        HeadersConfig = new CfnCachePolicy.HeadersConfigProperty
                        {
                            HeaderBehavior = "whitelist",
                            Headers = new []
                            {
                                "x-method-override",
                                "origin",
                                "host",
                                "x-http-method",
                                "x-http-method-override",
                            },
                        },
                        CookiesConfig = new CfnCachePolicy.CookiesConfigProperty
                        {
                            CookieBehavior = "all",
                        },
                        EnableAcceptEncodingGzip = true,
                    },
                    DefaultTTL = 0,
                    Name = "UseOriginCacheControlHeaders",
                },
            });
            var cloudFrontCachePolicy00a6bad94636c34c33aa98362c74a7fb1300D1xEa = new CfnCachePolicy(this, "CloudFrontCachePolicy00a6bad94636c34c33aa98362c74a7fb1300D1xEa", new CfnCachePolicyProps
            {
                CachePolicyConfig = new CfnCachePolicy.CachePolicyConfigProperty
                {
                    Comment = "Default Amplify cache policy without cookies",
                    MinTTL = 0,
                    MaxTTL = 31536000,
                    ParametersInCacheKeyAndForwardedToOrigin = new CfnCachePolicy.ParametersInCacheKeyAndForwardedToOriginProperty
                    {
                        QueryStringsConfig = new CfnCachePolicy.QueryStringsConfigProperty
                        {
                            QueryStringBehavior = "all",
                        },
                        EnableAcceptEncodingBrotli = true,
                        HeadersConfig = new CfnCachePolicy.HeadersConfigProperty
                        {
                            HeaderBehavior = "whitelist",
                            Headers = new []
                            {
                                "Authorization",
                                "Accept",
                                "CloudFront-Viewer-Country",
                                "Host",
                            },
                        },
                        CookiesConfig = new CfnCachePolicy.CookiesConfigProperty
                        {
                            CookieBehavior = "none",
                        },
                        EnableAcceptEncodingGzip = true,
                    },
                    DefaultTTL = 0,
                    Name = "Managed-Amplify-DefaultNoCookies",
                },
            });
            var cloudFrontCachePolicy00b2884449e4de46a7ac3670bc7f1ddd6d00gBi6k = new CfnCachePolicy(this, "CloudFrontCachePolicy00b2884449e4de46a7ac3670bc7f1ddd6d00gBI6K", new CfnCachePolicyProps
            {
                CachePolicyConfig = new CfnCachePolicy.CachePolicyConfigProperty
                {
                    Comment = "Default policy when compression is disabled",
                    MinTTL = 1,
                    MaxTTL = 31536000,
                    ParametersInCacheKeyAndForwardedToOrigin = new CfnCachePolicy.ParametersInCacheKeyAndForwardedToOriginProperty
                    {
                        QueryStringsConfig = new CfnCachePolicy.QueryStringsConfigProperty
                        {
                            QueryStringBehavior = "none",
                        },
                        EnableAcceptEncodingBrotli = false,
                        HeadersConfig = new CfnCachePolicy.HeadersConfigProperty
                        {
                            HeaderBehavior = "none",
                        },
                        CookiesConfig = new CfnCachePolicy.CookiesConfigProperty
                        {
                            CookieBehavior = "none",
                        },
                        EnableAcceptEncodingGzip = false,
                    },
                    DefaultTTL = 86400,
                    Name = "Managed-CachingOptimizedForUncompressedObjects",
                },
            });
            var cloudFrontOriginRequestPolicy0033f36d7ef39646d990e052428a34d9dc00FpgQn = new CfnOriginRequestPolicy(this, "CloudFrontOriginRequestPolicy0033f36d7ef39646d990e052428a34d9dc00FPGQn", new CfnOriginRequestPolicyProps
            {
                OriginRequestPolicyConfig = new CfnOriginRequestPolicy.OriginRequestPolicyConfigProperty
                {
                    QueryStringsConfig = new CfnOriginRequestPolicy.QueryStringsConfigProperty
                    {
                        QueryStringBehavior = "all",
                    },
                    Comment = "Policy to forward all parameters in viewer requests and all CloudFront headers as of June 2022",
                    HeadersConfig = new CfnOriginRequestPolicy.HeadersConfigProperty
                    {
                        HeaderBehavior = "allViewerAndWhitelistCloudFront",
                        Headers = new []
                        {
                            "CloudFront-Viewer-Time-Zone",
                            "CloudFront-Viewer-Address",
                            "CloudFront-Viewer-Country",
                            "CloudFront-Is-IOS-Viewer",
                            "CloudFront-Is-Tablet-Viewer",
                            "CloudFront-Forwarded-Proto",
                            "CloudFront-Viewer-Country-Name",
                            "CloudFront-Is-Mobile-Viewer",
                            "CloudFront-Is-SmartTV-Viewer",
                            "CloudFront-Viewer-Country-Region",
                            "CloudFront-Is-Android-Viewer",
                            "CloudFront-Viewer-Country-Region-Name",
                            "CloudFront-Viewer-City",
                            "CloudFront-Viewer-Latitude",
                            "CloudFront-Viewer-Longitude",
                            "CloudFront-Viewer-Http-Version",
                            "CloudFront-Viewer-Postal-Code",
                            "CloudFront-Viewer-ASN",
                            "CloudFront-Is-Desktop-Viewer",
                            "CloudFront-Viewer-Metro-Code",
                            "CloudFront-Viewer-TLS",
                        },
                    },
                    CookiesConfig = new CfnOriginRequestPolicy.CookiesConfigProperty
                    {
                        CookieBehavior = "all",
                    },
                    Name = "Managed-AllViewerAndCloudFrontHeaders-2022-06",
                },
            });
            var cloudFrontOriginRequestPolicy0059781a5b390341f3afcbaf62929ccde1003BwtF = new CfnOriginRequestPolicy(this, "CloudFrontOriginRequestPolicy0059781a5b390341f3afcbaf62929ccde1003BwtF", new CfnOriginRequestPolicyProps
            {
                OriginRequestPolicyConfig = new CfnOriginRequestPolicy.OriginRequestPolicyConfigProperty
                {
                    QueryStringsConfig = new CfnOriginRequestPolicy.QueryStringsConfigProperty
                    {
                        QueryStringBehavior = "none",
                    },
                    Comment = "Policy for custom origin with CORS",
                    HeadersConfig = new CfnOriginRequestPolicy.HeadersConfigProperty
                    {
                        HeaderBehavior = "whitelist",
                        Headers = new []
                        {
                            "origin",
                        },
                    },
                    CookiesConfig = new CfnOriginRequestPolicy.CookiesConfigProperty
                    {
                        CookieBehavior = "none",
                    },
                    Name = "Managed-CORS-CustomOrigin",
                },
            });
            var cloudFrontOriginRequestPolicy00775133bc15f249f9abeaafb2e0bf67d200Xkn6m = new CfnOriginRequestPolicy(this, "CloudFrontOriginRequestPolicy00775133bc15f249f9abeaafb2e0bf67d200XKN6m", new CfnOriginRequestPolicyProps
            {
                OriginRequestPolicyConfig = new CfnOriginRequestPolicy.OriginRequestPolicyConfigProperty
                {
                    QueryStringsConfig = new CfnOriginRequestPolicy.QueryStringsConfigProperty
                    {
                        QueryStringBehavior = "all",
                    },
                    Comment = "Policy for Elemental MediaTailor Origin",
                    HeadersConfig = new CfnOriginRequestPolicy.HeadersConfigProperty
                    {
                        HeaderBehavior = "whitelist",
                        Headers = new []
                        {
                            "origin",
                            "access-control-request-headers",
                            "x-forwarded-for",
                            "access-control-request-method",
                            "user-agent",
                        },
                    },
                    CookiesConfig = new CfnOriginRequestPolicy.CookiesConfigProperty
                    {
                        CookieBehavior = "none",
                    },
                    Name = "Managed-Elemental-MediaTailor-PersonalizedManifests",
                },
            });
            var cloudFrontOriginRequestPolicy0088a5eaf42fd44709b370b4c650ea3fcf00UjlBf = new CfnOriginRequestPolicy(this, "CloudFrontOriginRequestPolicy0088a5eaf42fd44709b370b4c650ea3fcf00UjlBF", new CfnOriginRequestPolicyProps
            {
                OriginRequestPolicyConfig = new CfnOriginRequestPolicy.OriginRequestPolicyConfigProperty
                {
                    QueryStringsConfig = new CfnOriginRequestPolicy.QueryStringsConfigProperty
                    {
                        QueryStringBehavior = "none",
                    },
                    Comment = "Policy for S3 origin with CORS",
                    HeadersConfig = new CfnOriginRequestPolicy.HeadersConfigProperty
                    {
                        HeaderBehavior = "whitelist",
                        Headers = new []
                        {
                            "origin",
                            "access-control-request-headers",
                            "access-control-request-method",
                        },
                    },
                    CookiesConfig = new CfnOriginRequestPolicy.CookiesConfigProperty
                    {
                        CookieBehavior = "none",
                    },
                    Name = "Managed-CORS-S3Origin",
                },
            });
            var cloudFrontOriginRequestPolicy00acba4595bd2849b8b9fe13317c0390fa00nbtQk = new CfnOriginRequestPolicy(this, "CloudFrontOriginRequestPolicy00acba4595bd2849b8b9fe13317c0390fa00nbtQk", new CfnOriginRequestPolicyProps
            {
                OriginRequestPolicyConfig = new CfnOriginRequestPolicy.OriginRequestPolicyConfigProperty
                {
                    QueryStringsConfig = new CfnOriginRequestPolicy.QueryStringsConfigProperty
                    {
                        QueryStringBehavior = "none",
                    },
                    Comment = "Policy to forward user-agent and referer headers to origin",
                    HeadersConfig = new CfnOriginRequestPolicy.HeadersConfigProperty
                    {
                        HeaderBehavior = "whitelist",
                        Headers = new []
                        {
                            "referer",
                            "user-agent",
                        },
                    },
                    CookiesConfig = new CfnOriginRequestPolicy.CookiesConfigProperty
                    {
                        CookieBehavior = "none",
                    },
                    Name = "Managed-UserAgentRefererHeaders",
                },
            });
            var cloudFrontOriginRequestPolicy00b689b0a853d040abbaf268738e2966ac00bcc0c = new CfnOriginRequestPolicy(this, "CloudFrontOriginRequestPolicy00b689b0a853d040abbaf268738e2966ac00bcc0c", new CfnOriginRequestPolicyProps
            {
                OriginRequestPolicyConfig = new CfnOriginRequestPolicy.OriginRequestPolicyConfigProperty
                {
                    QueryStringsConfig = new CfnOriginRequestPolicy.QueryStringsConfigProperty
                    {
                        QueryStringBehavior = "all",
                    },
                    Comment = "Policy to forward all parameters in viewer requests except for the Host header",
                    HeadersConfig = new CfnOriginRequestPolicy.HeadersConfigProperty
                    {
                        HeaderBehavior = "allExcept",
                        Headers = new []
                        {
                            "host",
                        },
                    },
                    CookiesConfig = new CfnOriginRequestPolicy.CookiesConfigProperty
                    {
                        CookieBehavior = "all",
                    },
                    Name = "Managed-AllViewerExceptHostHeader",
                },
            });
        }
    }
}
