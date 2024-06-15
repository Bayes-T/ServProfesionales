using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServProfesionales.Migrations
{
    /// <inheritdoc />
    public partial class correcciondb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Professionals_ProfessionalId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Services_Professionals_ProfessionalId",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_Services_ProfessionalId",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_ProfessionalId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "ProfessionalId",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "ProfessionalId",
                table: "Appointments");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Client",
                newName: "ClientId");

            migrationBuilder.RenameColumn(
                name: "startingDate",
                table: "Appointments",
                newName: "StartingDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ClientId",
                table: "Client",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "StartingDate",
                table: "Appointments",
                newName: "startingDate");

            migrationBuilder.AddColumn<string>(
                name: "ProfessionalId",
                table: "Services",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProfessionalId",
                table: "Appointments",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Services_ProfessionalId",
                table: "Services",
                column: "ProfessionalId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_ProfessionalId",
                table: "Appointments",
                column: "ProfessionalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Professionals_ProfessionalId",
                table: "Appointments",
                column: "ProfessionalId",
                principalTable: "Professionals",
                principalColumn: "ProfessionalId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Professionals_ProfessionalId",
                table: "Services",
                column: "ProfessionalId",
                principalTable: "Professionals",
                principalColumn: "ProfessionalId");
        }
    }
}
