using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Biblioteca.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AjusteRelacionamentoLeitorEmprestimos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Emprestimos_Leitores_LeitorId1",
                table: "Emprestimos");

            migrationBuilder.DropIndex(
                name: "IX_Emprestimos_LeitorId1",
                table: "Emprestimos");

            migrationBuilder.DropColumn(
                name: "LeitorId1",
                table: "Emprestimos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "LeitorId1",
                table: "Emprestimos",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Emprestimos_LeitorId1",
                table: "Emprestimos",
                column: "LeitorId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Emprestimos_Leitores_LeitorId1",
                table: "Emprestimos",
                column: "LeitorId1",
                principalTable: "Leitores",
                principalColumn: "Id");
        }
    }
}
