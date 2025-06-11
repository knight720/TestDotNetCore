這是一份完整的 **Dockerfile 範例**，會建立一個 **SQL Server 2022 容器**，並自動把 **AdventureWorks2022.bak** 備份檔案複製進容器、啟動後還原成資料庫。

---

## ✅ 專案結構（範例）

```
your-project/
│
├── Dockerfile
├── entrypoint.sh
└── AdventureWorks2022.bak
```

---

## ✅ `Dockerfile`

```Dockerfile
FROM mcr.microsoft.com/mssql/server:2022-latest

ENV ACCEPT_EULA=Y
ENV SA_PASSWORD=YourStrong!Passw0rd

# 建立備份資料夾
RUN mkdir -p /var/opt/mssql/backup

# 複製 .bak 檔案與啟動腳本進容器
COPY AdventureWorks2022.bak /var/opt/mssql/backup/
COPY entrypoint.sh /usr/src/app/entrypoint.sh

# 設定腳本執行權限
RUN chmod +x /usr/src/app/entrypoint.sh

# 啟動 SQL Server 並執行還原腳本
CMD ["/bin/bash", "/usr/src/app/entrypoint.sh"]
```

---

## ✅ `entrypoint.sh`

```bash
#!/bin/bash

# 啟動 SQL Server 背景執行
/opt/mssql/bin/sqlservr &

# 等待 SQL Server 啟動完成
echo "Waiting for SQL Server to start..."
sleep 20

# 還原 AdventureWorks2022 資料庫
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P YourStrong!Passw0rd -Q \
"RESTORE DATABASE AdventureWorks2022
FROM DISK = N'/var/opt/mssql/backup/AdventureWorks2022.bak'
WITH MOVE 'AdventureWorks2022' TO '/var/opt/mssql/data/AdventureWorks2022.mdf',
     MOVE 'AdventureWorks2022_log' TO '/var/opt/mssql/data/AdventureWorks2022_log.ldf',
     REPLACE"

# 前景啟動 SQL Server
wait
```

---

## ✅ 建立並執行容器

在專案資料夾中執行以下指令：

```bash
docker build -t mssql-with-adventureworks .
docker run -d -p 1433:1433 --name mssql-dev mssql-with-adventureworks
```

---

## ✅ 連線方式

用 SSMS / Azure Data Studio 連線：

* Server: `localhost`
* Login: `sa`
* Password: `YourStrong!Passw0rd`

---

## ❗ 注意事項

1. `.bak` 檔案需事先手動下載並放在專案目錄。

   * 官方下載點：[AdventureWorks2022.bak](https://github.com/Microsoft/sql-server-samples/releases/tag/adventureworks)
2. `MOVE` 裡的檔名要與 .bak 中實際檔名一致，可先用這指令查：

   ```sql
   RESTORE FILELISTONLY FROM DISK = N'/var/opt/mssql/backup/AdventureWorks2022.bak'
   ```

---

如果你要改用 **WideWorldImporters**，只需要更換 `.bak` 檔案與 `RESTORE` 指令裡的邏輯名稱。需要我幫你改成 WideWorldImporters 的版本嗎？
