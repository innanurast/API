using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class add_regis_model : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Account_Employees_NIK",
                table: "Account");

            migrationBuilder.DropForeignKey(
                name: "FK_Profilling_Account_NIK",
                table: "Profilling");

            migrationBuilder.DropForeignKey(
                name: "FK_Profilling_Education_educationId",
                table: "Profilling");

            migrationBuilder.DropForeignKey(
                name: "FK_University_Education_EducationId",
                table: "University");

            migrationBuilder.DropPrimaryKey(
                name: "PK_University",
                table: "University");

            migrationBuilder.DropIndex(
                name: "IX_University_EducationId",
                table: "University");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Profilling",
                table: "Profilling");

            migrationBuilder.DropIndex(
                name: "IX_Profilling_educationId",
                table: "Profilling");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Education",
                table: "Education");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Account",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "EducationId",
                table: "University");

            migrationBuilder.DropColumn(
                name: "educationId",
                table: "Profilling");

            migrationBuilder.RenameTable(
                name: "University",
                newName: "universities");

            migrationBuilder.RenameTable(
                name: "Profilling",
                newName: "profillings");

            migrationBuilder.RenameTable(
                name: "Education",
                newName: "Educations");

            migrationBuilder.RenameTable(
                name: "Account",
                newName: "Accounts");

            migrationBuilder.AlterColumn<int>(
                name: "Degree",
                table: "Educations",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_universities",
                table: "universities",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_profillings",
                table: "profillings",
                column: "NIK");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Educations",
                table: "Educations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Accounts",
                table: "Accounts",
                column: "NIK");

            migrationBuilder.CreateIndex(
                name: "IX_profillings_Education_id",
                table: "profillings",
                column: "Education_id");

            migrationBuilder.CreateIndex(
                name: "IX_Educations_University_id",
                table: "Educations",
                column: "University_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Employees_NIK",
                table: "Accounts",
                column: "NIK",
                principalTable: "Employees",
                principalColumn: "NIK",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Educations_universities_University_id",
                table: "Educations",
                column: "University_id",
                principalTable: "universities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_profillings_Accounts_NIK",
                table: "profillings",
                column: "NIK",
                principalTable: "Accounts",
                principalColumn: "NIK",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_profillings_Educations_Education_id",
                table: "profillings",
                column: "Education_id",
                principalTable: "Educations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Employees_NIK",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Educations_universities_University_id",
                table: "Educations");

            migrationBuilder.DropForeignKey(
                name: "FK_profillings_Accounts_NIK",
                table: "profillings");

            migrationBuilder.DropForeignKey(
                name: "FK_profillings_Educations_Education_id",
                table: "profillings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_universities",
                table: "universities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_profillings",
                table: "profillings");

            migrationBuilder.DropIndex(
                name: "IX_profillings_Education_id",
                table: "profillings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Educations",
                table: "Educations");

            migrationBuilder.DropIndex(
                name: "IX_Educations_University_id",
                table: "Educations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Accounts",
                table: "Accounts");

            migrationBuilder.RenameTable(
                name: "universities",
                newName: "University");

            migrationBuilder.RenameTable(
                name: "profillings",
                newName: "Profilling");

            migrationBuilder.RenameTable(
                name: "Educations",
                newName: "Education");

            migrationBuilder.RenameTable(
                name: "Accounts",
                newName: "Account");

            migrationBuilder.AddColumn<int>(
                name: "EducationId",
                table: "University",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "educationId",
                table: "Profilling",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Degree",
                table: "Education",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_University",
                table: "University",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Profilling",
                table: "Profilling",
                column: "NIK");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Education",
                table: "Education",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Account",
                table: "Account",
                column: "NIK");

            migrationBuilder.CreateIndex(
                name: "IX_University_EducationId",
                table: "University",
                column: "EducationId");

            migrationBuilder.CreateIndex(
                name: "IX_Profilling_educationId",
                table: "Profilling",
                column: "educationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Account_Employees_NIK",
                table: "Account",
                column: "NIK",
                principalTable: "Employees",
                principalColumn: "NIK",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Profilling_Account_NIK",
                table: "Profilling",
                column: "NIK",
                principalTable: "Account",
                principalColumn: "NIK",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Profilling_Education_educationId",
                table: "Profilling",
                column: "educationId",
                principalTable: "Education",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_University_Education_EducationId",
                table: "University",
                column: "EducationId",
                principalTable: "Education",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
