using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WonderPlane.Server.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "WonderPlane");

            migrationBuilder.CreateTable(
                name: "BoardingPasses",
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
                    table.PrimaryKey("PK_BoardingPasses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Flights",
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
                    BagPrice = table.Column<int>(type: "int", nullable: false),
                    FlightCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    FirstClassPrice = table.Column<int>(type: "int", nullable: false),
                    EconomicClassPrice = table.Column<int>(type: "int", nullable: false),
                    AvailableSeats = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flights", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Travelers",
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
                    table.PrimaryKey("PK_Travelers", x => x.Id);
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
                        name: "FK_News_Flights_FlightId",
                        column: x => x.FlightId,
                        principalSchema: "WonderPlane",
                        principalTable: "Flights",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Promotions",
                schema: "WonderPlane",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PromotionStatus = table.Column<int>(type: "int", nullable: false),
                    PromotionType = table.Column<int>(type: "int", nullable: false),
                    FlightId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Promotions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Promotions_Flights_FlightId",
                        column: x => x.FlightId,
                        principalSchema: "WonderPlane",
                        principalTable: "Flights",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Seats",
                schema: "WonderPlane",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<int>(type: "int", nullable: false),
                    SeatType = table.Column<int>(type: "int", nullable: false),
                    SeatStatus = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FlightId = table.Column<int>(type: "int", nullable: true),
                    TicketId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Seats_Flights_FlightId",
                        column: x => x.FlightId,
                        principalSchema: "WonderPlane",
                        principalTable: "Flights",
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
                    Balance = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TravelerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Travelers_TravelerId",
                        column: x => x.TravelerId,
                        principalSchema: "WonderPlane",
                        principalTable: "Travelers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Cards",
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
                    Balance = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    RegisteredUserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cards_Users_RegisteredUserId",
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
                name: "Purchases",
                schema: "WonderPlane",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PurchaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PurchaseStatus = table.Column<int>(type: "int", nullable: false),
                    RegisteredUserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Purchases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Purchases_Users_RegisteredUserId",
                        column: x => x.RegisteredUserId,
                        principalSchema: "WonderPlane",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Recommendations",
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
                    table.PrimaryKey("PK_Recommendations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recommendations_Users_RegisteredUserId",
                        column: x => x.RegisteredUserId,
                        principalSchema: "WonderPlane",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
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
                    table.PrimaryKey("PK_Reservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservations_Users_RegisteredUserId",
                        column: x => x.RegisteredUserId,
                        principalSchema: "WonderPlane",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Searches",
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
                    table.PrimaryKey("PK_Searches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Searches_Users_RegisteredUserId",
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
                name: "FlightRecommendations",
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
                    table.PrimaryKey("PK_FlightRecommendations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FlightRecommendations_Flights_FlightId",
                        column: x => x.FlightId,
                        principalSchema: "WonderPlane",
                        principalTable: "Flights",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FlightRecommendations_Recommendations_RecommendationId",
                        column: x => x.RecommendationId,
                        principalSchema: "WonderPlane",
                        principalTable: "Recommendations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
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
                    table.PrimaryKey("PK_Tickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tickets_BoardingPasses_BoardingPassId",
                        column: x => x.BoardingPassId,
                        principalSchema: "WonderPlane",
                        principalTable: "BoardingPasses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tickets_Purchases_PurchaseId",
                        column: x => x.PurchaseId,
                        principalSchema: "WonderPlane",
                        principalTable: "Purchases",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tickets_Reservations_ReservationId",
                        column: x => x.ReservationId,
                        principalSchema: "WonderPlane",
                        principalTable: "Reservations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tickets_Seats_SeatId",
                        column: x => x.SeatId,
                        principalSchema: "WonderPlane",
                        principalTable: "Seats",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tickets_Travelers_TravelerId",
                        column: x => x.TravelerId,
                        principalSchema: "WonderPlane",
                        principalTable: "Travelers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cards_RegisteredUserId",
                schema: "WonderPlane",
                table: "Cards",
                column: "RegisteredUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FlightRecommendations_FlightId",
                schema: "WonderPlane",
                table: "FlightRecommendations",
                column: "FlightId");

            migrationBuilder.CreateIndex(
                name: "IX_FlightRecommendations_RecommendationId",
                schema: "WonderPlane",
                table: "FlightRecommendations",
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
                name: "IX_Promotions_FlightId",
                schema: "WonderPlane",
                table: "Promotions",
                column: "FlightId");

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_RegisteredUserId",
                schema: "WonderPlane",
                table: "Purchases",
                column: "RegisteredUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Recommendations_RegisteredUserId",
                schema: "WonderPlane",
                table: "Recommendations",
                column: "RegisteredUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_RegisteredUserId",
                schema: "WonderPlane",
                table: "Reservations",
                column: "RegisteredUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Searches_RegisteredUserId",
                schema: "WonderPlane",
                table: "Searches",
                column: "RegisteredUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Seats_FlightId",
                schema: "WonderPlane",
                table: "Seats",
                column: "FlightId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_BoardingPassId",
                schema: "WonderPlane",
                table: "Tickets",
                column: "BoardingPassId",
                unique: true,
                filter: "[BoardingPassId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_PurchaseId",
                schema: "WonderPlane",
                table: "Tickets",
                column: "PurchaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_ReservationId",
                schema: "WonderPlane",
                table: "Tickets",
                column: "ReservationId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_SeatId",
                schema: "WonderPlane",
                table: "Tickets",
                column: "SeatId",
                unique: true,
                filter: "[SeatId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_TravelerId",
                schema: "WonderPlane",
                table: "Tickets",
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
                name: "Cards",
                schema: "WonderPlane");

            migrationBuilder.DropTable(
                name: "FlightRecommendations",
                schema: "WonderPlane");

            migrationBuilder.DropTable(
                name: "Messages",
                schema: "WonderPlane");

            migrationBuilder.DropTable(
                name: "News",
                schema: "WonderPlane");

            migrationBuilder.DropTable(
                name: "Promotions",
                schema: "WonderPlane");

            migrationBuilder.DropTable(
                name: "Searches",
                schema: "WonderPlane");

            migrationBuilder.DropTable(
                name: "Tickets",
                schema: "WonderPlane");

            migrationBuilder.DropTable(
                name: "Recommendations",
                schema: "WonderPlane");

            migrationBuilder.DropTable(
                name: "Forums",
                schema: "WonderPlane");

            migrationBuilder.DropTable(
                name: "BoardingPasses",
                schema: "WonderPlane");

            migrationBuilder.DropTable(
                name: "Purchases",
                schema: "WonderPlane");

            migrationBuilder.DropTable(
                name: "Reservations",
                schema: "WonderPlane");

            migrationBuilder.DropTable(
                name: "Seats",
                schema: "WonderPlane");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "WonderPlane");

            migrationBuilder.DropTable(
                name: "Flights",
                schema: "WonderPlane");

            migrationBuilder.DropTable(
                name: "Travelers",
                schema: "WonderPlane");
        }
    }
}
