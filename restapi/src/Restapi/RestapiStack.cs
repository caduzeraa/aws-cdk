using Amazon.CDK;
using Amazon.CDK.AWS.APIGateway;
using Amazon.CDK.AWS.Lambda;
using Constructs;
using Amazon.CDK.AWS.RDS;
using Amazon.CDK.AWS.EC2;
using Amazon.CDK.AWS.SecretsManager;
using System.Collections.Generic;


namespace Restapi
{
    public class RestapiStack : Stack
    {
        internal RestapiStack(Construct scope, string id, IStackProps props = null) : base(scope, id, props)
        {

#region database
            // VPC
            // var VPC = Vpc.FromLookup(this, "VPC", new VpcLookupOptions{
            //     VpcId = "vpc-0b994ad0e161844fb",
            //     Region = "us-east-1"
            // });

            // SERVERLESS DATABASE
            // var cluster = new ServerlessCluster(this, "KingdomHeartsCluster", new ServerlessClusterProps {
            //     Engine = DatabaseClusterEngine.AURORA_POSTGRESQL,
            //     ParameterGroup = ParameterGroup.FromParameterGroupName(this, "ParameterGroup", "default.aurora-postgresql13"),
            //     Scaling = new ServerlessScalingOptions {
            //         AutoPause = Duration.Minutes(5)  // default is to pause after 5 minutes of idle time
            //     },
            //     Vpc = VPC,
            //     VpcSubnets = new SubnetSelection {
            //         SubnetType = SubnetType.PRIVATE_WITH_EGRESS
            //     },
            //     EnableDataApi = true
            // });

            // DATA API SECRET
            // var secret = Secret.FromSecretAttributes(this, "PostgresDataAPISecret", new SecretAttributes {
            //     SecretCompleteArn = cluster.Secret.SecretArn
            // });
#endregion

#region functions
            // BACKEND GET /
            var backendFn = new Function(this, "KHbackendFn", new FunctionProps {
                Runtime = Runtime.NODEJS_18_X,
                Handler = "index.handler",
                Code = Code.FromAsset("lambda")
            });

            // GET /characters
            var getCharactersFn = new Function(this, "GetCharacters", new FunctionProps {
                Runtime = Runtime.NODEJS_18_X,
                Handler = "characters.get",
                Code = Code.FromAsset("lambda")
                // Environment = new Dictionary<string, string> {
                //     { "DB_USER", secret.SecretValueFromJson("username").UnsafeUnwrap().ToString()},
                //     { "DB_SECRET", secret.SecretValueFromJson("password").UnsafeUnwrap().ToString() },
                //     { "DB_DATABASE", "kingdom_hearts_db" },
                //     { "DB_HOST", secret.SecretValueFromJson("host").UnsafeUnwrap().ToString() }
                // }
            });

            // POST /characters
            var postCharactersFn = new Function(this, "PostCharacters", new FunctionProps {
                Runtime = Runtime.NODEJS_18_X,
                Handler = "characters.post",
                Code = Code.FromAsset("lambda")
                // Environment = new Dictionary<string, string> {
                //     { "DB_USER", secret.SecretValueFromJson("username").UnsafeUnwrap().ToString()},
                //     { "DB_SECRET", secret.SecretValueFromJson("password").UnsafeUnwrap().ToString() },
                //     { "DB_DATABASE", "kingdom_hearts_db" },
                //     { "DB_HOST", secret.SecretValueFromJson("host").UnsafeUnwrap().ToString() }
                // }
            });

            // GET /keyblades
            var getKeybladesFn = new Function(this, "GetKeyblade", new FunctionProps {
                Runtime = Runtime.NODEJS_18_X,
                Handler = "keyblades.get",
                Code = Code.FromAsset("lambda")
                // Environment = new Dictionary<string, string> {
                //     { "DB_USER", secret.SecretValueFromJson("username").UnsafeUnwrap().ToString()},
                //     { "DB_SECRET", secret.SecretValueFromJson("password").UnsafeUnwrap().ToString() },
                //     { "DB_DATABASE", "kingdom_hearts_db" },
                //     { "DB_HOST", secret.SecretValueFromJson("host").UnsafeUnwrap().ToString() }
                // }
            });

            // POST /keyblades
            var postKeybladeFn = new Function(this, "PostKeyblade", new FunctionProps {
                Runtime = Runtime.NODEJS_18_X,
                Handler = "keyblades.post",
                Code = Code.FromAsset("lambda")
                // Environment = new Dictionary<string, string> {
                //     { "DB_USER", secret.SecretValueFromJson("username").UnsafeUnwrap().ToString()},
                //     { "DB_SECRET", secret.SecretValueFromJson("password").UnsafeUnwrap().ToString() },
                //     { "DB_DATABASE", "kingdom_hearts_db" },
                //     { "DB_HOST", secret.SecretValueFromJson("host").UnsafeUnwrap().ToString() }
                // }
            });

            // LAMBDA PERMISSIONS TO THE DATABASE
            // cluster.GrantDataApiAccess(getCharactersFn);
            // cluster.GrantDataApiAccess(postCharactersFn);
#endregion
            
#region api
            // API GATEWAY
            var api = new RestApi(this, "KingdomHeartsAPI", new RestApiProps {});
            
            // GET /
            var rootIntegration = new LambdaIntegration(backendFn);
            api.Root.AddMethod("GET", rootIntegration);
#endregion

#region api_endpoints
            // GET /characters/
            var characters = api.Root.AddResource("characters");
            var getCharactersIntegration = new LambdaIntegration(getCharactersFn);
            characters.AddMethod("GET", getCharactersIntegration);

            // POST /characters/
            var postCharacterIntegration = new LambdaIntegration(postCharactersFn);
            characters.AddMethod("POST", postCharacterIntegration);
            
            // GET /characters/{character_name}
            var character = characters.AddResource("{character}");
            var getCharacterIntegration = new LambdaIntegration(getCharactersFn);
            character.AddMethod("GET", getCharacterIntegration);


            // GET /keyblades/
            var keyblades = api.Root.AddResource("keyblades");
            var getKeybladesIntegration = new LambdaIntegration(getKeybladesFn);
            keyblades.AddMethod("GET", getKeybladesIntegration);

            // POST /keyblades/
            var postKeybladeIntegration = new LambdaIntegration(postKeybladeFn);
            keyblades.AddMethod("POST", postKeybladeIntegration);
            
            // GET /characters/{character_name}
            var keyblade = keyblades.AddResource("{keyblade}");
            var getKeybladeIntegration = new LambdaIntegration(getKeybladesFn);
            keyblade.AddMethod("GET", getKeybladeIntegration);
#endregion

        }
    }
}
