using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RL.Data.Migrations
{
    public partial class AddAssignUserTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PlanProcedures",
                table: "PlanProcedures");

            migrationBuilder.AddColumn<int>(
                name: "PlanProcedureId",
                table: "PlanProcedures",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0)
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlanProcedures",
                table: "PlanProcedures",
                column: "PlanProcedureId");

            migrationBuilder.CreateTable(
                name: "UserPlanProcedureRelations",
                columns: table => new
                {
                    UserPlanProcedureRelationId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PlanProcedureId = table.Column<int>(type: "INTEGER", nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPlanProcedureRelations", x => x.UserPlanProcedureRelationId);
                    table.ForeignKey(
                        name: "FK_UserPlanProcedureRelations_PlanProcedures_PlanProcedureId",
                        column: x => x.PlanProcedureId,
                        principalTable: "PlanProcedures",
                        principalColumn: "PlanProcedureId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserPlanProcedureRelations_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlanProcedures_PlanId",
                table: "PlanProcedures",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPlanProcedureRelations_PlanProcedureId",
                table: "UserPlanProcedureRelations",
                column: "PlanProcedureId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPlanProcedureRelations_UserId",
                table: "UserPlanProcedureRelations",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserPlanProcedureRelations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlanProcedures",
                table: "PlanProcedures");

            migrationBuilder.DropIndex(
                name: "IX_PlanProcedures_PlanId",
                table: "PlanProcedures");

            migrationBuilder.DropColumn(
                name: "PlanProcedureId",
                table: "PlanProcedures");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlanProcedures",
                table: "PlanProcedures",
                columns: new[] { "PlanId", "ProcedureId" });
        }
    }
}
