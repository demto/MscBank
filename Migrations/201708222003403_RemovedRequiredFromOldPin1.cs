namespace MScBank.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedRequiredFromOldPin1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.BankCards", "OldPinNumber");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BankCards", "OldPinNumber", c => c.Int(nullable: false));
        }
    }
}
