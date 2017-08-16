namespace MScBank.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Changesinmodel : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.BankAccountBases", "TransactionId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BankAccountBases", "TransactionId", c => c.Int(nullable: false));
        }
    }
}
