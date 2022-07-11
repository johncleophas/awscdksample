using Amazon.CDK;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TestCdk
{
    sealed class Program
    {
        public static void Main(string[] args)
        {
            var app = new App();

            var localStackEnv = new Amazon.CDK.Environment
            {
                Account = "000000000000",
                Region = "eu-west-1",
            };

            //1
            //new TestS3CdkStack(app, "TestS3CdkStack", new StackProps
            //{
            //    Env = localStackEnv
            //});

            ////2
            //new TestIAMCdkStack(app, "TestIAMCdkStack", new StackProps
            //{
            //    Env = localStackEnv
            //});

            ////3
            //new TestLambdaCdkStack(app, "TestLambdaCdkStack", new StackProps
            //{
            //    Env = localStackEnv
            //});

            //4
            //new TestApiGatewayCdkStack(app, "TestApiGatewayCdkStack", new StackProps
            //{
            //    Env = localStackEnv
            //});

            //5
            new TestApiMethodCdkStack(app, "TestApiMethodCdkStack", new StackProps
            {
                Env = localStackEnv
            });

            app.Synth();
        }
    }
}
