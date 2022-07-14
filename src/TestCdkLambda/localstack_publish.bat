@echo off
awslocal lambda update-function-code --function-name testcdklambda --s3-bucket testcdk-bucket --s3-key TestCdkLambda.zip --publish --region eu-west-1
echo "lambda code updated"
pause
