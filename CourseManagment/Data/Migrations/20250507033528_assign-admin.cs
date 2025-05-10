using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseManagment.Data.Migrations
{
    /// <inheritdoc />
    public partial class assignadmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Address], [First_Name], [Last_Name]) VALUES (N'b7dcbc72-3f16-4ce4-9d34-2c8726370217', N'mohamed@yahoo.com', N'MOHAMED@YAHOO.COM', N'mohamed@yahoo.com', N'MOHAMED@YAHOO.COM', 0, N'AQAAAAIAAYagAAAAEIe7ZLiZZL0Wgn9fo6MSymZQvklibNJpPTeUxh6c1ajvgeSF3VkyTnFXloz4KkN0/g==', N'OAQHJHOUVGYIIG63UPUAOEAGZAB5QR73', N'962da8ed-d25f-4209-9d02-c11341bf960d', NULL, 0, 0, NULL, 1, 0, N'bani suef', N'mohamed', N'hussien')\r\n");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [dbo].[AspNetUsers] WHERE ID='b7dcbc72-3f16-4ce4-9d34-2c8726370217' ");
        }
    }
}
