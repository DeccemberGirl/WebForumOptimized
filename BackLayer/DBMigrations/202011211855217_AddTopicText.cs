using System;
using System.Data.Entity.Migrations;

namespace DataLayer.DBMigrations
{
    public partial class AddTopicText : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Topic", "Text", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Topic", "Text");
        }
    }
}
