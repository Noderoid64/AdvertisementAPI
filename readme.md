### Overview
The project exists for the purpose of code practice.

### Prerequisites
- dotnet 5.0 runtime (https://dotnet.microsoft.com/download/dotnet/5.0)
- Entity Framework Core Cli (https://docs.microsoft.com/en-us/ef/core/cli/dotnet)
- PostgressDb (https://www.postgresql.org/download/)

### Project setup
1. Open cmd in project root directory
2. Edit connection string to your db in appsettings.json
3. Create migrations:<br> ``dotnet ef migration add init``
4. Update your db by application model schema:<br> ``dotnet ef database update``
5. Run app:<br> ``dotnet run``

### Note from author
Please contact me if you have any problems/questions