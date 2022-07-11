using Amazon.CDK;
using Amazon.CDK.AWS.APIGateway;
using Amazon.CDK.AWS.IAM;
using Constructs;
using System.Collections.Generic;

namespace TestCdk
{
    public class TestApiGatewayCdkStack : Stack
    {
        internal TestApiGatewayCdkStack(Construct scope, string id, IStackProps props = null) : base(scope, id, props)
        {
            var apiName = "testcdk-api";
            var stageName = "dev";

            // The code that defines your stack goes here
            var resourcePolicyStatement1 = new PolicyStatement(new PolicyStatementProps()
            {
                Principals = new List<AnyPrincipal>() { new AnyPrincipal() }.ToArray(),
                Actions = new List<string>() { "execute-api:Invoke" }.ToArray(),
                Effect = Effect.ALLOW,
                Resources = new List<string>() { "execute-api:/*" }.ToArray()
            });

            var restApiProps = new RestApiProps()
            {
                RestApiName = "testcdk-api",
                CloudWatchRole = false,
                Deploy = false,
                DefaultCorsPreflightOptions = new CorsOptions()
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
                },
                Policy = new PolicyDocument(new PolicyDocumentProps()
                {
                    Statements = new List<PolicyStatement>() { resourcePolicyStatement1 }.ToArray()
                })
            };

            var api = new RestApi(this, apiName, restApiProps);

            var deploymentProps = new DeploymentProps()
            {
                Api = api
            };

            var deployment = new Deployment(this, "testcdk", deploymentProps);

            var stageProps = new Amazon.CDK.AWS.APIGateway.StageProps()
            {
                Deployment = deployment,
                StageName = stageName
            };

            var stage = new Amazon.CDK.AWS.APIGateway.Stage(this, "stage-dev", stageProps);
            api.DeploymentStage = stage;
        }
    }
}
