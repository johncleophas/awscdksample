using Amazon.CDK;
using Amazon.CDK.AWS.APIGateway;
using Amazon.CDK.AWS.IAM;
using Amazon.CDK.AWS.Lambda;
using Amazon.CDK.AWS.S3;
using Constructs;
using System.Collections.Generic;

namespace TestCdk
{
    public class TestIAMCdkStack : Stack
    {
        internal TestIAMCdkStack(Construct scope, string id, IStackProps props = null) : base(scope, id, props)
        {
            var lambaExecutionRole = new Role(this, "lambaExecutionRole", new RoleProps()
            {
                RoleName = "TestCdkLambdaExecutionRole",
                AssumedBy = new ServicePrincipal("lambda.amazonaws.com")
            });
        }
    }
}
