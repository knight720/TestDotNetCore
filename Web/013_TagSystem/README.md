# 啟動
```
docker-compose -f dynamodb.yaml up
```

# DynamoDB
```
# 啟動 DynamoDB
# 跨 Application 共享 DB
docker run --rm -it -d -p 8000:8000 amazon/dynamodb-local -jar DynamoDBLocal.jar  -sharedDb -inMemory
```

# 設定 AWS
```
aws configure
# AWS Access Key ID [None]: KeyId
# AWS Secret Access Key [None]: AccessKey
# Default region name [None]: ap-northeast-1
# Default output format [None]: json
```

# CLI 憑據文件 
```
C:\Users\[USERNAME]\.aws\credentials
```

# 使用 AWS CLI 連線至 DynamoDB
```
aws dynamodb list-tables --endpoint-url http://localhost:8000
```

# Reference
> [Docker image](https://hub.docker.com/r/cnadiminti/dynamodb-local/)  
> [AWS Command Line Interface 組態基礎概念](https://docs.aws.amazon.com/zh_tw/cli/latest/userguide/cli-configure-quickstart.html)  
> [Create a DynamoDB Client](https://docs.aws.amazon.com/amazondynamodb/latest/developerguide/GettingStarted.NET.01.html)  
> [Best Practices for Designing and Architecting with DynamoDB](https://docs.aws.amazon.com/amazondynamodb/latest/developerguide/best-practices.html)  
> [Amazon DynamoDB 文件](https://docs.aws.amazon.com/dynamodb/index.html)  
> [Study Notes - DynamoDB 學習筆記](https://rickhw.github.io/2016/08/17/AWS/Study-Notes-DynamoDB/)  
> [Amazon DynamoDB 筆記](https://blog.gslin.org/archives/2015/01/14/5534/amazon-dynamodb-%E7%AD%86%E8%A8%98/)  

#### Develop
> [Getting Started with .NET and DynamoDB](https://docs.aws.amazon.com/amazondynamodb/latest/developerguide/GettingStarted.NET.html)  

#### Local DynamoDB
- -sharedDb
> [DynamoDB Local Usage Notes](https://docs.aws.amazon.com/amazondynamodb/latest/developerguide/DynamoDBLocal.UsageNotes.html)

#### GUI Tool
> [Download NoSQL Workbench](https://docs.aws.amazon.com/amazondynamodb/latest/developerguide/workbench.settingup.html)