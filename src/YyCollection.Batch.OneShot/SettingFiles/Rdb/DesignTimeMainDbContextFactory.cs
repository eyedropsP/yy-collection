using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace YyCollection.Batch.OneShot.SettingFiles.Rdb;

/// <summary>
/// デザイン時に <see cref="CoreDbContext"/> を生成する機構を提供します。
/// </summary>
internal class DesignTimeMainDbContextFactory : IDesignTimeDbContextFactory<CoreDbContext>
{
    /// <inheritdoc />
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

            //--- 環境変数または UserSecrets から取得
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") != "Production")
                return config.GetValue<string>("RDB_URL_PRIMARY");
            
            var match = Regex.Match(config.GetValue<string>("RDB_URL_PRIMARY")!, @"postgres://(.*):(.*)@(.*):(.*)/(.*)");
            return $"Server={match.Groups[3]};Port={match.Groups[4]};User Id={match.Groups[1]};Password={match.Groups[2]};Database={match.Groups[5]};sslmode=Prefer;Trust Server Certificate=true";

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