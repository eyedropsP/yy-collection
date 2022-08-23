using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace YyCollection.Batch.OneShot.SettingFiles.Rdb;

internal class DesignTimeMainDbContextFactory : IDesignTimeDbContextFactory<CoreDbContext>
{
    public CoreDbContext CreateDbContext(string[] args)
    {
        var connectionString = getConnectionString();
        ArgumentNullException.ThrowIfNull(connectionString, nameof(connectionString));

        var options = createDbContextOptions(connectionString);
        return new CoreDbContext(options);

        #region ローカル関数

        static string getConnectionString()
        {
            //--- 環境変数読み込み
            var config = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .AddUserSecrets<DesignTimeMainDbContextFactory>(optional: true)
                .Build();

            //--- 本番環境とローカルで取得するものを変更する
            //--- 環境変数から取得
            var connectionString = config.GetValue<string>("DATABASE_URL");
            if (!string.IsNullOrWhiteSpace(connectionString))
                return connectionString;

            //--- 環境変数にないなら UserSecrets から取得
            var host = config.GetValue<string>("HOST");
            var userName = config.GetValue<string>("POSTGRES_USER");
            var db = config.GetValue<string>("POSTGRES_DB");
            var password = config.GetValue<string>("POSTGRES_PASSWORD");
            var port = config.GetValue<int>("PORT");
            var builder = new NpgsqlConnectionStringBuilder
            {
                Host = host,
                Username = userName,
                Database = db,
                Password = password,
                Port = port,
            };

            return builder.ConnectionString;
        }

        
        static DbContextOptions<CoreDbContext> createDbContextOptions(string connectionString)
        {
            var builder = new DbContextOptionsBuilder<CoreDbContext>();
            builder.UseNpgsql(connectionString, static o => { o.CommandTimeout(600); });
            return builder.Options;
        }

        #endregion
    }
}