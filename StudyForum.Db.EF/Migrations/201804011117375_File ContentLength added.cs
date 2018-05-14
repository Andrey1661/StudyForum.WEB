namespace StudyForum.Db.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FileContentLengthadded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Files", "ContentLength", c => c.Int(nullable: false));
            AlterColumn("dbo.Files", "FileType", c => c.String(maxLength: 200));
            AlterColumn("dbo.Files", "FileName", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Files", "FileType", c => c.String());
            AlterColumn("dbo.Files", "FileName", c => c.String());
            DropColumn("dbo.Files", "ContentLength");
        }
    }
}
