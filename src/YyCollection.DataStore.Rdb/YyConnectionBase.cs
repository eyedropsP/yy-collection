using Npgsql;
using QLimitive;

namespace YyCollection.DataStore.Rdb;

public abstract class YyConnectionBase : IAsyncDisposable
{
    #region プロパティ
    /// <summary>
    /// データベースの方言を取得します。
    /// </summary>
    internal DbDialect Dialect => DbDialect.PostgreSql;


    /// <summary>
    /// 接続設定を取得します。
    /// </summary>
    private RdbOptions.ConnectionSetting ConnectionString { get; }


    /// <summary>
    /// プライマリ DB へのコネクションを取得します。
    /// </summary>
    public NpgsqlConnection Primary
        => this.GetOrCreateConnection(ref this._primary, this.ConnectionString.Primary);

    private NpgsqlConnection? _primary;


    /// <summary>
    /// セダンダリ DB のコネクションを取得します。
    /// </summary>
    public NpgsqlConnection Secondary
    {
        get
        {
            if (this.ForcePrimary)
                return this.Primary;

            if (this.IsHighAvailable)
                return this.GetOrCreateConnection(ref this._secondary, this.ConnectionString.Secondary);

            return this.Primary;
        }
    }


    private NpgsqlConnection? _secondary;


    /// <summary>
    /// 強制的にプライマリ DB を利用するかどうかを取得します。
    /// </summary>
    public bool ForcePrimary { get; }


    /// <summary>
    /// 高可用構成かどうかを取得します。
    /// </summary>
    private bool IsHighAvailable { get; }


    /// <summary>
    /// 使用したリソースがすでに解放されているかどうかを取得または設定しています。
    /// </summary>
    private bool IsDisposed { get; set; }
    #endregion


    #region コンストラクタ / デストラクタ
    /// <summary>
    /// インスタンスを生成します。
    /// </summary>
    /// <param name="setting"></param>
    /// <param name="forcePrimary"></param>
    private protected YyConnectionBase(RdbOptions.ConnectionSetting setting, bool forcePrimary)
    {
        this.ConnectionString = setting;
        this.ForcePrimary = forcePrimary;
        this.IsHighAvailable = setting.Primary != setting.Secondary;
    }


    /// <summary>
    /// インスタンスを破棄します。
    /// </summary>
    ~YyConnectionBase()
        => this.Dispose(false);
    #endregion


    #region IAsyncDisposable implementations
    /// <inheritdoc />
    public async ValueTask DisposeAsync()
    {
        await this.DisposeAsync(true).ConfigureAwait(false);
        GC.SuppressFinalize(this);
    }


    /// <summary>
    /// 使用したリソースを解放します。
    /// </summary>
    /// <param name="_"></param>
    private async ValueTask DisposeAsync(bool _)
    {
        if (this.IsDisposed)
            return;

        if (this._secondary is not null)
        {
            await this._secondary.DisposeAsync().ConfigureAwait(false);
            this._secondary = null;
        }

        if (this._primary is not null)
        {
            await this._primary.DisposeAsync().ConfigureAwait(false);
            this._primary = null;
        }

        this.IsDisposed = true;
    }


    /// <summary>
    /// 使用したリソースを解放します。
    /// </summary>
    /// <param name="_"></param>
    private void Dispose(bool _)
    {
        if (this.IsDisposed)
            return;

        if (this._secondary is not null)
        {
            this._secondary.Dispose();
            this._secondary = null;
        }

        if (this._primary is not null)
        {
            this._primary.Dispose();
            this._primary = null;
        }

        this.IsDisposed = true;
    }
    #endregion


    #region abstruct / virtual
    /// <summary>
    /// 指定された接続文字列でコネクションを生成します。
    /// </summary>
    /// <param name="connectionString"></param>
    /// <returns></returns>
    protected abstract NpgsqlConnection CreateConnection(string connectionString);
    #endregion


    #region その他
    /// <summary>
    /// データベース接続を取得します。
    /// </summary>
    /// <param name="connection"></param>
    /// <param name="connectionString"></param>
    /// <returns></returns>
    /// <exception cref="ObjectDisposedException"></exception>
    private ref NpgsqlConnection GetOrCreateConnection(ref NpgsqlConnection? connection, string connectionString)
    {
        if (this.IsDisposed)
            throw new ObjectDisposedException(this.GetType().FullName);

        connection ??= this.CreateConnection(connectionString);

        return ref connection!;
    }
    #endregion
}