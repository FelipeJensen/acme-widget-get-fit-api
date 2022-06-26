using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AcmeWidget.GetFit.Data.Migrations
{
    public partial class activitiessignup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActivitySignUps",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    YearsOfExperienceInActivity = table.Column<int>(type: "int", nullable: true),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActivityId = table.Column<long>(type: "bigint", nullable: false),
                    ActivityDateId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivitySignUps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActivitySignUps_Activities_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "Activities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ActivitySignUps_ActivityDates_ActivityDateId",
                        column: x => x.ActivityDateId,
                        principalTable: "ActivityDates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActivitySignUps_ActivityDateId",
                table: "ActivitySignUps",
                column: "ActivityDateId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivitySignUps_ActivityId",
                table: "ActivitySignUps",
                column: "ActivityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivitySignUps");
        }
    }
}
