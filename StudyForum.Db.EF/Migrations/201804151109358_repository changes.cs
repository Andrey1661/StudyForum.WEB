namespace StudyForum.Db.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class repositorychanges : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Repositories", "LinkId", c => c.Guid());
            AddColumn("dbo.Repositories", "LinkCreationDate", c => c.DateTime());
            AlterColumn("dbo.Subjects", "Name", c => c.String(nullable: false, maxLength: 250));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Subjects", "Name", c => c.String(nullable: false, maxLength: 100));
            DropColumn("dbo.Repositories", "LinkCreationDate");
            DropColumn("dbo.Repositories", "LinkId");
        }
    }
}
