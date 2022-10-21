using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Courses.Data.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Instructors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    Thumbnail = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instructors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Videos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    Thumbnail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    CourseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Videos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Videos_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseInstructor",
                columns: table => new
                {
                    CoursesId = table.Column<int>(type: "int", nullable: false),
                    InstructorsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseInstructor", x => new { x.CoursesId, x.InstructorsId });
                    table.ForeignKey(
                        name: "FK_CourseInstructor_Courses_CoursesId",
                        column: x => x.CoursesId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseInstructor_Instructors_InstructorsId",
                        column: x => x.InstructorsId,
                        principalTable: "Instructors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseInstructors",
                columns: table => new
                {
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    InstructorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseInstructors", x => new { x.CourseId, x.InstructorId });
                    table.ForeignKey(
                        name: "FK_CourseInstructors_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseInstructors_Instructors_InstructorId",
                        column: x => x.InstructorId,
                        principalTable: "Instructors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "Description", "Title" },
                values: new object[,]
                {
                    { 1, "Course 1 description.", "Course 1" },
                    { 2, "Course 2 description.", "Course 2" },
                    { 3, "Course 3 description.", "Course 3" }
                });

            migrationBuilder.InsertData(
                table: "Instructors",
                columns: new[] { "Id", "Description", "Name", "Thumbnail" },
                values: new object[,]
                {
                    { 1, "John Doe's description.", "John Doe", "/images/john.png" },
                    { 2, "Jane Doe's description.", "Jane Doe", "/images/jane.png" }
                });

            migrationBuilder.InsertData(
                table: "CourseInstructors",
                columns: new[] { "CourseId", "InstructorId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 2, 2 },
                    { 3, 1 }
                });

            migrationBuilder.InsertData(
                table: "Videos",
                columns: new[] { "Id", "CourseId", "Description", "Thumbnail", "Title", "Url" },
                values: new object[,]
                {
                    { 1, 1, "Video 1 description", "Thumbnail 1 Url", "Video 1 Title", "https://www.youtube.com/embed/BJFyzpBcaCY" },
                    { 2, 1, "Video 2 description", "Thumbnail 2 Url", "Video 2 Title", "https://www.youtube.com/embed/BJFyzpBcaCY" },
                    { 3, 2, "Video 3 description", "Thumbnail 3 Url", "Video 3 Title", "https://www.youtube.com/embed/BJFyzpBcaCY" },
                    { 4, 3, "Video 4 description", "Thumbnail 4 Url", "Video 4 Title", "https://www.youtube.com/embed/BJFyzpBcaCY" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseInstructor_InstructorsId",
                table: "CourseInstructor",
                column: "InstructorsId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseInstructors_InstructorId",
                table: "CourseInstructors",
                column: "InstructorId");

            migrationBuilder.CreateIndex(
                name: "IX_Videos_CourseId",
                table: "Videos",
                column: "CourseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseInstructor");

            migrationBuilder.DropTable(
                name: "CourseInstructors");

            migrationBuilder.DropTable(
                name: "Videos");

            migrationBuilder.DropTable(
                name: "Instructors");

            migrationBuilder.DropTable(
                name: "Courses");
        }
    }
}
