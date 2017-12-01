namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeVemueToVenueIntoGig : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Gigs", "Venue", c => c.String(nullable: false, maxLength: 255));
            DropColumn("dbo.Gigs", "Vemue");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Gigs", "Vemue", c => c.String(nullable: false, maxLength: 255));
            DropColumn("dbo.Gigs", "Venue");
        }
    }
}
