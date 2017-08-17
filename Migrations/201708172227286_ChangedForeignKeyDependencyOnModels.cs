namespace MScBank.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedForeignKeyDependencyOnModels : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.BankAccountBases", name: "BankCardId", newName: "BankCard_Id");
            RenameIndex(table: "dbo.BankAccountBases", name: "IX_BankCardId", newName: "IX_BankCard_Id");
            AddColumn("dbo.BankCards", "BankAccountBaseId", c => c.Int(nullable: false));
            CreateIndex("dbo.BankCards", "BankAccountBaseId");
            AddForeignKey("dbo.BankCards", "BankAccountBaseId", "dbo.BankAccountBases", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BankCards", "BankAccountBaseId", "dbo.BankAccountBases");
            DropIndex("dbo.BankCards", new[] { "BankAccountBaseId" });
            DropColumn("dbo.BankCards", "BankAccountBaseId");
            RenameIndex(table: "dbo.BankAccountBases", name: "IX_BankCard_Id", newName: "IX_BankCardId");
            RenameColumn(table: "dbo.BankAccountBases", name: "BankCard_Id", newName: "BankCardId");
        }
    }
}
