using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MiniBlog.Data.Migrations
{
    public partial class RoleMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogPostBase_AspNetUsers_CreatedById",
                table: "BlogPostBase");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_BlogPostBase_OwnerPostId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_PersistedGrants_SubjectId_SessionId_Type",
                table: "PersistedGrants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BlogPostBase",
                table: "BlogPostBase");

            migrationBuilder.DropColumn(
                name: "ConsumedTime",
                table: "PersistedGrants");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "PersistedGrants");

            migrationBuilder.DropColumn(
                name: "SessionId",
                table: "PersistedGrants");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "DeviceCodes");

            migrationBuilder.DropColumn(
                name: "SessionId",
                table: "DeviceCodes");

            migrationBuilder.RenameTable(
                name: "BlogPostBase",
                newName: "BlogPosts");

            migrationBuilder.RenameIndex(
                name: "IX_BlogPostBase_CreatedById",
                table: "BlogPosts",
                newName: "IX_BlogPosts_CreatedById");

            migrationBuilder.AddColumn<Guid>(
                name: "ProfilePictureId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "BackgroundImageId",
                table: "BlogPosts",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BlogPosts",
                table: "BlogPosts",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ImageName = table.Column<string>(nullable: true),
                    ImagePath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ProfilePictureId",
                table: "AspNetUsers",
                column: "ProfilePictureId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogPosts_BackgroundImageId",
                table: "BlogPosts",
                column: "BackgroundImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Images_ProfilePictureId",
                table: "AspNetUsers",
                column: "ProfilePictureId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPosts_Images_BackgroundImageId",
                table: "BlogPosts",
                column: "BackgroundImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPosts_AspNetUsers_CreatedById",
                table: "BlogPosts",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_BlogPosts_OwnerPostId",
                table: "Comments",
                column: "OwnerPostId",
                principalTable: "BlogPosts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Images_ProfilePictureId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_BlogPosts_Images_BackgroundImageId",
                table: "BlogPosts");

            migrationBuilder.DropForeignKey(
                name: "FK_BlogPosts_AspNetUsers_CreatedById",
                table: "BlogPosts");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_BlogPosts_OwnerPostId",
                table: "Comments");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ProfilePictureId",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BlogPosts",
                table: "BlogPosts");

            migrationBuilder.DropIndex(
                name: "IX_BlogPosts_BackgroundImageId",
                table: "BlogPosts");

            migrationBuilder.DropColumn(
                name: "ProfilePictureId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "BackgroundImageId",
                table: "BlogPosts");

            migrationBuilder.RenameTable(
                name: "BlogPosts",
                newName: "BlogPostBase");

            migrationBuilder.RenameIndex(
                name: "IX_BlogPosts_CreatedById",
                table: "BlogPostBase",
                newName: "IX_BlogPostBase_CreatedById");

            migrationBuilder.AddColumn<DateTime>(
                name: "ConsumedTime",
                table: "PersistedGrants",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "PersistedGrants",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SessionId",
                table: "PersistedGrants",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "DeviceCodes",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SessionId",
                table: "DeviceCodes",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BlogPostBase",
                table: "BlogPostBase",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_PersistedGrants_SubjectId_SessionId_Type",
                table: "PersistedGrants",
                columns: new[] { "SubjectId", "SessionId", "Type" });

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPostBase_AspNetUsers_CreatedById",
                table: "BlogPostBase",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_BlogPostBase_OwnerPostId",
                table: "Comments",
                column: "OwnerPostId",
                principalTable: "BlogPostBase",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
