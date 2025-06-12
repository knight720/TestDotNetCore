這是一份完整的 **Dockerfile 範例**，使用 **multistage build** 建立一個 **SQL Server 2022 容器**，並自動把 **AdventureWorksLT2022.bak** 備份檔案還原成資料庫。

---

## ✅ 專案結構

```
Database/
│
├── Dockerfile
├── attach-db.sh
└── AdventureWorksLT2022.bak
```

---

## ✅ `Dockerfile` (Multistage Build)

```dockerfile
# Stage 1: 還原資料庫
FROM mcr.microsoft.com/mssql/server:2022-latest AS restore-stage

ENV ACCEPT_EULA=Y
ENV SA_PASSWORD=P@ssw0rd

# 建立必要資料夾
RUN mkdir -p /var/opt/mssql/backup
RUN mkdir -p /var/opt/mssql/data

# 複製備份檔案
COPY AdventureWorksLT2022.bak /var/opt/mssql/backup/

# 啟動 SQL Server 並還原資料庫
RUN (/opt/mssql/bin/sqlservr &) && \
    sleep 30 && \
    echo "Checking logical file names..." && \
    /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P P@ssw0rd -C -Q \
    "RESTORE FILELISTONLY FROM DISK = N'/var/opt/mssql/backup/AdventureWorksLT2022.bak'" && \
    echo "Restoring database..." && \
    /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P P@ssw0rd -C -Q \
    "RESTORE DATABASE AdventureWorksLT2022 \
     FROM DISK = N'/var/opt/mssql/backup/AdventureWorksLT2022.bak' \
     WITH MOVE 'AdventureWorksLT2022_Data' TO '/var/opt/mssql/data/AdventureWorksLT2022.mdf', \
          MOVE 'AdventureWorksLT2022_Log' TO '/var/opt/mssql/data/AdventureWorksLT2022_log.ldf', \
          REPLACE" && \
    /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P P@ssw0rd -C -Q "SHUTDOWN"

# Stage 2: 最終的 SQL Server 容器
FROM mcr.microsoft.com/mssql/server:2022-latest

ENV ACCEPT_EULA=Y
ENV SA_PASSWORD=P@ssw0rd

# 從第一階段複製已還原的資料庫檔案
COPY --from=restore-stage /var/opt/mssql/data/AdventureWorksLT2022.mdf /var/opt/mssql/data/
COPY --from=restore-stage /var/opt/mssql/data/AdventureWorksLT2022_log.ldf /var/opt/mssql/data/

# 複製啟動腳本
COPY --chmod=755 attach-db.sh /usr/src/app/attach-db.sh

# 啟動 SQL Server 並附加資料庫
CMD ["/bin/bash", "/usr/src/app/attach-db.sh"]
```

---

## ✅ `attach-db.sh`

```bash
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
```

---

## ✅ 建立並執行容器

在 Database 資料夾中執行以下指令：

```bash
# 建立映像檔
docker build -t mssql-with-adventureworkslt .

# 執行容器
docker run -d -p 1433:1433 --name mssql-dev mssql-with-adventureworkslt
```

---

## ✅ 連線方式

用 SSMS / Azure Data Studio 連線：

* **Server**: `localhost`
* **Login**: `sa`
* **Password**: `P@ssw0rd`

---

## 🚀 Multistage Build 優點

1. **建置時還原**：第一階段在建置時就完成資料庫還原
2. **快速啟動**：容器啟動時只需附加現有資料庫檔案
3. **映像檔最佳化**：最終映像檔不包含 .bak 檔案，減少儲存空間
4. **一致性**：每次建立容器都使用相同的預建資料庫

---

## ❗ 注意事項

1. **下載 .bak 檔案**：需事先手動下載並放在 Database 目錄
   * 官方下載點：[AdventureWorksLT2022.bak](https://github.com/Microsoft/sql-server-samples/releases/tag/adventureworks)

2. **建置時間**：第一次建置會較久，因為需要還原資料庫

3. **檔案名稱對應**：`MOVE` 裡的檔名要與 .bak 中實際檔名一致，可先用這指令查：
   ```sql
   RESTORE FILELISTONLY FROM DISK = N'/var/opt/mssql/backup/AdventureWorksLT2022.bak'
   ```

4. **密碼安全性**：生產環境請使用更安全的密碼並透過環境變數設定

---

## 🔍 驗證資料庫

容器啟動後，可以執行以下指令驗證資料庫是否成功附加：

```bash
# 進入容器
docker exec -it mssql-dev /bin/bash

# 查看資料庫列表
/opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P P@ssw0rd -C -Q "SELECT name FROM sys.databases"
```

---

如果你要改用 **WideWorldImporters** 或其他範例資料庫，只需要更換 `.bak` 檔案與相關的資料庫名稱。需要我幫你建立其他資料庫的版本嗎？
