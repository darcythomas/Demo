namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Demo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "Demo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("Demo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("Demo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "Demo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "Demo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Demo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "Demo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("Demo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("Demo.AspNetUserRoles", "UserId", "Demo.AspNetUsers");
            DropForeignKey("Demo.AspNetUserLogins", "UserId", "Demo.AspNetUsers");
            DropForeignKey("Demo.AspNetUserClaims", "UserId", "Demo.AspNetUsers");
            DropForeignKey("Demo.AspNetUserRoles", "RoleId", "Demo.AspNetRoles");
            DropIndex("Demo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("Demo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("Demo.AspNetUsers", "UserNameIndex");
            DropIndex("Demo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("Demo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("Demo.AspNetRoles", "RoleNameIndex");
            DropTable("Demo.AspNetUserLogins");
            DropTable("Demo.AspNetUserClaims");
            DropTable("Demo.AspNetUsers");
            DropTable("Demo.AspNetUserRoles");
            DropTable("Demo.AspNetRoles");
        }
    }
}
