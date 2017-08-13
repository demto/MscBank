namespace MScBank.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedTransactionListToAccountBase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TransactionTimeStamp = c.DateTime(),
                        BankAccountBase_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BankAccountBases", t => t.BankAccountBase_Id)
                .Index(t => t.BankAccountBase_Id);
            
            AddColumn("dbo.BankAccountBases", "TransactionId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transactions", "BankAccountBase_Id", "dbo.BankAccountBases");
            DropIndex("dbo.Transactions", new[] { "BankAccountBase_Id" });
            DropColumn("dbo.BankAccountBases", "TransactionId");
            DropTable("dbo.Transactions");
        }
    }
}
