#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
RUN apt-get update
RUN apt-get install awscli --assume-yes
RUN aws configure set aws_access_key_id ABC
RUN aws configure set aws_secret_access_key DEF
RUN aws configure set region ap-northeast-1
RUN aws configure set output json
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["TagSystem.csproj", ""]
RUN dotnet restore "./TagSystem.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "TagSystem.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TagSystem.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TagSystem.dll"]