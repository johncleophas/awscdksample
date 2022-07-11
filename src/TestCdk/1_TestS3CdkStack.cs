using Amazon.CDK;
using Amazon.CDK.AWS.APIGateway;
using Amazon.CDK.AWS.IAM;
using Amazon.CDK.AWS.S3;
using Constructs;
using System.Collections.Generic;

namespace TestCdk
{
    public class TestS3CdkStack : Stack
    {
        internal TestS3CdkStack(Construct scope, string id, IStackProps props = null) : base(scope, id, props)
        {
            var corsRules = new List<CorsRule>();

            corsRules.Add(new CorsRule()
            {
                Id = "AllowHTTP",
                AllowedHeaders = new string[] { "*" },
                AllowedMethods = new HttpMethods[] { HttpMethods.DELETE, HttpMethods.GET, HttpMethods.HEAD, HttpMethods.POST, HttpMethods.PUT },
                AllowedOrigins = new string[] { "*" }
            });

            var websiteBucket = new Bucket(this, $"testcdk-bucket", new BucketProps()
            {
                AccessControl = BucketAccessControl.LOG_DELIVERY_WRITE,
                BlockPublicAccess = new BlockPublicAccess(new BlockPublicAccessOptions() { BlockPublicAcls = false, RestrictPublicBuckets = false, IgnorePublicAcls = false, BlockPublicPolicy = false }),
                BucketName = "testcdk-bucket",
                Cors = corsRules.ToArray(),
                Encryption = BucketEncryption.S3_MANAGED,
                Versioned = false,
                ServerAccessLogsBucket = new Bucket(this, "testcdk-logs-bucket", new BucketProps()
                {
                    AccessControl = BucketAccessControl.LOG_DELIVERY_WRITE,
                    BlockPublicAccess = new BlockPublicAccess(new BlockPublicAccessOptions() { BlockPublicAcls = false, RestrictPublicBuckets = false, IgnorePublicAcls = false, BlockPublicPolicy = false }),
                    BucketName = "testcdk-logs-bucket",
                    Encryption = BucketEncryption.S3_MANAGED,
                    Versioned = false
                })
            });

        }
    }
}
