# Read Me

## 新增環境變數

- 查詢
```powershell
# 全部
Get-ChildItem Env:
# 模糊查詢
Get-ChildItem Env:KNIGHT_*
```

- 新增
```powershell
$Env:KNIGHT_FIRSTKEY = "No.1"
```

- 移除
```powershell
Remove-Item Env:KNIGHT_FIRSTKEY
```
> [Powershell](https://hackmd.io/k6L9RJcZTOKKiLNMl91Fnw)