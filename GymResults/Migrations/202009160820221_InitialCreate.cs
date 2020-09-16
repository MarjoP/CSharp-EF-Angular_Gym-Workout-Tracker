namespace GymResults.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Exercises",
                c => new
                    {
                        ExerciseId = c.Int(nullable: false, identity: true),
                        ExerciseName = c.String(),
                    })
                .PrimaryKey(t => t.ExerciseId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.WorkoutDatas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Result_Repeats = c.Int(nullable: false),
                        Result_Weight = c.Int(nullable: false),
                        Exercise_ExerciseId = c.Int(),
                        User_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Exercises", t => t.Exercise_ExerciseId)
                .ForeignKey("dbo.Users", t => t.User_UserId)
                .Index(t => t.Exercise_ExerciseId)
                .Index(t => t.User_UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WorkoutDatas", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.WorkoutDatas", "Exercise_ExerciseId", "dbo.Exercises");
            DropIndex("dbo.WorkoutDatas", new[] { "User_UserId" });
            DropIndex("dbo.WorkoutDatas", new[] { "Exercise_ExerciseId" });
            DropTable("dbo.WorkoutDatas");
            DropTable("dbo.Users");
            DropTable("dbo.Exercises");
        }
    }
}
