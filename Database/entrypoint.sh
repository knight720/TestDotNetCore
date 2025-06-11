#!/bin/bash

# 啟動 SQL Server 背景執行
/opt/mssql/bin/sqlservr &

# 等待 SQL Server 啟動完成
echo "Waiting for SQL Server to start..."
sleep 20

# 還原 AdventureWorks2022 資料庫
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P P@ssw0rd -Q \
"RESTORE DATABASE AdventureWorks2022
FROM DISK = N'/var/opt/mssql/backup/AdventureWorks2022.bak'
WITH MOVE 'AdventureWorks2022' TO '/var/opt/mssql/data/AdventureWorks2022.mdf',
     MOVE 'AdventureWorks2022_log' TO '/var/opt/mssql/data/AdventureWorks2022_log.ldf',
     REPLACE"

# 前景啟動 SQL Server
wait