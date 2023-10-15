FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
# RUN dotnet tool install --global dotnet-trace
WORKDIR /app
RUN wget https://aka.ms/dotnet-trace/linux-x64 -O dotnet-trace
RUN chmod +x dotnet-trace

COPY . ./
RUN dotnet restore
RUN dotnet publish -c Release -o out

# ENTRYPOINT ["dotnet", "cs.dll"]
