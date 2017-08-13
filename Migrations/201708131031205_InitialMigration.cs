namespace MScBank.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BankAccountBases",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Balance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SortCode = c.String(),
                        AccountNumber = c.String(),
                        BankCardId = c.Int(),
                        InterestRate = c.Double(),
                        Payment = c.Decimal(precision: 18, scale: 2),
                        Limit = c.Decimal(precision: 18, scale: 2),
                        Term = c.Int(),
                        LendingAmount = c.Decimal(precision: 18, scale: 2),
                        SecurityAddress = c.String(),
                        InterestRate1 = c.Double(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BankCards", t => t.BankCardId)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.BankCardId)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.BankCards",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CardNumber = c.String(),
                        PinNumber = c.Int(nullable: false),
                        ExpiryDate = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.AspNetUsers", "BankAccountBaseId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BankAccountBases", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.BankAccountBases", "BankCardId", "dbo.BankCards");
            DropIndex("dbo.BankAccountBases", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.BankAccountBases", new[] { "BankCardId" });
            DropColumn("dbo.AspNetUsers", "BankAccountBaseId");
            DropTable("dbo.BankCards");
            DropTable("dbo.BankAccountBases");
        }
    }
}
