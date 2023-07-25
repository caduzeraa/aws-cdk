using Amazon.CDK;
using Amazon.CDK.AWS.Lambda;
using Amazon.CDK.AWS.APIGateway;
using Constructs;

namespace DotnetApp
{
    public class DotnetAppStack : Stack
    {
        internal DotnetAppStack(Construct scope, string id, IStackProps props = null) : base(scope, id, props)
        {
            // var hello = new Function(this, "HelloHandler", new FunctionProps{
            //     Runtime = Runtime.NODEJS_14_X,
            //     Code = Code.FromAsset("lambda"),
            //     Handler = "hello.handler"
            // });

            // // defines an API Gateway REST API resource backed by our "hello" function.
            // new LambdaRestApi(this, "Endpoint", new LambdaRestApiProps
            // {
            //     Handler = hello
            // });
        }
    }
}
