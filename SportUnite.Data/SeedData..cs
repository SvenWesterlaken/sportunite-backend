using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SportUnite.Domain;

namespace SportUnite.Data
{
    public static class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            {
                ApplicationDbContext context = app.ApplicationServices
                    .GetRequiredService<ApplicationDbContext>();


                if (context.Sports.Any())
                {
                    return;
                }

                var sports = new Sport[]
                {
                    new Sport
                    {
                        Name = "Voetbal"
                    },
                    new Sport
                    {
                        Name = "Basketbal"
                    },
                    new Sport
                    {
                        Name = "Trefbal"
                    },
                    new Sport
                    {
                        Name = "Hockey"
                    },
                    new Sport
                    {
                        Name = "Fitness"
                    },
                    new Sport
                    {
                        Name = "Volleyball"
                    },
                    new Sport
                    {
                        Name = "Badminton"
                    }
                };

                foreach (Sport s in sports)
                {
                    context.Sports.Add(s);
                }

                context.SaveChanges();

                var buildings = new Building[]
                {
                    new Building
                    {
                        Name = "Fitness Group"

                    },
                    new Building
                    {
                        Name = "Sport For Free"

                    },
                    new Building
                    {
                        Name = "Sport Fit"

                    }
                };


                foreach (Building b in buildings)
                {
                    context.Buildings.Add(b);
                }

                context.SaveChanges();

                var addresses = new Address[]
                {

                    new Address
                    {
                        City = "Breda",
                        HouseNumber = 51,
                        ZipCode = "4815AA",
                        StreetName = "Sportstraat",
                        Country = "Nederland",
                        State = "Noord-Brabant",
                        BuildingId = buildings.Single(b => b.Name == "Fitness Group").BuildingId
                    },
                    new Address
                    {
                        City = "Breda",
                        HouseNumber = 101,
                        ZipCode = "4819BB",
                        StreetName = "Van Voorstelstraat",
                        Country = "Nederland",
                        State = "Noord-Brabant",
                        BuildingId = buildings.Single(b => b.Name == "Sport For Free").BuildingId
                    },
                    new Address
                    {
                        City = "Breda",
                        HouseNumber = 33,
                        ZipCode = "4814CC",
                        StreetName = "Dijkweg",
                        Country = "Nederland",
                        State = "Noord-Brabant",
                        BuildingId = buildings.Single(b => b.Name == "Sport Fit").BuildingId
                    }
                };

                foreach (Address a in addresses)
                {
                    context.Addresses.Add(a);
                }

                context.SaveChanges();


                var halls = new Hall[]
                {
                    new Hall
                    {
                        Name = "Hall 1",
                        Price = 10,
                        Size = "Groot",
                        BuildingId = buildings.Single(b => b.Name == "Sport Fit").BuildingId,
                        Available = true
                    },
                    new Hall
                    {
                        Name = "Hall 2",
                        Price = 20,
                        Size = "Klein",
                        BuildingId = buildings.Single(b => b.Name == "Fitness Group").BuildingId,
                        Available = false
                    },
                    new Hall
                    {
                        Name = "Hall 3",
                        Price = 50,
                        Size = "Klein",
                        BuildingId = buildings.Single(b => b.Name == "Sport Fit").BuildingId,
                        Available = true
                    },
                    new Hall
                    {
                        Name = "Hall 4",
                        Price = 40,
                        Size = "Groot",
                        BuildingId = buildings.Single(b => b.Name == "Sport For Free").BuildingId,
                        Available = false
                    },
                    new Hall
                    {
                        Name = "Hall 5",
                        Price = 100,
                        Size = "Middel",
                        BuildingId = buildings.Single(b => b.Name == "Sport Fit").BuildingId,
                        Available = true
                    },
                    new Hall
                    {
                        Name = "Hall 6",
                        Price = 40,
                        Size = "Middel",
                        BuildingId = buildings.Single(b => b.Name == "Fitness Group").BuildingId,
                        Available = true
                    }

                };

                foreach (var h in halls)
                {
                    context.Halls.Add(h);
                }

                context.SaveChanges();

