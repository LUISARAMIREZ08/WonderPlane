using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WonderPlane.Server.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "WonderPlane");

            migrationBuilder.CreateTable(
                name: "BoardingPass",
                schema: "WonderPlane",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CheckInDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CheckInTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    CheckInStatus = table.Column<int>(type: "int", nullable: false),
                    TicketId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoardingPass", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Flight",
                schema: "WonderPlane",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Origin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Destination = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepartureDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DepartureTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    ArriveDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ArriveTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    FlightStatus = table.Column<int>(type: "int", nullable: false),
                    IsInternational = table.Column<bool>(type: "bit", nullable: false),
                    BagPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    flightCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Duration = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PromotionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flight", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Traveler",
                schema: "WonderPlane",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Document = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegisteredUserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Traveler", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "News",
                schema: "WonderPlane",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FlightId = table.Column<int>(type: "int", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_News", x => x.Id);
                    table.ForeignKey(
                        name: "FK_News_Flight_FlightId",
                        column: x => x.FlightId,
                        principalSchema: "WonderPlane",
                        principalTable: "Flight",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Promotion",
                schema: "WonderPlane",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    PromotionStatus = table.Column<int>(type: "int", nullable: false),
                    FlightId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Promotion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Promotion_Flight_FlightId",
                        column: x => x.FlightId,
                        principalSchema: "WonderPlane",
                        principalTable: "Flight",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Seat",
                schema: "WonderPlane",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<int>(type: "int", nullable: false),
                    SeatType = table.Column<int>(type: "int", nullable: false),
                    SeatStatus = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    FlightId = table.Column<int>(type: "int", nullable: true),
                    TicketId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seat", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Seat_Flight_FlightId",
                        column: x => x.FlightId,
                        principalSchema: "WonderPlane",
                        principalTable: "Flight",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "WonderPlane",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Document = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    Country = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    IsSuscribedToNews = table.Column<bool>(type: "bit", nullable: true),
                    ReciveNotifications = table.Column<bool>(type: "bit", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TravelerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Traveler_TravelerId",
                        column: x => x.TravelerId,
                        principalSchema: "WonderPlane",
                        principalTable: "Traveler",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Card",
                schema: "WonderPlane",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HolderName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CardType = table.Column<int>(type: "int", nullable: false),
                    SecurityCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegisteredUserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Card", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Card_Users_RegisteredUserId",
                        column: x => x.RegisteredUserId,
                        principalSchema: "WonderPlane",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Forums",
                schema: "WonderPlane",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PublicationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Forums", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Forums_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "WonderPlane",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Purchase",
                schema: "WonderPlane",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PurchaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    PurchaseStatus = table.Column<int>(type: "int", nullable: false),
                    RegisteredUserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Purchase", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Purchase_Users_RegisteredUserId",
                        column: x => x.RegisteredUserId,
                        principalSchema: "WonderPlane",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Recommendation",
                schema: "WonderPlane",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecommendationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegisteredUserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recommendation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recommendation_Users_RegisteredUserId",
                        column: x => x.RegisteredUserId,
                        principalSchema: "WonderPlane",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Reservation",
                schema: "WonderPlane",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReservationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaymentLimitDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReservationStatus = table.Column<int>(type: "int", nullable: false),
                    RegisteredUserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservation_Users_RegisteredUserId",
                        column: x => x.RegisteredUserId,
                        principalSchema: "WonderPlane",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Search",
                schema: "WonderPlane",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SearchDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    RegisteredUserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Search", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Search_Users_RegisteredUserId",
                        column: x => x.RegisteredUserId,
                        principalSchema: "WonderPlane",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                schema: "WonderPlane",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ForumId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_Forums_ForumId",
                        column: x => x.ForumId,
                        principalSchema: "WonderPlane",
                        principalTable: "Forums",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Messages_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "WonderPlane",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FlightRecommendation",
                schema: "WonderPlane",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FlightId = table.Column<int>(type: "int", nullable: true),
                    RecommendationId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlightRecommendation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FlightRecommendation_Flight_FlightId",
                        column: x => x.FlightId,
                        principalSchema: "WonderPlane",
                        principalTable: "Flight",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FlightRecommendation_Recommendation_RecommendationId",
                        column: x => x.RecommendationId,
                        principalSchema: "WonderPlane",
                        principalTable: "Recommendation",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Ticket",
                schema: "WonderPlane",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<int>(type: "int", nullable: false),
                    SeatId = table.Column<int>(type: "int", nullable: true),
                    TravelerId = table.Column<int>(type: "int", nullable: true),
                    PurchaseId = table.Column<int>(type: "int", nullable: true),
                    ReservationId = table.Column<int>(type: "int", nullable: true),
                    BoardingPassId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ticket", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ticket_BoardingPass_BoardingPassId",
                        column: x => x.BoardingPassId,
                        principalSchema: "WonderPlane",
                        principalTable: "BoardingPass",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Ticket_Purchase_PurchaseId",
                        column: x => x.PurchaseId,
                        principalSchema: "WonderPlane",
                        principalTable: "Purchase",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Ticket_Reservation_ReservationId",
                        column: x => x.ReservationId,
                        principalSchema: "WonderPlane",
                        principalTable: "Reservation",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Ticket_Seat_SeatId",
                        column: x => x.SeatId,
                        principalSchema: "WonderPlane",
                        principalTable: "Seat",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Ticket_Traveler_TravelerId",
                        column: x => x.TravelerId,
                        principalSchema: "WonderPlane",
                        principalTable: "Traveler",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Card_RegisteredUserId",
                schema: "WonderPlane",
                table: "Card",
                column: "RegisteredUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FlightRecommendation_FlightId",
                schema: "WonderPlane",
                table: "FlightRecommendation",
                column: "FlightId");

            migrationBuilder.CreateIndex(
                name: "IX_FlightRecommendation_RecommendationId",
                schema: "WonderPlane",
                table: "FlightRecommendation",
                column: "RecommendationId");

            migrationBuilder.CreateIndex(
                name: "IX_Forums_UserId",
                schema: "WonderPlane",
                table: "Forums",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ForumId",
                schema: "WonderPlane",
                table: "Messages",
                column: "ForumId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_UserId",
                schema: "WonderPlane",
                table: "Messages",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_News_FlightId",
                schema: "WonderPlane",
                table: "News",
                column: "FlightId");

            migrationBuilder.CreateIndex(
                name: "IX_Promotion_FlightId",
                schema: "WonderPlane",
                table: "Promotion",
                column: "FlightId",
                unique: true,
                filter: "[FlightId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Purchase_RegisteredUserId",
                schema: "WonderPlane",
                table: "Purchase",
                column: "RegisteredUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Recommendation_RegisteredUserId",
                schema: "WonderPlane",
                table: "Recommendation",
                column: "RegisteredUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_RegisteredUserId",
                schema: "WonderPlane",
                table: "Reservation",
                column: "RegisteredUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Search_RegisteredUserId",
                schema: "WonderPlane",
                table: "Search",
                column: "RegisteredUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Seat_FlightId",
                schema: "WonderPlane",
                table: "Seat",
                column: "FlightId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_BoardingPassId",
                schema: "WonderPlane",
                table: "Ticket",
                column: "BoardingPassId",
                unique: true,
                filter: "[BoardingPassId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_PurchaseId",
                schema: "WonderPlane",
                table: "Ticket",
                column: "PurchaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_ReservationId",
                schema: "WonderPlane",
                table: "Ticket",
                column: "ReservationId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_SeatId",
                schema: "WonderPlane",
                table: "Ticket",
                column: "SeatId",
                unique: true,
                filter: "[SeatId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_TravelerId",
                schema: "WonderPlane",
                table: "Ticket",
                column: "TravelerId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_TravelerId",
                schema: "WonderPlane",
                table: "Users",
                column: "TravelerId",
                unique: true,
                filter: "[TravelerId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Card",
                schema: "WonderPlane");

            migrationBuilder.DropTable(
                name: "FlightRecommendation",
                schema: "WonderPlane");

            migrationBuilder.DropTable(
                name: "Messages",
                schema: "WonderPlane");

            migrationBuilder.DropTable(
                name: "News",
                schema: "WonderPlane");

            migrationBuilder.DropTable(
                name: "Promotion",
                schema: "WonderPlane");

            migrationBuilder.DropTable(
                name: "Search",
                schema: "WonderPlane");

            migrationBuilder.DropTable(
                name: "Ticket",
                schema: "WonderPlane");

            migrationBuilder.DropTable(
                name: "Recommendation",
                schema: "WonderPlane");

            migrationBuilder.DropTable(
                name: "Forums",
                schema: "WonderPlane");

            migrationBuilder.DropTable(
                name: "BoardingPass",
                schema: "WonderPlane");

            migrationBuilder.DropTable(
                name: "Purchase",
                schema: "WonderPlane");

            migrationBuilder.DropTable(
                name: "Reservation",
                schema: "WonderPlane");

            migrationBuilder.DropTable(
                name: "Seat",
                schema: "WonderPlane");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "WonderPlane");

            migrationBuilder.DropTable(
                name: "Flight",
                schema: "WonderPlane");

            migrationBuilder.DropTable(
                name: "Traveler",
                schema: "WonderPlane");
        }
    }
}
