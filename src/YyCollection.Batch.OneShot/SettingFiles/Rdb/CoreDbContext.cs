using Microsoft.EntityFrameworkCore;
using Npgsql;
using YyCollection.DataStore.Rdb.Core.Entities.Tables;
using YyCollection.Definitions.Enums;

namespace YyCollection.Batch.OneShot.SettingFiles.Rdb;

/// <summary>
/// YyCollection Database の <see cref="DbContext"/> を提供します。
/// </summary>
internal class CoreDbContext : DbContext
{
    #region プロパティ
    public virtual DbSet<Category> Categories { get; init; }
    public virtual DbSet<Comment> Comments { get; init; }
    public virtual DbSet<Follow> Follows { get; init; }
    public virtual DbSet<Like> Likes { get; init; }
    public virtual DbSet<MyList> MyLists { get; init; }
    public virtual DbSet<MyListContent> MyListContents { get; init; }
    public virtual DbSet<Post> Posts { get; init; }
    public virtual DbSet<PostCategoryRelation> PostCategoryRelations { get; init; }
    public virtual DbSet<PostTagRelation> PostTagRelations { get; init; }
    public virtual DbSet<Tag> Tags { get; init; }
    public virtual DbSet<User> Users { get; init; }
    #endregion


    #region コンストラクタ
#pragma warning disable CS8618
    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="options"></param>
    public CoreDbContext(DbContextOptions<CoreDbContext> options) : base(options)
    {
        NpgsqlConnection.GlobalTypeMapper.MapEnum<PrivacyStatus>();
    }
#pragma warning restore CS8618
    #endregion


    #region Overrides
    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //--- MultipleKey
        modelBuilder.Entity<PostCategoryRelation>().HasKey(static x => new {x.CategoryId, x.PostId});
        modelBuilder.Entity<PostTagRelation>().HasKey(static x => new {x.PostId, x.TagId});
        modelBuilder.Entity<MyListContent>().HasKey(static x => new {x.MyListId, x.PostId});
        modelBuilder.Entity<Like>().HasKey(static x => new {x.PostId, x.UserId});
        modelBuilder.Entity<Follow>().HasKey(static x => new {x.FollowerId, x.FolloweeId});

        //--- enum
        modelBuilder.HasPostgresEnum<PrivacyStatus>();
        base.OnModelCreating(modelBuilder);
    }

    /// <inheritdoc />
    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder
            .Properties<Ulid>()
            .HaveConversion<UlidConverter>();
    }
    #endregion
}