                var sportHalls = new SportHall[]
                {

                    new SportHall
                    {
                        SportId = sports.Single(s => s.Name == "Voetbal").SportId,
                        HallId = halls.Single(h => h.Name == "Hall 1").HallId

                    },

                    new SportHall
                    {
                        SportId = sports.Single(s => s.Name == "Basketbal").SportId,
                        HallId = halls.Single(h => h.Name == "Hall 2").HallId

                    },

                    new SportHall
                    {
                        SportId = sports.Single(s => s.Name == "Volleyball").SportId,
                        HallId = halls.Single(h => h.Name == "Hall 4").HallId

                    },
                    new SportHall
                    {
                        SportId = sports.Single(s => s.Name == "Hockey").SportId,
                        HallId = halls.Single(h => h.Name == "Hall 5").HallId

                    },
                    new SportHall
                    {
                        SportId = sports.Single(s => s.Name == "Volleyball").SportId,
                        HallId = halls.Single(h => h.Name == "Hall 6").HallId

                    }
                };

                foreach (var h in sportHalls)
                {
                    context.SportHalls.Add(h);
                }

                context.SaveChanges();

                var openingHours = new OpeningHours[]
                {
                    new OpeningHours
                    {
                        Day = "Maandag",
                        ClosingTime = "18",
                        OpeningTime = "10"
                    },
                    new OpeningHours
                    {
                        Day = "Dinsdag",
                        ClosingTime = "18",
                        OpeningTime = "8"
                    },
                    new OpeningHours
                    {
                        Day = "Woensdag",
                        ClosingTime = "20",
                        OpeningTime = "8"
                    },
                    new OpeningHours
                    {
                        Day = "Donderdag",
                        ClosingTime = "22",
                        OpeningTime = "10"
                    },
                    new OpeningHours
                    {
                        Day = "Vrijdag",
                        ClosingTime = "22",
                        OpeningTime = "10" 
                    },
                    new OpeningHours
                    {
                        Day = "Zaterdag",
                        ClosingTime = "20",
                        OpeningTime = "11"
                    },
                    new OpeningHours
                    {
                        Day = "Zondag",
                        ClosingTime = "18",
                        OpeningTime = "12"
                    },
                    new OpeningHours
                    {
                        Day = "Maandag2",
                        ClosingTime = "16",
                        OpeningTime = "8"
                    },
                    new OpeningHours
                    {
                        Day = "Maandag3",
                        ClosingTime = "16",
                        OpeningTime = "9"
                    }
                };

                foreach (var openingHour in openingHours)
                {
                    context.OpeningHours.Add(openingHour);
                }

                context.SaveChanges();

           

                var hallOpeningHours = new HallOpeningHours[]
                {
                    new HallOpeningHours
                    {
                        HallId = halls.Single(h => h.Name == "Hall 1").HallId,
                        OpeningHoursId = openingHours.Single(o => o.Day == "Maandag").OpeningHoursId
                    },
                    new HallOpeningHours
                    {
                        HallId = halls.Single(h => h.Name == "Hall 1").HallId,
                        OpeningHoursId = openingHours.Single(o => o.Day == "Dinsdag").OpeningHoursId
                    },
                    new HallOpeningHours
                    {
                        HallId = halls.Single(h => h.Name == "Hall 1").HallId,
                        OpeningHoursId = openingHours.Single(o => o.Day == "Woensdag").OpeningHoursId
                    },
                    new HallOpeningHours
                    {
                        HallId = halls.Single(h => h.Name == "Hall 1").HallId,
                        OpeningHoursId = openingHours.Single(o => o.Day == "Donderdag").OpeningHoursId
                    },
                    new HallOpeningHours
                    {
                        HallId = halls.Single(h => h.Name == "Hall 1").HallId,
                        OpeningHoursId = openingHours.Single(o => o.Day == "Vrijdag").OpeningHoursId
                    },
                    new HallOpeningHours
                    {
                        HallId = halls.Single(h => h.Name == "Hall 1").HallId,
                        OpeningHoursId = openingHours.Single(o => o.Day == "Zaterdag").OpeningHoursId
                    },
                    new HallOpeningHours
                    {
                        HallId = halls.Single(h => h.Name == "Hall 1").HallId,
                        OpeningHoursId = openingHours.Single(o => o.Day == "Zondag").OpeningHoursId
                    }

                    //TODO add some more openinghours foreach hall
                };

                foreach (HallOpeningHours h in hallOpeningHours)
                {
                    context.HallOpeningHours.Add(h);
                }

