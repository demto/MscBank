namespace MScBank.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MyAccountsViewModelChange : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.BankAccountBases", "Name", c => c.String(nullable: true));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.BankAccountBases", "Name", c => c.String());
        }
    }
}
