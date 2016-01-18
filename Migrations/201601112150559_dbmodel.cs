namespace Education_Solution.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dbmodel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Owner = c.String(maxLength: 50),
                        Paper = c.String(),
                        QuestionName = c.String(nullable: false),
                        Text = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Questions");
        }
    }
}
