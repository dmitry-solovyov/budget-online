namespace BudgetOnline.Data.MSSQL.EF.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Sections",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Description = c.String(maxLength: 512),
                        IsDisabled = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedWhen = c.DateTime(nullable: false),
                        CreatedBy = c.Guid(nullable: false),
                        UpdatedWhen = c.DateTime(),
                        UpdatedBy = c.Guid(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedBy, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UpdatedBy)
                .Index(t => t.CreatedBy)
                .Index(t => t.UpdatedBy);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        SectionId = c.Guid(),
                        Email = c.String(nullable: false, maxLength: 255),
                        UserName = c.String(nullable: false, maxLength: 255),
                        IsDisabled = c.Boolean(nullable: false),
                        CreatedWhen = c.DateTime(nullable: false),
                        CreatedBy = c.Guid(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedBy)
                .ForeignKey("dbo.Sections", t => t.SectionId)
                .Index(t => t.SectionId)
                .Index(t => t.CreatedBy);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sections", "UpdatedBy", "dbo.Users");
            DropForeignKey("dbo.Sections", "CreatedBy", "dbo.Users");
            DropForeignKey("dbo.Users", "SectionId", "dbo.Sections");
            DropForeignKey("dbo.Users", "CreatedBy", "dbo.Users");
            DropIndex("dbo.Users", new[] { "CreatedBy" });
            DropIndex("dbo.Users", new[] { "SectionId" });
            DropIndex("dbo.Sections", new[] { "UpdatedBy" });
            DropIndex("dbo.Sections", new[] { "CreatedBy" });
            DropTable("dbo.Users");
            DropTable("dbo.Sections");
        }
    }
}
