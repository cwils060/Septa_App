using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SEPTA_App.Migrations
{
    /// <inheritdoc />
    public partial class Seed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Stations",
                columns: new[] { "Id", "Description", "Image", "Name" },
                values: new object[,]
                {
                    { 1, "30th Street Station, officially William H. Gray III 30th Street Station, is a major intermodal transit station in Philadelphia, Pennsylvania, United States. It is metropolitan Philadelphia's main railroad station and a major stop on Amtrak's Northeast and Keystone corridors. It was named in memory of U.S. representative William H. Gray III in 2020.The station is also a major commuter rail station served by all SEPTA Regional Rail lines and is the western terminus for NJ Transit's Atlantic City Line. The station is also served by several SEPTA city and suburban buses and by NJ Transit, Amtrak Thruway, and intercity operators.", "https://upload.wikimedia.org/wikipedia/commons/thumb/a/ad/30th_Street_Station_east_entrance_from_PA_3_WB.jpeg/300px-30th_Street_Station_east_entrance_from_PA_3_WB.jpeg", "30th Street Station" },
                    { 2, "Richard Allen Lane station (formerly Allen Lane station) is a SEPTA Regional Rail station in Philadelphia. It is located at 200 West Allens Lane in the Mount Airy neighborhood and serves the Chestnut Hill West Line. The station building was built circa 1880, according to the Philadelphia Architects and Buildings project. Like many in Philadelphia, it retains much of its Victorian/Edwardian appearance.The former station building now houses a coffee shop, the High Point Cafe.", "https://assets1.cbsnewsstatic.com/i/cbslocal/wp-content/uploads/sites/15116066/2011/05/allen-lane-bridge-mcdev.jpg", "Allen Lane" },
                    { 3, "Carpenter station is a SEPTA Regional Rail station in Philadelphia, Pennsylvania. Located at 201 Carpenter Lane, it serves the Chestnut Hill West Line.\nThe historic station building has been listed in the Philadelphia Register of Historic Places since August 6, 1981.It is in zone 2 on the Chestnut Hill West Line, on former Pennsylvania Railroad tracks, and is 9.8 track miles from Suburban Station. In fiscal 2012, this station saw 371 boardings on an average weekday.", "https://upload.wikimedia.org/wikipedia/commons/4/44/Carpenter_Lane_SEPTA.JPG", "Carpenter Station" },
                    { 4, "Chelten Avenue station is a SEPTA Regional Rail station in Philadelphia, Pennsylvania. Located on West Chelten Avenue in the Germantown neighborhood, it serves the Chestnut Hill West Line. The concrete station structure, part of a Pennsylvania Railroad grade-separation project completed in 1918 in conjunction with electrification of the line, was designed by William Holmes Cookman.\n\nA station has been at this location since June 11, 1884. Known initially as Germantown, the 1918 station was named Chelten Avenue to avoid confusion with the Philadelphia & Reading Railroad's Germantown.", "https://upload.wikimedia.org/wikipedia/commons/thumb/f/f4/Chelten_Avenue_Station.jpg/300px-Chelten_Avenue_Station.jpg", "Chelten Avenue station" },
                    { 5, "Jefferson Station (formerly named Market East Station) is an underground SEPTA Regional Rail station located on Market Street in Philadelphia, Pennsylvania. It is the easternmost of the three Center City stations of the SEPTA Regional Rail system and is part of the Center City Commuter Connection, which connects the former Penn Central commuter lines with the former Reading Company commuter lines. In 2014, the station saw approximately 26,000 passengers every weekday.", "https://upload.wikimedia.org/wikipedia/commons/thumb/a/a4/Train_emerging_from_the_Center_City_Commuter_Connection_at_the_Market_East_Station%2C_Philadelphia_PA.jpg/300px-Train_emerging_from_the_Center_City_Commuter_Connection_at_the_Market_East_Station%2C_Philadelphia_PA.jpg", "Market East" },
                    { 6, "Queen Lane station is a SEPTA Regional Rail station in Philadelphia, Pennsylvania. Located at 5319 Wissahickon Avenue facing West Queen Lane, it serves the Chestnut Hill West Line.\n\nThe station is 7.4 miles (11.9 km) from Suburban Station. In 2004, this station saw 470 boardings on an average weekday. It was built for the Philadelphia, Germantown and Chestnut Hill Railroad, a subsidiary of the Pennsylvania Railroad, in 1885 to a design by Washington Bleddyn Powell.", "https://upload.wikimedia.org/wikipedia/commons/thumb/f/f1/Queen_Lane_SEPTA.JPG/300px-Queen_Lane_SEPTA.JPG", "Queen Lane" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 6);
        }
    }
}
