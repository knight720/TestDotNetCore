## 說明
在批次取得 Cache Data 時比較兩者在效能上的差異  
- 使用 Batch 方式呼叫 Task<RedisValue> StringGetAsync(RedisKey key, CommandFlags flags = 0)
- 只用 Array 方式呼叫 Task<RedisValue[]> StringGetAsync(RedisKey[] keys, CommandFlags flags = 0)  


## 前置作業
```
# 執行 Redis Server
docker run --rm -d -p 6379:6379 redis
```

## 測試案例 Keys Count
### Keys Count 10
|                Method |     Mean |   Error |  StdDev | Ratio |
|---------------------- |---------:|--------:|--------:|------:|
|          GetListAsync | 272.4 us | 4.79 us | 4.48 us |  1.00 |
| GetListWithBatchAsync | 264.5 us | 2.25 us | 1.88 us |  0.97 |

### Keys Count 100
|                Method |     Mean |   Error |  StdDev | Ratio | RatioSD |
|---------------------- |---------:|--------:|--------:|------:|--------:|
|          GetListAsync | 301.4 us | 4.50 us | 7.65 us |  1.00 |    0.00 |
| GetListWithBatchAsync | 293.2 us | 4.63 us | 3.86 us |  0.96 |    0.03 |

### Keys Count 1000
|                Method |     Mean |    Error |   StdDev | Ratio | RatioSD |
|---------------------- |---------:|---------:|---------:|------:|--------:|
|          GetListAsync | 601.3 us |  9.34 us |  8.74 us |  1.00 |    0.00 |
| GetListWithBatchAsync | 587.7 us | 11.34 us | 11.14 us |  0.98 |    0.02 |

## 結論
兩者差異不大

## Reference
- Redis
> [StackExchange.Redis](https://www.fuget.org/packages/StackExchange.Redis)  
> [Redis for .NET Developers – Redis Pipeline Batching](https://taswar.zeytinsoft.com/redis-pipeline-batching/)  
> [[食譜好菜] 使用 StackExchange.Redis 對 Redis 執行批次寫入指令](https://dotblogs.com.tw/supershowwei/2018/05/14/180340)

- Benchmark
> [C#: BenchmarkDotnet —— 效能測試好簡單](https://igouist.github.io/post/2021/06/benchmarkdotnet/)  
> [dotnet/BenchmarkDotNet](https://github.com/dotnet/BenchmarkDotNet)