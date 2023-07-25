using Amazon.CDK;

namespace DotnetApp
{
    sealed class Program
    {
        public static void Main(string[] args)
        {
            var app = new App();
            new DotnetAppStack(app, "DotnetAppStack");

            app.Synth();
        }
    }
}
