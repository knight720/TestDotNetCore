FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /app
ENTRYPOINT ["dotnet", "watch", "run", "--no-restore"]