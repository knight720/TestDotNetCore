# EntityFramework Hooks
- Run SQL Server
```
docker run --rm -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=P@ssw0rd' -e 'MSSQL_PID=Express' -p 1433:1433 -d mcr.microsoft.com/mssql/server
```

- Create Database
```
CREATE DATABASE [EFHookDB]
```

- Create Table
```
USE EFHookDB

CREATE TABLE [dbo].[ShopDefault](
	[ShopDefault_Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ShopDefault_ShopId] [bigint] NOT NULL,
	[ShopDefault_GroupTypeDef] [varchar](30) NOT NULL,
	[ShopDefault_Key] [varchar](30) NOT NULL,
	[ShopDefault_Value] [nvarchar](200) NULL,
	[ShopDefault_CreatedDateTime] [datetime] NOT NULL,
	[ShopDefault_CreatedUser] [varchar](50) NOT NULL,
	[ShopDefault_UpdatedTimes] [tinyint] NOT NULL,
	[ShopDefault_UpdatedDateTime] [datetime] NOT NULL,
	[ShopDefault_UpdatedUser] [varchar](50) NOT NULL,
	[ShopDefault_ValidFlag] [bit] NOT NULL,
)
```

- Entity Framework Core 工具參考
```
dotnet tool install --global dotnet-ef
```

- DB First
```
dotnet ef dbcontext scaffold "Data Source=(localdb);Initial Catalog=EFHookDB;User ID=sa;Password=P@ssw0rd;" Microsoft.EntityFrameworkCore.SqlServer
```

- EF Core Power Tools
https://marketplace.visualstudio.com/items?itemName=ErikEJ.EFCorePowerTools

- EFCoreHooks
https://github.com/scottbot95/EFCoreHooks