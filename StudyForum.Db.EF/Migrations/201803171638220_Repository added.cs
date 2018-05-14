namespace StudyForum.Db.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Repositoryadded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Repositories",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        OwnerId = c.Guid(nullable: false),
                        OwnerType = c.Int(nullable: false),
                        Shared = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RepositoryFiles",
                c => new
                    {
                        RepositoryId = c.Guid(nullable: false),
                        FileId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.RepositoryId, t.FileId })
                .ForeignKey("dbo.Repositories", t => t.RepositoryId, cascadeDelete: true)
                .ForeignKey("dbo.Files", t => t.FileId, cascadeDelete: true)
                .Index(t => t.RepositoryId)
                .Index(t => t.FileId);
            
            AddColumn("dbo.Files", "Extension", c => c.String());
            AddColumn("dbo.Files", "UploadDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Files", "UploaderId", c => c.Guid(nullable: false));
            AddColumn("dbo.GroupSemesters", "RepositoryId", c => c.Guid());
            CreateIndex("dbo.Files", "UploaderId");
            CreateIndex("dbo.GroupSemesters", "RepositoryId");
            AddForeignKey("dbo.Files", "UploaderId", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.GroupSemesters", "RepositoryId", "dbo.Repositories", "Id");
            DropColumn("dbo.Files", "IsIndependent");
            DropColumn("dbo.Groups", "Description");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Groups", "Description", c => c.String());
            AddColumn("dbo.Files", "IsIndependent", c => c.Boolean(nullable: false));
            DropForeignKey("dbo.RepositoryFiles", "FileId", "dbo.Files");
            DropForeignKey("dbo.GroupSemesters", "RepositoryId", "dbo.Repositories");
            DropForeignKey("dbo.RepositoryFiles", "RepositoryId", "dbo.Repositories");
            DropForeignKey("dbo.Files", "UploaderId", "dbo.Users");
            DropIndex("dbo.RepositoryFiles", new[] { "FileId" });
            DropIndex("dbo.RepositoryFiles", new[] { "RepositoryId" });
            DropIndex("dbo.GroupSemesters", new[] { "RepositoryId" });
            DropIndex("dbo.Files", new[] { "UploaderId" });
            DropColumn("dbo.GroupSemesters", "RepositoryId");
            DropColumn("dbo.Files", "UploaderId");
            DropColumn("dbo.Files", "UploadDate");
            DropColumn("dbo.Files", "Extension");
            DropTable("dbo.RepositoryFiles");
            DropTable("dbo.Repositories");
        }
    }
}
