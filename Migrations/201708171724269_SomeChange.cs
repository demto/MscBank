namespace MScBank.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SomeChange : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.BankAccountBases", "Name", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.BankAccountBases", "Name", c => c.String(nullable: false));
        }
    }
}
