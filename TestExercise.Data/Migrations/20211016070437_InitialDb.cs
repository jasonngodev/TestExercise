using Microsoft.EntityFrameworkCore.Migrations;

namespace TestExercise.Data.Migrations
{
    public partial class InitialDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BeautyNumbers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Numbers = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BeautyNumbers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Last2NumCases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    chars = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Last2NumCases", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MatchConditions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rule = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Conditions = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchConditions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Operators",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProviderName = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operators", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FengShuiNumbers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhoneNumber = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    LastNum = table.Column<string>(type: "varchar(2)", unicode: false, maxLength: 2, nullable: true),
                    OperatorID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FengShuiNumbers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FengShuiNumbers_Operators_OperatorID",
                        column: x => x.OperatorID,
                        principalTable: "Operators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrefixNumbers",
                columns: table => new
                {
                    PrefixId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OperatorId = table.Column<int>(type: "int", nullable: false),
                    PrefixNumber = table.Column<string>(type: "varchar(3)", unicode: false, maxLength: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrefixNumbers", x => x.PrefixId);
                    table.ForeignKey(
                        name: "FK_PrefixNumbers_Operators_OperatorId",
                        column: x => x.OperatorId,
                        principalTable: "Operators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "BeautyNumbers",
                columns: new[] { "Id", "Numbers" },
                values: new object[] { 1, "19, 24, 26, 37, 34" });

            migrationBuilder.InsertData(
                table: "Last2NumCases",
                columns: new[] { "Id", "chars" },
                values: new object[,]
                {
                    { 1, "00,66" },
                    { 2, "04, 45, 85, 27, 67" },
                    { 3, "17, 57, 97, 98, 58" },
                    { 4, "42, 82" },
                    { 5, "69" }
                });

            migrationBuilder.InsertData(
                table: "MatchConditions",
                columns: new[] { "Id", "Conditions", "Rule" },
                values: new object[,]
                {
                    { 1, "24/29", null },
                    { 2, "24/28", null }
                });

            migrationBuilder.InsertData(
                table: "Operators",
                columns: new[] { "Id", "ProviderName" },
                values: new object[,]
                {
                    { 1, "MobiFone" },
                    { 2, "Vinaphone" },
                    { 3, "Viettel" }
                });

            migrationBuilder.InsertData(
                table: "PrefixNumbers",
                columns: new[] { "PrefixId", "OperatorId", "PrefixNumber" },
                values: new object[,]
                {
                    { 1, 1, "089" },
                    { 2, 1, "090" },
                    { 3, 1, "093" },
                    { 4, 2, "088" },
                    { 5, 2, "091" },
                    { 6, 2, "094" },
                    { 7, 3, "086" },
                    { 8, 3, "096" },
                    { 9, 3, "097" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_FengShuiNumbers_OperatorID",
                table: "FengShuiNumbers",
                column: "OperatorID");

            migrationBuilder.CreateIndex(
                name: "IX_PrefixNumbers_OperatorId",
                table: "PrefixNumbers",
                column: "OperatorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BeautyNumbers");

            migrationBuilder.DropTable(
                name: "FengShuiNumbers");

            migrationBuilder.DropTable(
                name: "Last2NumCases");

            migrationBuilder.DropTable(
                name: "MatchConditions");

            migrationBuilder.DropTable(
                name: "PrefixNumbers");

            migrationBuilder.DropTable(
                name: "Operators");
        }
    }
}
