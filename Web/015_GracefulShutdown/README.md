```
docker run --rm -p 5000:80 graceful-shutdown:20210801-1618
```

```
Watch-Command {Invoke-WebRequest http://localhost:5000/WeatherForecast} -Continuous -Second 1
```

```
docker kill --signal=SIGTERM e7d
docker kill --signal=SIGKILL e7d
```