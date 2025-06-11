FROM mcr.microsoft.com/mssql/server:2022-latest

ENV ACCEPT_EULA=Y
ENV SA_PASSWORD=P@ssw0rd

# 建立備份資料夾
RUN mkdir -p /var/opt/mssql/backup

# 複製 .bak 檔案與啟動腳本進容器
COPY AdventureWorks2022.bak /var/opt/mssql/backup/
COPY --chmod=755 entrypoint.sh /usr/src/app/entrypoint.sh

# 啟動 SQL Server 並執行還原腳本
CMD ["/bin/bash", "/usr/src/app/entrypoint.sh"]