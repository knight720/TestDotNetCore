

# DynamoDB
```
docker run --rm -it -p 8000:8000 amazon/dynamodb-local
```

# 設定AWS
```
aws configure
# AWS Access Key ID [None]: KeyId
# AWS Secret Access Key [None]: AccessKey
# Default region name [None]: ap-northeast-1
# Default output format [None]: json
```

# 使用 AWS CLI 連線至 DynamoDB
```
aws dynamodb list-tables --endpoint-url http://localhost:8000
```

> [Docker image](https://hub.docker.com/r/cnadiminti/dynamodb-local/)  
> [AWS Command Line Interface 組態基礎概念](https://docs.aws.amazon.com/zh_tw/cli/latest/userguide/cli-configure-quickstart.html)  
> [AWS](https://docs.aws.amazon.com/amazondynamodb/latest/developerguide/GettingStarted.NET.01.html)  
> [Getting Started with .NET and DynamoDB](https://docs.aws.amazon.com/amazondynamodb/latest/developerguide/GettingStarted.NET.html)  