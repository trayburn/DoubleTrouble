using FluentMigrator;
using FluentMigrator.SqlServer;
using System;
using System.Collections.Generic;
using System.Text;

namespace DoubleTrouble.Migrator.Migrations
{
    [Migration(202105112028)]
    public class AddDemoTable : Migration
    {
        public override void Down()
        {
            Delete.Table("Demo");
        }

        public override void Up()
        {
            Create.Table("Demo")
                .WithColumn("Id").AsGuid().PrimaryKey().WithDefault(SystemMethods.NewSequentialId)
                .WithColumn("Name").AsString(500);
        }
    }
}
