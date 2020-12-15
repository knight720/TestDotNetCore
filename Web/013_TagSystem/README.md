# How To Start
1. 啟動 Local DynamoDB
```
docker-compose -f dynamodb.yml up
```

2. 操作 NoSQL Workbench 新增 Table
- Commit to Amazon DynamoDB

3. 開啟 TagSystem Application
dotnet TagSystem.dll

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

# Build Image
```
docker build -t tag-system .
```

# TagSystem
```
docker run --rm -it -p 5000:80 -e AWS_ACCESS_KEY_ID=abc -e AWS_SECRET_ACCESS_KEY=def -e ASPNETCORE_ENVIRONMENT=Staging tag-system
```

# Evironment
- Development  
本機開發，連線至 host dynamodb
- Staging  
docker image 測試，連線至 host dynamodb
- Production  
docker compose 的整合測試

# Reference
> [Docker image](https://hub.docker.com/r/cnadiminti/dynamodb-local/)  
> [AWS Command Line Interface 組態基礎概念](https://docs.aws.amazon.com/zh_tw/cli/latest/userguide/cli-configure-quickstart.html)  
> [Create a DynamoDB Client](https://docs.aws.amazon.com/amazondynamodb/latest/developerguide/GettingStarted.NET.01.html)  
> [Best Practices for Designing and Architecting with DynamoDB](https://docs.aws.amazon.com/amazondynamodb/latest/developerguide/best-practices.html)  
> [Amazon DynamoDB 文件](https://docs.aws.amazon.com/dynamodb/index.html)  
> [Study Notes - DynamoDB 學習筆記](https://rickhw.github.io/2016/08/17/AWS/Study-Notes-DynamoDB/)  
> [Amazon DynamoDB 筆記](https://blog.gslin.org/archives/2015/01/14/5534/amazon-dynamodb-%E7%AD%86%E8%A8%98/)  

#### Develop

In the document model, the primary classes are Table and Document. The Table class provides data operation methods such as PutItem, GetItem, and DeleteItem. It also provides the Query and the Scan methods. The Document class represents a single item in a table.

> [.NET: Document Model](https://docs.aws.amazon.com/amazondynamodb/latest/developerguide/DotNetSDKMidLevel.html)
> [Getting Started with .NET and DynamoDB](https://docs.aws.amazon.com/amazondynamodb/latest/developerguide/GettingStarted.NET.html)  
> [Which is the best way to pass AWS credentials to Docker container?](https://stackoverflow.com/questions/36354423/which-is-the-best-way-to-pass-aws-credentials-to-docker-container)  

#### Local DynamoDB
- -sharedDb
> [DynamoDB Local Usage Notes](https://docs.aws.amazon.com/amazondynamodb/latest/developerguide/DynamoDBLocal.UsageNotes.html)

#### GUI Tool
> [Download NoSQL Workbench](https://docs.aws.amazon.com/amazondynamodb/latest/developerguide/workbench.settingup.html)

#### 效能提升到最高且輸送量成本降到最低的建議
> [使用 DynamoDB 進行設計和建構的最佳實務](https://docs.aws.amazon.com/zh_tw/amazondynamodb/latest/developerguide/best-practices.html)