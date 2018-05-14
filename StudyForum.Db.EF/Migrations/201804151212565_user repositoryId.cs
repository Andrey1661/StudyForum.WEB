namespace StudyForum.Db.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userrepositoryId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "RepositoryId", c => c.Guid());
            CreateIndex("dbo.Users", "RepositoryId");
            AddForeignKey("dbo.Users", "RepositoryId", "dbo.Repositories", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "RepositoryId", "dbo.Repositories");
            DropIndex("dbo.Users", new[] { "RepositoryId" });
            DropColumn("dbo.Users", "RepositoryId");
        }
    }
}
