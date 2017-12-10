namespace StudyForum.Db.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fluentapiupdate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Messages", "AuthorId", "dbo.Users");
            DropForeignKey("dbo.UserIdentities", "Id", "dbo.Users");
            DropForeignKey("dbo.UserIdentities", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.ThemeFiles", "ThemeId", "dbo.Themes");
            DropForeignKey("dbo.Themes", "AuthorId", "dbo.Users");
            DropForeignKey("dbo.Messages", "Theme_Id", "dbo.Themes");
            DropIndex("dbo.Messages", new[] { "Theme_Id" });
            DropIndex("dbo.Users", new[] { "AvatarId" });
            DropIndex("dbo.Subjects", new[] { "GroupSemesterId" });
            DropIndex("dbo.UserIdentities", "Index_UserIdentityEmail");
            DropIndex("dbo.Roles", "Index_RoleName");
            RenameColumn(table: "dbo.Messages", name: "Theme_Id", newName: "ThemeId");
            CreateTable(
                "dbo.GroupSemesterSubjects",
                c => new
                    {
                        SubjectId = c.Guid(nullable: false),
                        GroupSemesterId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.SubjectId, t.GroupSemesterId })
                .ForeignKey("dbo.Subjects", t => t.SubjectId, cascadeDelete: true)
                .Index(t => t.SubjectId)
                .Index(t => t.GroupSemesterId);
            
            AddColumn("dbo.Files", "AvatarId", c => c.Guid());
            AlterColumn("dbo.Messages", "ThemeId", c => c.Guid(nullable: false));
            AlterColumn("dbo.Semesters", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Subjects", "Name", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Themes", "Title", c => c.String(nullable: false, maxLength: 100));
            CreateIndex("dbo.Files", "AvatarId");
            CreateIndex("dbo.Messages", "ThemeId");
            CreateIndex("dbo.UserIdentities", "Email", unique: true, name: "UserIdentityEmail_Index");
            CreateIndex("dbo.Roles", "Name", unique: true, name: "RoleName_Index");
            AddForeignKey("dbo.Messages", "AuthorId", "dbo.Users", "Id");
            AddForeignKey("dbo.UserIdentities", "Id", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.UserIdentities", "RoleId", "dbo.Roles", "Id");
            AddForeignKey("dbo.ThemeFiles", "ThemeId", "dbo.Themes", "Id");
            AddForeignKey("dbo.Themes", "AuthorId", "dbo.Users", "Id");
            AddForeignKey("dbo.Messages", "ThemeId", "dbo.Themes", "Id", cascadeDelete: true);
            DropColumn("dbo.Subjects", "GroupSemesterId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Subjects", "GroupSemesterId", c => c.Guid(nullable: false));
            DropForeignKey("dbo.Messages", "ThemeId", "dbo.Themes");
            DropForeignKey("dbo.Themes", "AuthorId", "dbo.Users");
            DropForeignKey("dbo.ThemeFiles", "ThemeId", "dbo.Themes");
            DropForeignKey("dbo.UserIdentities", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.UserIdentities", "Id", "dbo.Users");
            DropForeignKey("dbo.Messages", "AuthorId", "dbo.Users");
            DropForeignKey("dbo.GroupSemesterSubjects", "SubjectId", "dbo.Subjects");
            DropIndex("dbo.Roles", "RoleName_Index");
            DropIndex("dbo.UserIdentities", "UserIdentityEmail_Index");
            DropIndex("dbo.GroupSemesterSubjects", new[] { "GroupSemesterId" });
            DropIndex("dbo.GroupSemesterSubjects", new[] { "SubjectId" });
            DropIndex("dbo.Messages", new[] { "ThemeId" });
            DropIndex("dbo.Files", new[] { "AvatarId" });
            AlterColumn("dbo.Themes", "Title", c => c.String(maxLength: 100));
            AlterColumn("dbo.Subjects", "Name", c => c.String(maxLength: 100));
            AlterColumn("dbo.Semesters", "Name", c => c.String(maxLength: 50));
            AlterColumn("dbo.Messages", "ThemeId", c => c.Guid());
            DropColumn("dbo.Files", "AvatarId");
            DropTable("dbo.GroupSemesterSubjects");
            RenameColumn(table: "dbo.Messages", name: "ThemeId", newName: "Theme_Id");
            CreateIndex("dbo.Roles", "Name", unique: true, clustered: true, name: "Index_RoleName");
            CreateIndex("dbo.UserIdentities", "Email", unique: true, clustered: true, name: "Index_UserIdentityEmail");
            CreateIndex("dbo.Subjects", "GroupSemesterId");
            CreateIndex("dbo.Users", "AvatarId");
            CreateIndex("dbo.Messages", "Theme_Id");
            AddForeignKey("dbo.Messages", "Theme_Id", "dbo.Themes", "Id");
            AddForeignKey("dbo.Themes", "AuthorId", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ThemeFiles", "ThemeId", "dbo.Themes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.UserIdentities", "RoleId", "dbo.Roles", "Id", cascadeDelete: true);
            AddForeignKey("dbo.UserIdentities", "Id", "dbo.Users", "Id");
            AddForeignKey("dbo.Messages", "AuthorId", "dbo.Users", "Id", cascadeDelete: true);
        }
    }
}
