namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addAttendance : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Attendances",
                c => new
                    {
                        GigId = c.Int(nullable: false),
                        AttendeeId = c.String(nullable: false, maxLength: 128),
                        Attende_Id = c.String(maxLength: 128),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.GigId, t.AttendeeId })
                .ForeignKey("dbo.AspNetUsers", t => t.Attende_Id)
                .ForeignKey("dbo.Gigs", t => t.GigId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.Attende_Id)
                .Index(t => t.GigId)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Attendances", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Attendances", "GigId", "dbo.Gigs");
            DropForeignKey("dbo.Attendances", "Attende_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Attendances", new[] { "User_Id" });
            DropIndex("dbo.Attendances", new[] { "GigId" });
            DropIndex("dbo.Attendances", new[] { "Attende_Id" });
            DropTable("dbo.Attendances");
        }
    }
}
