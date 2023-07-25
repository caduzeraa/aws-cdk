using Amazon.CDK;

namespace Restapi
{
    sealed class Program
    {
        public static void Main(string[] args)
        {

            Amazon.CDK.Environment makeEnv(string account, string region)
            {
                return new Amazon.CDK.Environment
                {
                    Account = account,
                    Region = region
                };
            }

            var envUSA = makeEnv(account: "320097415716", region: "us-east-1");

            var app = new App();
            new RestapiStack(app, "RestapiStack", new StackProps { Env=envUSA });

            app.Synth();
        }
    }
}
