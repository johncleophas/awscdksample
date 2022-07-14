using Amazon.CDK;
using Amazon.CDK.AWS.APIGateway;
using Amazon.CDK.AWS.IAM;
using Amazon.CDK.AWS.Lambda;
using Amazon.CDK.AWS.S3;
using Constructs;
using System.Collections.Generic;

namespace TestCdk
{
    public class TestApiMethodCdkStack : Stack
    {
        internal TestApiMethodCdkStack(Construct scope, string id, IStackProps props = null) : base(scope, id, props)
        {
            var restApiId = "l2ych8ebeh";
            var rootResourceId = "61xvzkvlg9";

            var api = RestApi.FromRestApiAttributes(this, $"testcdk-api", new RestApiAttributes()
            {
                RestApiId = restApiId,
                RootResourceId = rootResourceId
            });

            var corsPreflightOptions = new CorsOptions()
            {
                AllowHeaders = new List<string>() {
                        "Content-Type",
                        "X-Amz-Date",
                        "Authorization",
                        "X-Api-Key",
                        "X-Amz-Security-Token",
                        "Cookie",
                        "Info"
                    }.ToArray(),
                AllowMethods = new List<string>()
                    {
                        "POST",
                        "GET",
                        "OPTIONS"
                    }.ToArray(),
                AllowOrigins = new List<string>() {
                     "*"
                     }.ToArray(),
                StatusCode = 200
            };

            var defaultMethodOptions = new MethodOptions()
            {
                MethodResponses = new List<MethodResponse>() { new MethodResponse()
                    {
                        StatusCode = "200"
                    }}.ToArray()
            };

            var function = Function.FromFunctionArn(this, "testcdk-function", $"arn:aws:lambda:eu-west-1:000000000000:function:testcdklambda");
            var lambdaIntegration = new LambdaIntegration(function);

            var testCdkResource = api.Root.AddResource("testCdk");

            testCdkResource.AddMethod("POST", lambdaIntegration, defaultMethodOptions);

            testCdkResource.AddCorsPreflight(corsPreflightOptions);
        }
    }
}
