using Amazon.CDK;
using Amazon.CDK.AWS.APIGateway;
using Amazon.CDK.AWS.IAM;
using Amazon.CDK.AWS.Lambda;
using Amazon.CDK.AWS.S3;
using Constructs;
using System.Collections.Generic;

namespace TestCdk
{
    public class TestLambdaCdkStack : Stack
    {
        internal TestLambdaCdkStack(Construct scope, string id, IStackProps props = null) : base(scope, id, props)
        {
            var bucket = Bucket.FromBucketName(this, "testcdk-bucket", "testcdk-bucket");

            var lambda = new Function(this, "lambda-testcdk-lambda", new FunctionProps()
            {
                Runtime = Runtime.DOTNET_6,
                Handler = "TestCdkLambda::TestCdkLambda.Function::FunctionHandler",
                Timeout = Duration.Seconds(30),
                FunctionName = "testcdklambda",
                Code = Code.FromBucket(bucket, "TestCdkLambda.zip"),
                Role = Role.FromRoleArn(this, id, "arn:aws:iam::000000000000:role/TestCdkLambdaExecutionRole"),
                Tracing = Tracing.ACTIVE,
            });

            //Alias is not support by LocalStack free edition

            //lambda.AddAlias("live", new AliasOptions()
            //{
            //    Description = "This alias is used to map the desired version to api gateway",
            //    ProvisionedConcurrentExecutions = 1, 
            //});
        }
    }
}
