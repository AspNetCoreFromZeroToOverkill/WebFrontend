FROM mcr.microsoft.com/dotnet/core/sdk:2.2-alpine AS builder
WORKDIR /app

# Copy solution and restore as distinct layers to cache dependencies
COPY ./src/CodingMilitia.PlayBall.WebFrontend.BackForFront.Web/*.csproj ./src/CodingMilitia.PlayBall.WebFrontend.BackForFront.Web/
COPY *.sln ./
RUN dotnet restore

# Publish the application
COPY . ./
WORKDIR /app/src/CodingMilitia.PlayBall.WebFrontend.BackForFront.Web
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-alpine AS runtime
WORKDIR /app
COPY --from=builder /app/src/CodingMilitia.PlayBall.WebFrontend.BackForFront.Web/out .
ENTRYPOINT ["dotnet", "CodingMilitia.PlayBall.WebFrontend.BackForFront.Web.dll"]

# Sample build command
# docker build -t codingmilitia/webfrontend/bff .