namespace MScBank.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedRequiredFromOldPinAgainAgainAgain : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BankCards", "OldNum", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.BankCards", "OldNum");
        }
    }
}
