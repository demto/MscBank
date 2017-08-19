namespace MScBank.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeletedTransfersClass : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Transfers", "FromAccountId", "dbo.BankAccountBases");
            DropForeignKey("dbo.Transfers", "ToAccountId", "dbo.BankAccountBases");
            DropIndex("dbo.Transfers", new[] { "FromAccountId" });
            DropIndex("dbo.Transfers", new[] { "ToAccountId" });
            DropTable("dbo.Transfers");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Transfers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TimeStamp = c.DateTime(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FromAccountId = c.Int(nullable: false),
                        ToAccountId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.Transfers", "ToAccountId");
            CreateIndex("dbo.Transfers", "FromAccountId");
            AddForeignKey("dbo.Transfers", "ToAccountId", "dbo.BankAccountBases", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Transfers", "FromAccountId", "dbo.BankAccountBases", "Id", cascadeDelete: true);
        }
    }
}
