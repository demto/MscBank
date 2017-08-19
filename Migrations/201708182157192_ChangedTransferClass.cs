namespace MScBank.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedTransferClass : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Transactions", newName: "Transfers");
            DropIndex("dbo.Transfers", new[] { "BankAccountBaseId" });
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TransactionTimeStamp = c.DateTime(),
                        BankAccountBaseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.BankAccountBaseId);
            
            AddColumn("dbo.Transfers", "TimeStamp", c => c.DateTime(nullable: false));
            AddColumn("dbo.Transfers", "FromAccount_Id", c => c.Int());
            AlterColumn("dbo.Transfers", "BankAccountBaseId2", c => c.Int(nullable: true));
            CreateIndex("dbo.Transfers", "FromAccount_Id");
            AddForeignKey("dbo.Transfers", "FromAccount_Id", "dbo.BankAccountBases", "Id");
            DropColumn("dbo.Transfers", "TransactionTimeStamp");
            DropColumn("dbo.Transfers", "Discriminator");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Transfers", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.Transfers", "TransactionTimeStamp", c => c.DateTime());
            DropForeignKey("dbo.Transfers", "FromAccount_Id", "dbo.BankAccountBases");
            DropIndex("dbo.Transfers", new[] { "FromAccount_Id" });
            DropIndex("dbo.Transactions", new[] { "BankAccountBaseId" });
            AlterColumn("dbo.Transfers", "BankAccountBaseId2", c => c.Int());
            DropColumn("dbo.Transfers", "FromAccount_Id");
            DropColumn("dbo.Transfers", "TimeStamp");
            DropTable("dbo.Transactions");
            CreateIndex("dbo.Transfers", "BankAccountBaseId");
            RenameTable(name: "dbo.Transfers", newName: "Transactions");
        }
    }
}
