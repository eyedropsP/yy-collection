using System.Data;
using Dapper;

namespace YyCollection.DataStore.Rdb.Internals;

/// <summary>
/// 追加の <see cref="SqlMapper.TypeHandler{T}"/> を提供します。
/// </summary>
internal static class TypeHandlerProvider
{
    /// <summary>
    /// セットアップします。
    /// </summary>
    public static void Setup()
    {
        //--- DateTimeOffset
        SqlMapper.AddTypeHandler(new DateTimeOffsetHandler());
        SqlMapper.AddTypeHandler(new NullableDateTimeOffsetHandler());
        //--- Version
        SqlMapper.AddTypeHandler(new VersionHandler());
        //--- DateOnly
        SqlMapper.AddTypeHandler(new DateOnlyHandler());
        SqlMapper.AddTypeHandler(new NullableDateOnlyHandler());
        //--- Ulid
        SqlMapper.AddTypeHandler(new UlidHandler());
        SqlMapper.AddTypeHandler(new NullableUlidHandler());
    }
}



/// <summary>
/// <see cref="DateTimeOffset"/> の型ハンドラーを提供します。
/// </summary>
internal sealed class DateTimeOffsetHandler : SqlMapper.TypeHandler<DateTimeOffset>
{
    /// <inheritdoc/>
    public override void SetValue(IDbDataParameter parameter, DateTimeOffset value)
        => parameter.Value = value;


    /// <inheritdoc/>
    public override DateTimeOffset Parse(object value)
    {
        // note:
        //  - MySQL では DateTime で保存される
        //  - UTC とみなして変換する

        return new((DateTime)value, TimeSpan.Zero);
    }
}



/// <summary>
/// <see cref="DateTimeOffset"/>? の型ハンドラーを提供します。
/// </summary>
internal sealed class NullableDateTimeOffsetHandler : SqlMapper.TypeHandler<DateTimeOffset?>
{
    /// <inheritdoc/>
    public override void SetValue(IDbDataParameter parameter, DateTimeOffset? value)
    {
        if (value is not null)
            parameter.Value = value.Value;
    }


    /// <inheritdoc/>
    public override DateTimeOffset? Parse(object value)
    {
        // note:
        //  - MySQL では DateTime で保存される
        //  - UTC とみなして変換する

        return (value is null)
            ? null
            : new((DateTime)value, TimeSpan.Zero);
    }
}



/// <summary>
/// <see cref="Version"/> の型ハンドラーを提供します。
/// </summary>
internal sealed class VersionHandler : SqlMapper.TypeHandler<Version?>
{
    /// <inheritdoc/>
    public override void SetValue(IDbDataParameter parameter, Version? value)
        => parameter.Value = value?.ToString();


    /// <inheritdoc/>
    public override Version? Parse(object value)
    {
        var arg = value as string;
        return Version.TryParse(arg, out var result)
            ? result
            : null;
    }
}



/// <summary>
/// <see cref="DateOnly"/> の型ハンドラーを提供します。
/// </summary>
internal sealed class DateOnlyHandler : SqlMapper.TypeHandler<DateOnly>
{
    /// <inheritdoc/>
    public override void SetValue(IDbDataParameter parameter, DateOnly value)
        => parameter.Value = value.ToDateTime(TimeOnly.MinValue);


    /// <inheritdoc/>
    public override DateOnly Parse(object value)
    {
        // note:
        //  - MySQL では DateTime で保存される

        var x = (DateTime)value;
        return DateOnly.FromDateTime(x);
    }
}



/// <summary>
/// <see cref="DateOnly"/>? の型ハンドラーを提供します。
/// </summary>
internal sealed class NullableDateOnlyHandler : SqlMapper.TypeHandler<DateOnly?>
{
    /// <inheritdoc/>
    public override void SetValue(IDbDataParameter parameter, DateOnly? value)
    {
        if (value is not null)
            parameter.Value = value.Value.ToDateTime(TimeOnly.MinValue);
    }


    /// <inheritdoc/>
    public override DateOnly? Parse(object value)
    {
        // note:
        //  - MySQL では DateTime で保存される

        if (value is null)
            return null;

        var x = (DateTime)value;
        return DateOnly.FromDateTime(x);
    }
}



/// <summary>
/// <see cref="Ulid"/> の型ハンドラーを提供します。
/// </summary>
internal sealed class UlidHandler : SqlMapper.TypeHandler<Ulid>
{
    /// <inheritdoc/>
    public override void SetValue(IDbDataParameter parameter, Ulid value)
    {
        parameter.DbType = DbType.StringFixedLength;
        parameter.Size = 26;
        parameter.Value = value.ToString();
    }


    /// <inheritdoc/>
    public override Ulid Parse(object value)
        => Ulid.Parse((string)value);
}



/// <summary>
/// <see cref="Ulid"/>? の型ハンドラーを提供します。
/// </summary>
internal sealed class NullableUlidHandler : SqlMapper.TypeHandler<Ulid?>
{
    /// <inheritdoc/>
    public override void SetValue(IDbDataParameter parameter, Ulid? value)
    {
        if (value is not null)
        {
            parameter.DbType = DbType.StringFixedLength;
            parameter.Size = 26;
            parameter.Value = value.ToString();
        }
    }


    /// <inheritdoc/>
    public override Ulid? Parse(object value)
    {
        return (value is null)
            ? null
            : Ulid.Parse((string)value);
    }
}