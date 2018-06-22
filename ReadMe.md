# AWS Lambda Serverless .NET Core 2.0 Web API Demo

Port an existing .NET Core 2.0 Web API to AWS Lambda Serverless application.

> Note: At this time, the latest version of .NET Core supported by AWS Lambda is 2.0.  
> The package version of `Microsoft.AspNetCore.All` will need to be set to `2.0.5`.

## Prerequisites
1. Install AWS Toolkit for Visual Studio 2017: https://aws.amazon.com/visualstudio.
2. Create or open a .NET Core 2.0 Web API project in Visual Studio 2017.

## Configuration
1. Add Amazon.Lambda.AspNetCoreServer NuGet package
2. Add `LambdaEntryPoint` class that extends `APIGatewayProxyFunction`
    - Implement the `Init` abstract method.

    ```csharp
    protected override void Init(IWebHostBuilder builder)
    {
        builder.UseStartup<Startup>();
    }
    ```
3. Edit the Web API project **.csproj** file to enable AWS tooling.
    > This will enable support for AWS tooling in the .NET Core CLI.
    - Add the following to the `<ItemGroup>` that contains a `DotNetCliToolReference`.

    ```xml
    <DotNetCliToolReference Include="Amazon.Lambda.Tools" Version="2.1.3" />
    ```
    - Save the file, then run `dotnet restore` at the command line.
4. Configuring Amazon API Gateway
    - To expose the Web API via the AWS API Gateway, the app should be deployed as an **AWS Serverless project**.
    - Right-click the project and select Add, AWS Serverless Template.
    - Open the serverless.temmplate file to set the `Handler` property on the `DefaultFunction` resource.

    ```json
    "Handler": "<ASSEMBLY-NAME>::<TYPE-FULL-NAME>::<METHOD-NAME>"
    ```
    - Where assembly is the project name, and full type name includes namespace and class name for `LambdaEntryPoint`.
    - The method name will be `FunctionHandlerAsync`.
    - For example:

    ```json
    "Handler": "AwsNetCoreDemo::AwsNetCoreDemo.LambdaEntryPoint::FunctionHandlerAsync"
    ```

## Deployment
1. Right-click the project and select "Publish to AWS Lambda".
    - Enter a **new value** for the Stack Name under CloudFormation Settings.
    - Select or create a new S3 bucket.
2. Once the Web API has been published to AWS Lambda, you will see a Stack window with a summary of the publication details.
    - Copy the **AWS Serverless URL**
    - Append the controller route for the action you wish to invoke, for example: `api/values`
    - You can test controller actions in a browser or using a client such as PostMan.

