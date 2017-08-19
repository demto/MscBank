namespace MScBank.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedNameOnCardToBankCard : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BankCards", "NameOnCard", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.BankCards", "NameOnCard");
        }
    }
}
