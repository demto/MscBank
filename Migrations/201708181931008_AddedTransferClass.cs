namespace MScBank.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedTransferClass : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Transactions", "BankAccountBaseId2", c => c.Int());
            AddColumn("dbo.Transactions", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.Transactions", "ToAccount_Id", c => c.Int());
            CreateIndex("dbo.Transactions", "ToAccount_Id");
            AddForeignKey("dbo.Transactions", "ToAccount_Id", "dbo.BankAccountBases", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transactions", "ToAccount_Id", "dbo.BankAccountBases");
            DropIndex("dbo.Transactions", new[] { "ToAccount_Id" });
            DropColumn("dbo.Transactions", "ToAccount_Id");
            DropColumn("dbo.Transactions", "Discriminator");
            DropColumn("dbo.Transactions", "BankAccountBaseId2");
        }
    }
}
