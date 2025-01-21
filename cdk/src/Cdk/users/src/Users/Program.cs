using Amazon.CDK;

namespace UsersStack
{
    sealed class Program
    {
        public static void Main(string[] args)
        {
            var app = new App();
            new UsersStack(app, "UsersStack", new StackProps());
            app.Synth();
        }
    }
}