                context.SaveChanges();
                

                var sportObjects = new SportObject[]
                {
                    new SportObject {Name = "Voetbal"},
                    new SportObject {Name = "Volleybal"},
                    new SportObject {Name = "Hockeystick"},
                    new SportObject {Name = "Basketbal"},
                    new SportObject {Name = "Goal"},
                    new SportObject {Name = "Springtouw"},
                    new SportObject {Name = "Zachte bal"},
                    new SportObject {Name = "Badminton racket"},
                    new SportObject {Name = "Badminton net"},
                    new SportObject {Name = "Bench press"},
                    new SportObject {Name = "Gewichten"},
                    new SportObject {Name = "Mat"},
                    new SportObject {Name = "Volleybal net"},
                    new SportObject {Name = "Basketbal net"}

                    //TODO new sports, new items 

                };

                foreach (SportObject so in sportObjects)
                {
                    context.SportObjects.Add(so);
                }

                context.SaveChanges();

                var sportObjectSport = new SportObjectSport[]
                {
                    new SportObjectSport
                    {
                        SportId = sports.Single(s => s.Name == "Voetbal").SportId,
                        SportObjectId = sportObjects.Single(so => so.Name == "Voetbal").SportObjectId
                    },
                    new SportObjectSport
                    {
                        SportObjectId = sportObjects.Single(so => so.Name == "Mat").SportObjectId,
                        SportId = sports.Single(s => s.Name == "Fitness").SportId
                    },
                    new SportObjectSport
                    {
                        SportObjectId = sportObjects.Single(so => so.Name == "Gewichten").SportObjectId,
                        SportId = sports.Single(s => s.Name == "Fitness").SportId
                    },
                    new SportObjectSport
                    {
                        SportObjectId = sportObjects.Single(so => so.Name == "Bench press").SportObjectId,
                        SportId = sports.Single(s => s.Name == "Fitness").SportId
                    },
                    new SportObjectSport
                    {
                        SportObjectId = sportObjects.Single(so => so.Name == "Badminton net").SportObjectId,
                        SportId = sports.Single(s => s.Name == "Badminton").SportId
                    },
                    new SportObjectSport
                    {
                        SportObjectId = sportObjects.Single(so => so.Name == "Badminton racket").SportObjectId,
                        SportId = sports.Single(s => s.Name == "Badminton").SportId
                    },
                    new SportObjectSport
                    {
                        SportObjectId = sportObjects.Single(so => so.Name == "Zachte bal").SportObjectId,
                        SportId = sports.Single(s => s.Name == "Trefbal").SportId
                    },
                    new SportObjectSport
                    {
                        SportObjectId = sportObjects.Single(so => so.Name == "Springtouw").SportObjectId,
                        SportId = sports.Single(s => s.Name == "Fitness").SportId
                    },
                    new SportObjectSport
                    {
                        SportObjectId = sportObjects.Single(so => so.Name == "Goal").SportObjectId,
                        SportId = sports.Single(s => s.Name == "Voetbal").SportId
                    },
                    new SportObjectSport
                    {
                        SportObjectId = sportObjects.Single(so => so.Name == "Basketbal").SportObjectId,
                        SportId = sports.Single(s => s.Name == "Basketbal").SportId
                    },
                    new SportObjectSport
                    {
                        SportObjectId = sportObjects.Single(so => so.Name == "Hockeystick").SportObjectId,
                        SportId = sports.Single(s => s.Name == "Hockey").SportId
                    },
                    new SportObjectSport
                    {
                        SportObjectId = sportObjects.Single(so => so.Name == "Goal").SportObjectId,
                        SportId = sports.Single(s => s.Name == "Hockey").SportId
                    },
                    new SportObjectSport
                    {
                        SportObjectId = sportObjects.Single(so => so.Name == "Volleybal").SportObjectId,
                        SportId = sports.Single(s => s.Name == "Volleyball").SportId
                    },
                    new SportObjectSport
                    {
                        SportObjectId = sportObjects.Single(so => so.Name == "Basketbal net").SportObjectId,
                        SportId = sports.Single(s => s.Name == "Basketbal").SportId
                    },
                    new SportObjectSport
                    {
                        SportObjectId = sportObjects.Single(so => so.Name == "Volleybal net").SportObjectId,
                        SportId = sports.Single(s => s.Name == "Volleyball").SportId
                    }


                };

