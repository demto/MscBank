namespace MScBank.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedOldPinToBankCard : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BankCards", "OldPinNumber", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.BankCards", "OldPinNumber");
        }
    }
}
