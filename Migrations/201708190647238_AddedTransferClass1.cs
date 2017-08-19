namespace MScBank.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedTransferClass1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Transactions", "ToAccountId", c => c.Int());
            AddColumn("dbo.Transactions", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Transactions", "ToAccountId");
            AddForeignKey("dbo.Transactions", "ToAccountId", "dbo.BankAccountBases", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transactions", "ToAccountId", "dbo.BankAccountBases");
            DropIndex("dbo.Transactions", new[] { "ToAccountId" });
            DropColumn("dbo.Transactions", "Discriminator");
            DropColumn("dbo.Transactions", "ToAccountId");
        }
    }
}
