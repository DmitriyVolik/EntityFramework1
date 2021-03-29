using Microsoft.EntityFrameworkCore.Migrations;

namespace ConsoleApp1.Migrations
{
    public partial class Custom_raw_sql_code_execute : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE FUNCTION example()
    RETURNS INT
AS
BEGIN
    RETURN 42
END
");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
