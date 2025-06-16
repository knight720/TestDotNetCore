## 執行範例資料庫
```
docker run --rm -it -p 1433:1433 mssql-with-adventureworks
```

## 情境
- 建立專案
- 建立和設定模型
  - EF Power Tools 新增 Product 資料表
- 群組組態
  - 新增 Customer 資料表(拆分為配置類別)
- 全域查詢篩選
  - Count Mr. / Ms. / Total
  - 全域篩選 Mr. 
- 簡單記錄
  - LogTo()
- 查詢標籤
  - TagWith()

## 問題排除

- provider: SSL Provider, error: 0 - 此憑證鏈結是由不受信任的授權單位發出的。
```
# 連線字串加上
;TrustServerCertificate=true;
```