                foreach (SportObjectSport sop in sportObjectSport)
                {
                    context.SportObjectSports.Add(sop);
                }

                context.SaveChanges();



                var sportObjectHall = new SportObjectHall[]
                {
                    new SportObjectHall
                    {
                        HallId = halls.Single(s => s.Name == "Hall 1").HallId,
                        SportObjectId = sportObjects.Single(so => so.Name == "Voetbal").SportObjectId
                    },
                    new SportObjectHall
                    {

                        SportObjectId = sportObjects.Single(so => so.Name == "Mat").SportObjectId,
                        HallId = halls.Single(s => s.Name == "Hall 3").HallId
                    },
                    new SportObjectHall
                    {
                        SportObjectId = sportObjects.Single(so => so.Name == "Gewichten").SportObjectId,
                        HallId = halls.Single(s => s.Name == "Hall 3").HallId
                    },
                    new SportObjectHall
                    {
                        SportObjectId = sportObjects.Single(so => so.Name == "Bench press").SportObjectId,
                        HallId = halls.Single(s => s.Name == "Hall 3").HallId
                    },
                    new SportObjectHall
                    {
                        SportObjectId = sportObjects.Single(so => so.Name == "Badminton net").SportObjectId,
                        HallId = halls.Single(s => s.Name == "Hall 6").HallId
                    },
                    new SportObjectHall
                    {
                        SportObjectId = sportObjects.Single(so => so.Name == "Badminton racket").SportObjectId,
                        HallId = halls.Single(s => s.Name == "Hall 6").HallId
                    },
                    new SportObjectHall
                    {
                        SportObjectId = sportObjects.Single(so => so.Name == "Zachte bal").SportObjectId,
                        HallId = halls.Single(s => s.Name == "Hall 1").HallId
                    },
                    new SportObjectHall
                    {
                        SportObjectId = sportObjects.Single(so => so.Name == "Springtouw").SportObjectId,
                        HallId = halls.Single(s => s.Name == "Hall 3").HallId
                    },
                    new SportObjectHall
                    {
                        SportObjectId = sportObjects.Single(so => so.Name == "Goal").SportObjectId,
                        HallId = halls.Single(s => s.Name == "Hall 1").HallId
                    },
                    new SportObjectHall
                    {
                        SportObjectId = sportObjects.Single(so => so.Name == "Basketbal").SportObjectId,
                        HallId = halls.Single(s => s.Name == "Hall 2").HallId
                    },
                    new SportObjectHall
                    {
                        SportObjectId = sportObjects.Single(so => so.Name == "Hockeystick").SportObjectId,
                        HallId = halls.Single(s => s.Name == "Hall 5").HallId
                    },
                    new SportObjectHall
                    {
                        SportObjectId = sportObjects.Single(so => so.Name == "Goal").SportObjectId,
                        HallId = halls.Single(s => s.Name == "Hall 5").HallId
                    },
                    new SportObjectHall
                    {
                        SportObjectId = sportObjects.Single(so => so.Name == "Volleybal").SportObjectId,
                        HallId = halls.Single(s => s.Name == "Hall 4").HallId
                    },
                    new SportObjectHall
                    {
                        SportObjectId = sportObjects.Single(so => so.Name == "Basketbal net").SportObjectId,
                        HallId = halls.Single(s => s.Name == "Hall 2").HallId
                    },
                    new SportObjectHall
                    {
                        SportObjectId = sportObjects.Single(so => so.Name == "Volleybal net").SportObjectId,
                        HallId = halls.Single(s => s.Name == "Hall 4").HallId
                    },
                    new SportObjectHall
                    {
                        SportObjectId = sportObjects.Single(so => so.Name == "Volleybal net").SportObjectId,
                        HallId = halls.Single(s => s.Name == "Hall 6").HallId
                    },
                    new SportObjectHall
                    {
                        SportObjectId = sportObjects.Single(so => so.Name == "Volleybal").SportObjectId,
                        HallId = halls.Single(s => s.Name == "Hall 6").HallId
                    }



                };

                foreach (SportObjectHall soh in sportObjectHall)
                {
                    context.SportObjectHalls.Add(soh);
                }

                context.SaveChanges();
            }

        }
    }
}

