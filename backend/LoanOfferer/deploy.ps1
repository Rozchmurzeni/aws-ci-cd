dotnet restore
dotnet publish -c RELEASE
dotnet lambda deploy-serverless -t template.yaml -cfg deploy-config.json