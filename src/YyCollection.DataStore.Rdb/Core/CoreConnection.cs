using Npgsql;

namespace YyCollection.DataStore.Rdb.Core;

public sealed class CoreConnection : YyConnectionBase
{
    #region コンストラクタ
    /// <inheritdoc />
    internal CoreConnection(RdbOptions.ConnectionSetting setting, bool forcePrimary) : base(setting, forcePrimary)
    {
        
    }
    #endregion


    #region override
    /// <inheritdoc />
    protected override NpgsqlConnection CreateConnection(string connectionString)
        => new(connectionString);
    #endregion
}