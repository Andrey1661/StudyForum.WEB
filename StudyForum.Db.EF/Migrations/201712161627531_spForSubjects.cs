namespace StudyForum.Db.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class spForSubjects : DbMigration
    {
        public override void Up()
        {
            CreateStoredProcedure("SelectSubjectsBySemesterAndGroup", pb => new
                {
                    groupId = pb.Guid(),
                    semesterId = pb.Guid()
                },
                @"select gss.Id, s.Name from Subjects s
              join GroupSemesterSubjects gss on gss.SubjectId = s.Id
              join GroupSemesters gs on gs.Id = gss.GroupSemesterId
              join Groups g on g.Id = gs.GroupId
              join Semesters sem on sem.Id = gs.SemesterId 
              where g.Id = @groupId and sem.Id = @semesterId");
        }
        
        public override void Down()
        {
            DropStoredProcedure("SelectSubjectsBySemesterAndGroup");
        }
    }
}
