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
    /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P P@ssw0rd -C -Q "SHUTDOWN" || true

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