#!/bin/bash

# 啟動 SQL Server 背景執行
/opt/mssql/bin/sqlservr &

# 等待 SQL Server 啟動完成
echo "Waiting for SQL Server to start..."
sleep 20

# 附加 AdventureWorksLT2022 資料庫
/opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P P@ssw0rd -C -Q \
"CREATE DATABASE AdventureWorksLT2022 
 ON (FILENAME = '/var/opt/mssql/data/AdventureWorksLT2022.mdf'),
    (FILENAME = '/var/opt/mssql/data/AdventureWorksLT2022_log.ldf')
 FOR ATTACH"

echo "AdventureWorksLT2022 database attached successfully!"

# 前景啟動 SQL Server
wait