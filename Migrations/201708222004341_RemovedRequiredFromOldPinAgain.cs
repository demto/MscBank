namespace MScBank.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedRequiredFromOldPinAgain : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BankCards", "OldPinNumber", c => c.Int(nullable: true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.BankCards", "OldPinNumber");
        }
    }
}
