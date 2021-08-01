- 執行 container
```
docker run --rm -p 5000:80 graceful-shutdown:20210801-1618
```

- 呼叫 API
```
Watch-Command {Invoke-WebRequest http://localhost:5000/WeatherForecast} -Continuous -Second 1
```

- 中斷 Application
```
docker kill --signal=SIGTERM e7d
```

- 中斷 Application (container 內)
```
$ docker exec -it 3f2 bash
# pidof dotnet
# kill -s SIGTERM 1
```

## $ docker kill --siginal=SIGTERM
## \# kill -s SIGTERM 1
1. Unloading 維持 3sec
2. 停止 6sec
3. ApplicationStopping 不會結束

## $ docker kill --siginal=SIGKILL  
直接終止

## \# kill -s SIGKILL 1
沒有終止