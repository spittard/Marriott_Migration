using Amazon.CDK;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MarriottDevWeb2022
{
    sealed class Program
    {
        public static void Main(string[] args)
        {
            var app = new App();
            new MarriottDevWeb2022Stack(app, "Marriott-DevWeb-2022", new StackProps
            {
                // If you don't specify 'env', this stack will be environment-agnostic.
                // Account/Region-dependent features and context lookups will not work,
                // but a single synthesized template can be deployed anywhere.

                // Uncomment the next block to specialize this stack for the AWS Account
                // and Region that are implied by the current CLI configuration.
                /*
                Env = new Amazon.CDK.Environment
                {
                    Account = System.Environment.GetEnvironmentVariable("CDK_DEFAULT_ACCOUNT"),
                    Region = System.Environment.GetEnvironmentVariable("CDK_DEFAULT_REGION"),
                }
                */
            });
            app.Synth();
        }
    }
}