namespace MScBank.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FewChanges : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Transactions", "FromCurrentBalance", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Transactions", "ToCurrentBalance", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Transactions", "Description", c => c.String());
            DropColumn("dbo.Transactions", "CurrentBalance");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Transactions", "CurrentBalance", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Transactions", "Description");
            DropColumn("dbo.Transactions", "ToCurrentBalance");
            DropColumn("dbo.Transactions", "FromCurrentBalance");
        }
    }
}
