namespace MScBank.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedIdsBasedOn1stNormalisation : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Transactions", "BankAccountBase_Id", "dbo.BankAccountBases");
            DropIndex("dbo.Transactions", new[] { "BankAccountBase_Id" });
            RenameColumn(table: "dbo.Transactions", name: "BankAccountBase_Id", newName: "BankAccountBaseId");
            RenameColumn(table: "dbo.BankAccountBases", name: "ApplicationUser_Id", newName: "ApplicationUserId");
            RenameIndex(table: "dbo.BankAccountBases", name: "IX_ApplicationUser_Id", newName: "IX_ApplicationUserId");
            AlterColumn("dbo.Transactions", "BankAccountBaseId", c => c.Int(nullable: false));
            CreateIndex("dbo.Transactions", "BankAccountBaseId");
            AddForeignKey("dbo.Transactions", "BankAccountBaseId", "dbo.BankAccountBases", "Id", cascadeDelete: true);
            DropColumn("dbo.AspNetUsers", "BankAccountBaseId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "BankAccountBaseId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Transactions", "BankAccountBaseId", "dbo.BankAccountBases");
            DropIndex("dbo.Transactions", new[] { "BankAccountBaseId" });
            AlterColumn("dbo.Transactions", "BankAccountBaseId", c => c.Int());
            RenameIndex(table: "dbo.BankAccountBases", name: "IX_ApplicationUserId", newName: "IX_ApplicationUser_Id");
            RenameColumn(table: "dbo.BankAccountBases", name: "ApplicationUserId", newName: "ApplicationUser_Id");
            RenameColumn(table: "dbo.Transactions", name: "BankAccountBaseId", newName: "BankAccountBase_Id");
            CreateIndex("dbo.Transactions", "BankAccountBase_Id");
            AddForeignKey("dbo.Transactions", "BankAccountBase_Id", "dbo.BankAccountBases", "Id");
        }
    }
}
