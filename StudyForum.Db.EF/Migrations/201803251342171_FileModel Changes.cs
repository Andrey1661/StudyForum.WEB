namespace StudyForum.Db.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FileModelChanges : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Files", "FileName", c => c.String(nullable: false, maxLength: 200));
            AddColumn("dbo.Files", "FileType", c => c.String());
            DropColumn("dbo.Files", "PhysicalPath");
            DropColumn("dbo.Files", "Extension");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Files", "Extension", c => c.String());
            AddColumn("dbo.Files", "PhysicalPath", c => c.String(nullable: false, maxLength: 200));
            DropColumn("dbo.Files", "FileType");
            DropColumn("dbo.Files", "FileName");
        }
    }
}
