namespace MScBank.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedNewPropertiesToBankAccountBase : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BankAccountBases", "Name", c => c.String());
            AddColumn("dbo.BankAccountBases", "Type", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.BankAccountBases", "Type");
            DropColumn("dbo.BankAccountBases", "Name");
        }
    }
}
