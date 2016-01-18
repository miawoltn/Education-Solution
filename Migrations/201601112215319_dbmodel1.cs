namespace Education_Solution.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dbmodel1 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Questions");
            AlterColumn("dbo.Questions", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Questions", "Owner", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Questions", "Paper", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Questions", "QuestionName", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Questions", new[] { "Id", "Owner", "Paper", "QuestionName" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Questions");
            AlterColumn("dbo.Questions", "QuestionName", c => c.String(nullable: false));
            AlterColumn("dbo.Questions", "Paper", c => c.String());
            AlterColumn("dbo.Questions", "Owner", c => c.String(maxLength: 50));
            AlterColumn("dbo.Questions", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Questions", "Id");
        }
    }
}
