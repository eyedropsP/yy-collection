using System;
using Microsoft.EntityFrameworkCore.Migrations;
using YyCollection.Definitions.Enums;

#nullable disable

namespace YyCollection.Batch.OneShot.SettingFiles.Rdb.Migrations
{
    public partial class Initialize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:privacy_status", "private,public");

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<string>(type: "char(26)", nullable: false),
                    Name = table.Column<string>(type: "varchar(128)", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<string>(type: "char(26)", nullable: false),
                    PostId = table.Column<string>(type: "char(26)", nullable: false),
                    UserId = table.Column<string>(type: "char(26)", nullable: false),
                    Comment = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Follows",
                columns: table => new
                {
                    FollowerId = table.Column<string>(type: "char(26)", nullable: false),
                    Followee = table.Column<string>(type: "char(26)", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Follows", x => new { x.FollowerId, x.Followee });
                });

            migrationBuilder.CreateTable(
                name: "Likes",
                columns: table => new
                {
                    PostId = table.Column<string>(type: "char(26)", nullable: false),
                    UserId = table.Column<string>(type: "char(26)", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Likes", x => new { x.PostId, x.UserId });
                });

            migrationBuilder.CreateTable(
                name: "MyListContents",
                columns: table => new
                {
                    MyLIstId = table.Column<string>(type: "char(26)", nullable: false),
                    PostId = table.Column<string>(type: "char(26)", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyListContents", x => new { x.MyLIstId, x.PostId });
                });

            migrationBuilder.CreateTable(
                name: "MyLists",
                columns: table => new
                {
                    Id = table.Column<string>(type: "char(26)", nullable: false),
                    Title = table.Column<string>(type: "varchar(255)", nullable: false),
                    UserId = table.Column<string>(type: "char(26)", nullable: false),
                    PrivacyStatus = table.Column<PrivacyStatus>(type: "privacy_status", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyLists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PostCategoryRelations",
                columns: table => new
                {
                    PostId = table.Column<string>(type: "char(26)", nullable: false),
                    CategoryId = table.Column<string>(type: "char(26)", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostCategoryRelations", x => new { x.CategoryId, x.PostId });
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<string>(type: "char(26)", nullable: false),
                    Title = table.Column<string>(type: "varchar(255)", nullable: false),
                    Overview = table.Column<string>(type: "text", nullable: true),
                    MediaUrl = table.Column<string>(type: "varchar(255)", nullable: false),
                    StartTime = table.Column<int>(type: "integer", nullable: false),
                    EndTime = table.Column<int>(type: "integer", nullable: false),
                    ContributorId = table.Column<string>(type: "char(26)", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PostTagRelations",
                columns: table => new
                {
                    PostId = table.Column<string>(type: "char(26)", nullable: false),
                    TagId = table.Column<string>(type: "char(26)", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostTagRelations", x => new { x.PostId, x.TagId });
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<string>(type: "char(26)", nullable: false),
                    Name = table.Column<string>(type: "varchar(128)", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "char(26)", nullable: false),
                    Name = table.Column<string>(type: "varchar(255)", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Follows");

            migrationBuilder.DropTable(
                name: "Likes");

            migrationBuilder.DropTable(
                name: "MyListContents");

            migrationBuilder.DropTable(
                name: "MyLists");

            migrationBuilder.DropTable(
                name: "PostCategoryRelations");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "PostTagRelations");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
