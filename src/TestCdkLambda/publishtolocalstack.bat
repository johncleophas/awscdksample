@echo off
dotnet lambda package
awslocal s3api put-object --bucket testcdk-bucket --key TestCdkLambda.zip --body .\bin\Release\net6.0\TestCdkLambda.zip
pause
