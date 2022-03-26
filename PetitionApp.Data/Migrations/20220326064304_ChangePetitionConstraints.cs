using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetitionApp.Data.Migrations
{
    public partial class ChangePetitionConstraints : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateOnly>(
                name: "CreationDate",
                table: "Petitions",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(2022, 3, 26),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValue: new DateOnly(2022, 3, 21));

            migrationBuilder.CreateIndex(
                name: "IX_Petitions_Title",
                table: "Petitions",
                column: "Title",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Petitions_Title",
                table: "Petitions");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "CreationDate",
                table: "Petitions",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(2022, 3, 21),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValue: new DateOnly(2022, 3, 26));
        }
    }
}
