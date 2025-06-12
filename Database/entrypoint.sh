#!/bin/bash

# 啟動 SQL Server 背景執行
/opt/mssql/bin/sqlservr &

# 等待 SQL Server 啟動完成
echo "Waiting for SQL Server to start..."
sleep 20

# 還原 AdventureWorks2022 資料庫
/opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P P@ssw0rd -C -Q \
"RESTORE DATABASE AdventureWorksLT2022
FROM DISK = N'/var/opt/mssql/backup/AdventureWorksLT2022.bak'
WITH MOVE 'AdventureWorksLT2022_Data' TO '/var/opt/mssql/data/AdventureWorksLT2022.mdf',
     MOVE 'AdventureWorksLT2022_log' TO '/var/opt/mssql/data/AdventureWorksLT2022_log.ldf',
     REPLACE"

# 前景啟動 SQL Server
wait