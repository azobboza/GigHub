namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFollowingClass : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Followings",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        ArtistId = c.Int(nullable: false),
                        Artist_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.ArtistId })
                .ForeignKey("dbo.AspNetUsers", t => t.Artist_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.Artist_Id)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Followings", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Followings", "Artist_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Followings", new[] { "UserId" });
            DropIndex("dbo.Followings", new[] { "Artist_Id" });
            DropTable("dbo.Followings");
        }
    }
}
