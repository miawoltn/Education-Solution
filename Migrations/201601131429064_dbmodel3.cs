namespace Education_Solution.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dbmodel3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Questions", "Paper", c => c.String(maxLength: 50));
            AlterColumn("dbo.Questions", "QuestionName", c => c.String(nullable: false, maxLength: 50));
            CreateIndex("dbo.Questions", new[] { "Owner", "Paper", "QuestionName" }, unique: true, name: "IX_OPQ");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Questions", "IX_OPQ");
            AlterColumn("dbo.Questions", "QuestionName", c => c.String(nullable: false));
            AlterColumn("dbo.Questions", "Paper", c => c.String());
        }
    }
}
