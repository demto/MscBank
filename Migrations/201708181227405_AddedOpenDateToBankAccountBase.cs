namespace MScBank.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedOpenDateToBankAccountBase : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BankAccountBases", "OpenDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.BankAccountBases", "OpenDate");
        }
    }
}
