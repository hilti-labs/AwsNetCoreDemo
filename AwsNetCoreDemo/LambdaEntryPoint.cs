using Amazon.Lambda.AspNetCoreServer;
using Microsoft.AspNetCore.Hosting;

namespace AwsNetCoreDemo
{
    /// <summary>
    /// This class extends from APIGatewayProxyFunction which contains the method FunctionHandlerAsync which is the 
    /// actual Lambda function entry point. The Lambda handler field should be set to
    /// 
    /// AwsNetCoreDemo::AwsNetCoreDemo.LambdaEntryPoint::FunctionHandlerAsync
    /// </summary>
    public class LambdaEntryPoint : APIGatewayProxyFunction
    {
        protected override void Init(IWebHostBuilder builder)
        {
            builder.UseStartup<Startup>();
        }
    }
}