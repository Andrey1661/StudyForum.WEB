namespace StudyForum.Db.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                    "dbo.Files",
                    c => new
                    {
                        Id = c.Guid(nullable: false),
                        PhysicalPath = c.String(nullable: false, maxLength: 200),
                        IsIndependent = c.Boolean(nullable: false)
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MessageFiles",
                c => new
                    {
                        MessageId = c.Guid(nullable: false),
                        FileId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.MessageId, t.FileId })
                .ForeignKey("dbo.Messages", t => t.MessageId, cascadeDelete: true)
                .ForeignKey("dbo.Files", t => t.FileId, cascadeDelete: true)
                .Index(t => t.MessageId)
                .Index(t => t.FileId);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Content = c.String(),
                        CreationDate = c.DateTime(nullable: false),
                        AuthorId = c.Guid(nullable: false),
                        ParentMessageId = c.Guid(),
                        ThemeId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.AuthorId)
                .ForeignKey("dbo.Messages", t => t.ParentMessageId)
                .ForeignKey("dbo.Themes", t => t.ThemeId, cascadeDelete: true)
                .Index(t => t.AuthorId)
                .Index(t => t.ParentMessageId)
                .Index(t => t.ThemeId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FirstName = c.String(maxLength: 50),
                        SecondName = c.String(maxLength: 50),
                        Patronymic = c.String(maxLength: 50),
                        GroupId = c.Guid(),
                        AvatarId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Groups", t => t.GroupId)
                .Index(t => t.GroupId);
            
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.GroupSemesters",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        GroupId = c.Guid(nullable: false),
                        SemesterId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Semesters", t => t.SemesterId, cascadeDelete: true)
                .ForeignKey("dbo.Groups", t => t.GroupId, cascadeDelete: true)
                .Index(t => t.GroupId)
                .Index(t => t.SemesterId);
            
            CreateTable(
                "dbo.Semesters",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        DateBegin = c.DateTime(nullable: false),
                        DateEnd = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.GroupSemesterSubjects",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        SubjectId = c.Guid(nullable: false),
                        GroupSemesterId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Subjects", t => t.SubjectId, cascadeDelete: true)
                .ForeignKey("dbo.GroupSemesters", t => t.GroupSemesterId, cascadeDelete: true)
                .Index(t => t.SubjectId)
                .Index(t => t.GroupSemesterId);
            
            CreateTable(
                "dbo.Subjects",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Themes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(nullable: false, maxLength: 100),
                        Description = c.String(maxLength: 1000),
                        CreationDate = c.DateTime(nullable: false),
                        SubjectId = c.Guid(nullable: false),
                        AuthorId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.AuthorId)
                .ForeignKey("dbo.GroupSemesterSubjects", t => t.SubjectId, cascadeDelete: true)
                .Index(t => t.SubjectId)
                .Index(t => t.AuthorId);
            
            CreateTable(
                "dbo.ThemeFiles",
                c => new
                    {
                        ThemeId = c.Guid(nullable: false),
                        FileId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.ThemeId, t.FileId })
                .ForeignKey("dbo.Themes", t => t.ThemeId)
                .ForeignKey("dbo.Files", t => t.FileId, cascadeDelete: true)
                .Index(t => t.ThemeId)
                .Index(t => t.FileId);
            
            CreateTable(
                "dbo.UserIdentities",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Email = c.String(nullable: false, maxLength: 100),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        RegistrationDate = c.DateTime(nullable: false),
                        PasswordResetVerificationToken = c.Guid(),
                        RoleId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Roles", t => t.RoleId)
                .ForeignKey("dbo.Users", t => t.Id, cascadeDelete: true)
                .Index(t => t.Id)
                .Index(t => t.Email, unique: true, name: "UserIdentityEmail_Index")
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleName_Index");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ThemeFiles", "FileId", "dbo.Files");
            DropForeignKey("dbo.MessageFiles", "FileId", "dbo.Files");
            DropForeignKey("dbo.Messages", "ThemeId", "dbo.Themes");
            DropForeignKey("dbo.Messages", "ParentMessageId", "dbo.Messages");
            DropForeignKey("dbo.MessageFiles", "MessageId", "dbo.Messages");
            DropForeignKey("dbo.Messages", "AuthorId", "dbo.Users");
            DropForeignKey("dbo.UserIdentities", "Id", "dbo.Users");
            DropForeignKey("dbo.UserIdentities", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.Users", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.GroupSemesters", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.GroupSemesterSubjects", "GroupSemesterId", "dbo.GroupSemesters");
            DropForeignKey("dbo.Themes", "SubjectId", "dbo.GroupSemesterSubjects");
            DropForeignKey("dbo.ThemeFiles", "ThemeId", "dbo.Themes");
            DropForeignKey("dbo.Themes", "AuthorId", "dbo.Users");
            DropForeignKey("dbo.GroupSemesterSubjects", "SubjectId", "dbo.Subjects");
            DropForeignKey("dbo.GroupSemesters", "SemesterId", "dbo.Semesters");
            DropForeignKey("dbo.Files", "AvatarId", "dbo.Users");
            DropIndex("dbo.Roles", "RoleName_Index");
            DropIndex("dbo.UserIdentities", new[] { "RoleId" });
            DropIndex("dbo.UserIdentities", "UserIdentityEmail_Index");
            DropIndex("dbo.UserIdentities", new[] { "Id" });
            DropIndex("dbo.ThemeFiles", new[] { "FileId" });
            DropIndex("dbo.ThemeFiles", new[] { "ThemeId" });
            DropIndex("dbo.Themes", new[] { "AuthorId" });
            DropIndex("dbo.Themes", new[] { "SubjectId" });
            DropIndex("dbo.GroupSemesterSubjects", new[] { "GroupSemesterId" });
            DropIndex("dbo.GroupSemesterSubjects", new[] { "SubjectId" });
            DropIndex("dbo.GroupSemesters", new[] { "SemesterId" });
            DropIndex("dbo.GroupSemesters", new[] { "GroupId" });
            DropIndex("dbo.Users", new[] { "GroupId" });
            DropIndex("dbo.Messages", new[] { "ThemeId" });
            DropIndex("dbo.Messages", new[] { "ParentMessageId" });
            DropIndex("dbo.Messages", new[] { "AuthorId" });
            DropIndex("dbo.MessageFiles", new[] { "FileId" });
            DropIndex("dbo.MessageFiles", new[] { "MessageId" });
            DropIndex("dbo.Files", new[] { "AvatarId" });
            DropTable("dbo.Roles");
            DropTable("dbo.UserIdentities");
            DropTable("dbo.ThemeFiles");
            DropTable("dbo.Themes");
            DropTable("dbo.Subjects");
            DropTable("dbo.GroupSemesterSubjects");
            DropTable("dbo.Semesters");
            DropTable("dbo.GroupSemesters");
            DropTable("dbo.Groups");
            DropTable("dbo.Users");
            DropTable("dbo.Messages");
            DropTable("dbo.MessageFiles");
            DropTable("dbo.Files");
        }
    }
}
