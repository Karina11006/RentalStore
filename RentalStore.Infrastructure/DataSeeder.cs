using RentalStore.Domain.Models;
using RentalStore.SharedKernel.Dto;
using Condition = RentalStore.Domain.Models.Condition;

namespace RentalStore.Infrastructure
{
    public class DataSeeder
    {
        private readonly RentalStoreDbContext _dbContext;

        public DataSeeder(RentalStoreDbContext context)
        {
            this._dbContext = context;
        }

        public void Seed()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Database.EnsureCreated();

            if (_dbContext.Database.CanConnect())
            {
                if (!_dbContext.Categories.Any())
                {
                    var categories = new List<Category>
                {
                    new Category { CategoryName = "Narty zjazdowe", Description = "Sprzęt narciarski", ImageUrl = "https://localhost:7269/images/default_skis.png" },
                    new Category { CategoryName = "Rower", Description = "Rower górski", ImageUrl = "https://localhost:7269/images/default_bicycle.png" },
                    new Category { CategoryName = "Narty biegowe", Description = "Sprzęt do narciarstwa biegowego", ImageUrl = "https://localhost:7269/images/run_skis.png" },
                    new Category { CategoryName = "Deski snowboardowe", Description = "Sprzęt snowboardowy", ImageUrl = "https://localhost:7269/images/snowboard.png" },
                    new Category { CategoryName = "Kajaki", Description = "Sprzęt kajakowy", ImageUrl = "https://localhost:7269/images/default_kayak.png" },
                    new Category { CategoryName = "Rolki", Description = "Sprzęt do jazdy na rolkach", ImageUrl = "https://localhost:7269/images/default_rollerblades.png" },
                    new Category { CategoryName = "Łyżwy", Description = "Sprzęt do jazdy na łyżwach", ImageUrl = "https://localhost:7269/images/default_skates.png" },
                    new Category { CategoryName = "Hulajnogi", Description = "Sprzęt do jazdy na hulajnodze", ImageUrl = "https://localhost:7269/images/default_scooter.png" },
                    new Category { CategoryName = "Namioty", Description = "Sprzęt campingowy", ImageUrl = "https://localhost:7269/images/default_tent.png" },
                    new Category { CategoryName = "Sprzęt wspinaczkowy", Description = "Sprzęt do wspinaczki", ImageUrl = "https://localhost:7269/images/default_hike.png" }
                };
                    _dbContext.Categories.AddRange(categories);
                    _dbContext.SaveChanges();
                }

                if (!_dbContext.Equipments.Any())
                {
                    var equipments = new List<Equipment>();

                    var categories = _dbContext.Categories.ToList();

                    equipments.AddRange(new List<Equipment>
                {
                    new Equipment
                    {
                        Name = "Narty zjazdowe damskie",
                        CategoryId = categories.First(c => c.CategoryName == "Narty zjazdowe").CategoryId,
                        Description = "Super szybkie narty damskie. Idealne w góry jak i w jezdzie po asfalcie.",
                        Brand = "ROSSIGNOL",
                        Model = "Nova 6",
                        Availability = true,
                        Condition = Condition.New,
                        QuantityInStock = 15,
                        PricePerDay = 200,
                        ImageUrl = "https://localhost:7269/images/women_skis.png"
                    },
                    new Equipment
                    {
                        Name = "Narty zjazdowe męskie",
                        CategoryId = categories.First(c => c.CategoryName == "Narty zjazdowe").CategoryId,
                        Description = "Super szybkie narty męskie. Idealne w góry jak i w jezdzie po asfalcie.",
                        Brand = "Atomic",
                        Model = "Redster",
                        Availability = true,
                        Condition = Condition.New,
                        QuantityInStock = 10,
                        PricePerDay = 210,
                        ImageUrl = "https://localhost:7269/images/men_skis.png"
                    },
                    new Equipment
                    {
                        Name = "Rower górski",
                        CategoryId = categories.First(c => c.CategoryName == "Rower").CategoryId,
                        Description = "Wygodny i lekki rower górski. Idealny do jazdy po górach.",
                        Brand = "Kross",
                        Model = "Hexagon 6.0",
                        Availability = true,
                        Condition = Condition.New,
                        QuantityInStock = 15,
                        PricePerDay = 170,
                        ImageUrl = "https://localhost:7269/images/specific_bicycle2.png"
                    },
                    new Equipment
                    {
                        Name = "Rower szosowy",
                        CategoryId = categories.First(c => c.CategoryName == "Rower").CategoryId,
                        Description = "Szybki rower szosowy, idealny do jazdy po asfaltowych drogach.",
                        Brand = "Giant",
                        Model = "TCR Advanced",
                        Availability = true,
                        Condition = Condition.New,
                        QuantityInStock = 12,
                        PricePerDay = 200,
                        ImageUrl = "https://localhost:7269/images/specific_bicycle.png"
                    },
                    new Equipment
                    {
                        Name = "Narty biegowe",
                        CategoryId = categories.First(c => c.CategoryName == "Narty biegowe").CategoryId,
                        Description = "Idealne narty do biegów na długie dystanse.",
                        Brand = "Fischer",
                        Model = "Twin Skin Pro",
                        Availability = true,
                        Condition = Condition.New,
                        QuantityInStock = 10,
                        PricePerDay = 150,
                        ImageUrl = "https://localhost:7269/images/run_skis.png"
                    },
                    new Equipment
                    {
                        Name = "Deska snowboardowa",
                        CategoryId = categories.First(c => c.CategoryName == "Deski snowboardowe").CategoryId,
                        Description = "Deska snowboardowa idealna dla początkujących i zaawansowanych.",
                        Brand = "Burton",
                        Model = "Custom",
                        Availability = true,
                        Condition = Condition.New,
                        QuantityInStock = 20,
                        PricePerDay = 220,
                        ImageUrl = "https://localhost:7269/images/snowboard.png"
                    },
                    new Equipment
                    {
                        Name = "Kajak dwuosobowy",
                        CategoryId = categories.First(c => c.CategoryName == "Kajaki").CategoryId,
                        Description = "Stabilny kajak dwuosobowy, idealny na jeziora i rzeki.",
                        Brand = "Intex",
                        Model = "Explorer K2",
                        Availability = true,
                        Condition = Condition.New,
                        QuantityInStock = 8,
                        PricePerDay = 250,
                        ImageUrl = "https://localhost:7269/images/specific_kayak.png"
                    },
                    new Equipment
                    {
                        Name = "Rolki fitness",
                        CategoryId = categories.First(c => c.CategoryName == "Rolki").CategoryId,
                        Description = "Wygodne rolki do jazdy rekreacyjnej i fitness.",
                        Brand = "Rollerblade",
                        Model = "Macroblade 80",
                        Availability = true,
                        Condition = Condition.New,
                        QuantityInStock = 25,
                        PricePerDay = 100,
                        ImageUrl = "https://localhost:7269/images/specific_rollerblades.png"
                    },
                    new Equipment
                    {
                        Name = "Łyżwy hokejowe",
                        CategoryId = categories.First(c => c.CategoryName == "Łyżwy").CategoryId,
                        Description = "Profesjonalne łyżwy hokejowe, idealne do gry i rekreacyjnej jazdy.",
                        Brand = "Bauer",
                        Model = "Vapor X2.9",
                        Availability = true,
                        Condition = Condition.New,
                        QuantityInStock = 18,
                        PricePerDay = 120,
                        ImageUrl = "https://localhost:7269/images/specific_skates.png"
                    },
                    new Equipment
                    {
                        Name = "Hulajnoga elektryczna",
                        CategoryId = categories.First(c => c.CategoryName == "Hulajnogi").CategoryId,
                        Description = "Elektryczna hulajnoga, idealna do szybkiego przemieszczania się po mieście.",
                        Brand = "Xiaomi",
                        Model = "Mi Electric Scooter",
                        Availability = true,
                        Condition = Condition.New,
                        QuantityInStock = 20,
                        PricePerDay = 80,
                        ImageUrl = "https://localhost:7269/images/specific_scooter.png"
                    },
                    new Equipment
                    {
                        Name = "Namiot dwuosobowy",
                        CategoryId = categories.First(c => c.CategoryName == "Namioty").CategoryId,
                        Description = "Lekki i łatwy do rozłożenia namiot dwuosobowy.",
                        Brand = "Quechua",
                        Model = "2 Seconds",
                        Availability = true,
                        Condition = Condition.New,
                        QuantityInStock = 15,
                        PricePerDay = 50,
                        ImageUrl = "https://localhost:7269/images/specific_tent.png"
                    },
                    new Equipment
                    {
                        Name = "Kask wspinaczkowy",
                        CategoryId = categories.First(c => c.CategoryName == "Sprzęt wspinaczkowy").CategoryId,
                        Description = "Bezpieczny kask wspinaczkowy z regulacją.",
                        Brand = "Petzl",
                        Model = "Meteor",
                        Availability = true,
                        Condition = Condition.New,
                        QuantityInStock = 30,
                        PricePerDay = 30,
                        ImageUrl = "https://localhost:7269/images/specific_hike.png"
                    }
                    });
                    _dbContext.Equipments.AddRange(equipments);
                    _dbContext.SaveChanges();
                }
                    

                    if (!_dbContext.Rentals.Any())
                {
                    var rental1 = new Rental
                    {
                        RentalDate = DateTime.Now,
                        ReturnDate = DateTime.Now.AddDays(7),
                        Status = RentalStatus.Active,
                        CustomerName = "Karina",
                        CustomerSurname = "Krotkiewicz",
                        CustomerEmail = "karina@vp.pl",
                        CustomerPhone = "99711",
                        Details = new List<RentalDetail>
                    {
                        new RentalDetail
                        {
                            EquipmentId = 1, 
                            Count = 2,
                        },
                        new RentalDetail
                        {
                            EquipmentId = 2, 
                            Count = 1,
                        }
                    }
                    };

                    var rental2 = new Rental
                    {
                        RentalDate = DateTime.Now.AddDays(-10),
                        ReturnDate = DateTime.Now.AddDays(-3),
                        Status = RentalStatus.Completed,
                        CustomerName = "Jan",
                        CustomerSurname = "Kowalski",
                        CustomerEmail = "jan@vp.pl",
                        CustomerPhone = "123456789",
                        Details = new List<RentalDetail>
                    {
                        new RentalDetail
                        {
                            EquipmentId = 3, 
                            Count = 1,
                        },
                        new RentalDetail
                        {
                            EquipmentId = 4, 
                            Count = 1,
                        }
                    }
                    };

                    _dbContext.Rentals.AddRange(rental1, rental2);
                    _dbContext.SaveChanges();
                }
            }
        }
    }
}
