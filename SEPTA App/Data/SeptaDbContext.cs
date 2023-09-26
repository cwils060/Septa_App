using System;
using Microsoft.EntityFrameworkCore;
using SEPTA_App.Models;
using static System.Net.WebRequestMethods;

namespace SEPTA_App.Data
{
	public class SeptaDbContext : DbContext
	{

		public DbSet<Station> Stations { get; set; }

        public Station ThirtiethStreet = new Station()
        {
            Id = 1,
            Image = "https://upload.wikimedia.org/wikipedia/commons/thumb/a/ad/30th_Street_Station_east_entrance_from_PA_3_WB.jpeg/300px-30th_Street_Station_east_entrance_from_PA_3_WB.jpeg",
            Name = "30th Street Station",
            Description = "30th Street Station, officially William H. Gray III 30th Street Station, is a major intermodal transit station in Philadelphia, Pennsylvania, United States. It is metropolitan Philadelphia's main railroad station and a major stop on Amtrak's Northeast and Keystone corridors. It was named in memory of U.S. representative William H. Gray III in 2020.The station is also a major commuter rail station served by all SEPTA Regional Rail lines and is the western terminus for NJ Transit's Atlantic City Line. The station is also served by several SEPTA city and suburban buses and by NJ Transit, Amtrak Thruway, and intercity operators."
        };

        public Station AllensLane = new Station()
        {
            Id = 2,
            Image = "https://assets1.cbsnewsstatic.com/i/cbslocal/wp-content/uploads/sites/15116066/2011/05/allen-lane-bridge-mcdev.jpg",
            Name= "Allen Lane",
            Description= "Richard Allen Lane station (formerly Allen Lane station) is a SEPTA Regional Rail station in Philadelphia. It is located at 200 West Allens Lane in the Mount Airy neighborhood and serves the Chestnut Hill West Line. The station building was built circa 1880, according to the Philadelphia Architects and Buildings project. Like many in Philadelphia, it retains much of its Victorian/Edwardian appearance.The former station building now houses a coffee shop, the High Point Cafe."
        };

        public Station Carpenter = new Station()
        {
            Id = 3,
            Image = "https://upload.wikimedia.org/wikipedia/commons/4/44/Carpenter_Lane_SEPTA.JPG",
            Name = "Carpenter Station",
            Description = "Carpenter station is a SEPTA Regional Rail station in Philadelphia, Pennsylvania. Located at 201 Carpenter Lane, it serves the Chestnut Hill West Line.\nThe historic station building has been listed in the Philadelphia Register of Historic Places since August 6, 1981.It is in zone 2 on the Chestnut Hill West Line, on former Pennsylvania Railroad tracks, and is 9.8 track miles from Suburban Station. In fiscal 2012, this station saw 371 boardings on an average weekday."
        };

        public Station Chelten = new Station()
        {
            Id = 4,
            Image = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/f4/Chelten_Avenue_Station.jpg/300px-Chelten_Avenue_Station.jpg",
            Name = "Chelten Avenue station",
            Description = "Chelten Avenue station is a SEPTA Regional Rail station in Philadelphia, Pennsylvania. Located on West Chelten Avenue in the Germantown neighborhood, it serves the Chestnut Hill West Line. The concrete station structure, part of a Pennsylvania Railroad grade-separation project completed in 1918 in conjunction with electrification of the line, was designed by William Holmes Cookman.\n\nA station has been at this location since June 11, 1884. Known initially as Germantown, the 1918 station was named Chelten Avenue to avoid confusion with the Philadelphia & Reading Railroad's Germantown."
        };

        public Station Jefferson = new Station()
        {
            Id = 5,
            Image = "https://upload.wikimedia.org/wikipedia/commons/thumb/a/a4/Train_emerging_from_the_Center_City_Commuter_Connection_at_the_Market_East_Station%2C_Philadelphia_PA.jpg/300px-Train_emerging_from_the_Center_City_Commuter_Connection_at_the_Market_East_Station%2C_Philadelphia_PA.jpg",
            Name = "Market East",
            Description = "Jefferson Station (formerly named Market East Station) is an underground SEPTA Regional Rail station located on Market Street in Philadelphia, Pennsylvania. It is the easternmost of the three Center City stations of the SEPTA Regional Rail system and is part of the Center City Commuter Connection, which connects the former Penn Central commuter lines with the former Reading Company commuter lines. In 2014, the station saw approximately 26,000 passengers every weekday."
        };

        public Station QueenLane = new Station()
        {
            Id = 6,
            Image = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/f1/Queen_Lane_SEPTA.JPG/300px-Queen_Lane_SEPTA.JPG",
            Name = "Queen Lane",
            Description = "Queen Lane station is a SEPTA Regional Rail station in Philadelphia, Pennsylvania. Located at 5319 Wissahickon Avenue facing West Queen Lane, it serves the Chestnut Hill West Line.\n\nThe station is 7.4 miles (11.9 km) from Suburban Station. In 2004, this station saw 470 boardings on an average weekday. It was built for the Philadelphia, Germantown and Chestnut Hill Railroad, a subsidiary of the Pennsylvania Railroad, in 1885 to a design by Washington Bleddyn Powell."
        };



        public SeptaDbContext(DbContextOptions<SeptaDbContext> options) : base(options)
        {


        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Station>().HasData(
                ThirtiethStreet, AllensLane, Carpenter, Chelten, Jefferson, QueenLane);
        }  
    }
}

