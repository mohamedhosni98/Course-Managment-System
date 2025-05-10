using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseManagment.Data.Migrations
{
    /// <inheritdoc />
    public partial class nullimage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "Students",
                type: "nvarchar(200)",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "VARBINARY(MAX)");

          

            
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.AlterColumn<byte[]>(
                name: "Image",
                table: "Students",
                type: "VARBINARY(MAX)",
                nullable: false,
                defaultValue: new byte[0],
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldNullable: true);

           

        }
    }
}
