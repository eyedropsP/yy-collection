using System.Reflection;

namespace YyCollection.Server.Internals.Startup;

// ReSharper disable once InconsistentNaming
/// <summary>
/// <see cref="IConfigurationBuilder"/> の拡張機能を提供します。
/// </summary>
internal static class IConfigurationBuilderExtensions
{
    /// <summary>
    /// 環境変数または UserSecrets から構成情報を読み込むよう構成します
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public static IConfigurationBuilder AddAppConfiguration(this IConfigurationBuilder builder, HostBuilderContext context)
    {
        var env = context.HostingEnvironment;

        if (env.IsDevelopment())
        {
            var appAssembly = Assembly.Load(new AssemblyName(env.ApplicationName));
            builder.AddUserSecrets(appAssembly, optional: true);
        }
        
        builder.AddEnvironmentVariables();

        return builder;
    }
}