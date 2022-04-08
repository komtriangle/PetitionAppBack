using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using PetitionApp.Core.Models;
using PetitionApp.Core.Services;
using PetitionApp.Data;
using PetitionApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace PetitionApp.UnitTests.Petitions
{
    public class PetitionServiceTest
    {
        PetitionAppDbContext _context;
        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<PetitionAppDbContext>()
                .UseInMemoryDatabase(databaseName: "PetitionAppDbContextTest")
                .Options;

            _context = new PetitionAppDbContext(options);

       
        }

        [TearDown]
        public void DeleteData()
        {
            _context.Database.EnsureDeleted();
        }




        [Test]
        public  void TestGetPetitions()
        {

            var unitOfWork = new UnitOfWork(_context);
            var petitionService = new PetitionService(unitOfWork);

            var UserGuid = Guid.NewGuid();

            _context.Users.Add(new User()
            {
                Id = UserGuid,
                UserName = "TestUser",
                PasswordHash = "fref4fkf48ffj4fkk"
            });

            _context.Petitions.AddRange(
                new Petition()
                {
                    Id = 1,
                    AuthorId = UserGuid,
                    Title = "Test Petition 1",
                    Text = "Text of test petition 1",
                    Goal = 100,
                    CountVoices = 0
                },
                new Petition()
                {
                    Id = 2,
                    AuthorId = UserGuid,
                    Title = "Test Petition 2",
                    Text = "Text of test petition 2",
                    Goal = 100,
                    CountVoices = 0
                },
                new Petition()
                {
                    Id = 3,
                    AuthorId = UserGuid,
                    Title = "Test Petition 3",
                    Text = "Text of test petition 3",
                    Goal = 100,
                    CountVoices = 0
                },
                new Petition()
                {
                    Id = 4,
                    AuthorId = UserGuid,
                    Title = "Test Petition 4",
                    Text = "Text of test petition 4",
                    Goal = 100,
                    CountVoices = 0
                },
                new Petition()
                {
                    Id = 5,
                    AuthorId = UserGuid,
                    Title = "Test Petition 5",
                    Text = "Text of test petition 5",
                    Goal = 100,
                    CountVoices = 0
                },
                new Petition()
                {
                    Id = 6,
                    AuthorId = UserGuid,
                    Title = "Test Petition 6",
                    Text = "Text of test petition 6",
                    Goal = 100,
                    CountVoices = 0
                },
                new Petition()
                {
                    Id = 7,
                    AuthorId = UserGuid,
                    Title = "Test Petition 7",
                    Text = "Text of test petition 7",
                    Goal = 100,
                    CountVoices = 0
                }
                );

            _context.SaveChanges();


            var petitions = petitionService.GetTopPetitions(100).ToList();
            Assert.IsNotNull(petitions);
            Assert.AreEqual(7, petitions.Count);

            petitions = petitionService.GetTopPetitions(2).ToList();
            Assert.AreEqual(2, petitions.Count);

            petitions = petitionService.GetTopPetitions(0).ToList();
            Assert.AreEqual(0, petitions.Count);
        }

        [Test]
        public async Task TestGetPetitionsByTags()
        {
            var unitOfWork = new UnitOfWork(_context);
            var petitionService = new PetitionService(unitOfWork);

            var UserGuid = Guid.NewGuid();

            _context.Users.Add(new User()
            {
                Id = UserGuid,
                UserName = "TestUser",
                PasswordHash = "fref4fkf48ffj4fkk"
            });

            _context.Petitions.AddRange(
                new Petition()
                {
                    Id = 1,
                    AuthorId = UserGuid,
                    Title = "Test Petition 1",
                    Text = "Text of test petition 1",
                    Goal = 100,
                    CountVoices = 0,
                },
                new Petition()
                {
                    Id = 2,
                    AuthorId = UserGuid,
                    Title = "Test Petition 2",
                    Text = "Text of test petition 2",
                    Goal = 100,
                    CountVoices = 0
                },
                new Petition()
                {
                    Id = 3,
                    AuthorId = UserGuid,
                    Title = "Test Petition 3",
                    Text = "Text of test petition 3",
                    Goal = 100,
                    CountVoices = 0,
                },
                new Petition()
                {
                    Id = 4,
                    AuthorId = UserGuid,
                    Title = "Test Petition 4",
                    Text = "Text of test petition 4",
                    Goal = 100,
                    CountVoices = 0,
                },
                new Petition()
                {
                    Id = 5,
                    AuthorId = UserGuid,
                    Title = "Test Petition 5",
                    Text = "Text of test petition 5",
                    Goal = 100,
                    CountVoices = 0
                },
                new Petition()
                {
                    Id = 6,
                    AuthorId = UserGuid,
                    Title = "Test Petition 6",
                    Text = "Text of test petition 6",
                    Goal = 100,
                    CountVoices = 0
                },
                new Petition()
                {
                    Id = 7,
                    AuthorId = UserGuid,
                    Title = "Test Petition 7",
                    Text = "Text of test petition 7",
                    Goal = 100,
                    CountVoices = 0
                }
                );

            _context.Tags.AddRange(
                new Tag()
                {
                    Id = 1,
                    Name = "Tag1"
                },
                new Tag()
                {
                    Id = 2,
                    Name = "Tag2"
                },
                new Tag()
                {
                    Id = 3,
                    Name = "Tag3"
                },
                new Tag()
                {
                    Id = 4,
                    Name = "Tag4"
                },
                new Tag()
                {
                    Id = 5,
                    Name = "Tag5"
                });

            _context.PetitionTags.AddRange(
                new PetitionTags()
                {
                    PetitionId = 1,
                    TagId = 1
                },
                new PetitionTags()
                {
                    PetitionId = 1,
                    TagId = 2
                },
                new PetitionTags()
                {
                    PetitionId = 1,
                    TagId = 3
                },
                new PetitionTags()
                {
                    PetitionId = 2,
                    TagId = 1
                },
                new PetitionTags()
                {
                    PetitionId = 3,
                    TagId = 1
                },
                new PetitionTags()
                {
                    PetitionId = 4,
                    TagId = 2
                },
                new PetitionTags()
                {
                    PetitionId = 4,
                    TagId = 1
                },
                new PetitionTags()
                {
                    PetitionId = 5,
                    TagId = 1
                },
                new PetitionTags()
                {
                    PetitionId = 5,
                    TagId = 5
                });
            _context.SaveChanges();


            var petitions = await petitionService.GetPetitionsByTags(new List<Tag>() { new Tag() { Name = "Tag1" } });
            Assert.AreEqual(5, petitions.ToList().Count);

            petitions = await petitionService.GetPetitionsByTags(new List<Tag>() { new Tag() { Name = "Tag2" } });
            Assert.AreEqual(2, petitions.ToList().Count);

            petitions = await petitionService.GetPetitionsByTags(new List<Tag>() { new Tag() { Name = "Tag3" } });
            Assert.AreEqual(1, petitions.ToList().Count);

            petitions = await petitionService.GetPetitionsByTags(new List<Tag>() { new Tag() { Name = "Tag4" } });
            Assert.AreEqual(0, petitions.ToList().Count);

            petitions = await petitionService.GetPetitionsByTags(new List<Tag>() { new Tag() { Name = "Tag5" } });
            Assert.AreEqual(1, petitions.ToList().Count);

            petitions = await petitionService.GetPetitionsByTags(new List<Tag>() { new Tag() { Name = "Non-existentTag" } });
            Assert.AreEqual(0, petitions.ToList().Count);
        }

        [Test]
        public void GetPetitionByAuthor()
        {
            var unitOfWork = new UnitOfWork(_context);
            var petitionService = new PetitionService(unitOfWork);

            var UserGuid1 = Guid.NewGuid();
            var UserGuid2 = Guid.NewGuid();
            var UserGuid3 = Guid.NewGuid();

            _context.Users.AddRange(
                new User()
                {
                Id = UserGuid1,
                UserName = "TestUser",
                PasswordHash = "fref4fkf48ffj4fkk"
                },
                new User()
                {
                    Id = UserGuid2,
                    UserName = "TestUser",
                    PasswordHash = "fref4fkf48ffj4fkk"
                },
                new User()
                {
                    Id = UserGuid3,
                    UserName = "TestUser",
                    PasswordHash = "fref4fkf48ffj4fkk"
                });



            _context.Petitions.AddRange(
                new Petition()
                {
                    Id = 1,
                    AuthorId = UserGuid1,
                    Title = "Test Petition 1",
                    Text = "Text of test petition 1",
                    Goal = 100,
                    CountVoices = 0
                },
                new Petition()
                {
                    Id = 2,
                    AuthorId = UserGuid2,
                    Title = "Test Petition 2",
                    Text = "Text of test petition 2",
                    Goal = 100,
                    CountVoices = 0
                },
                new Petition()
                {
                    Id = 3,
                    AuthorId = UserGuid3,
                    Title = "Test Petition 3",
                    Text = "Text of test petition 3",
                    Goal = 100,
                    CountVoices = 0
                },
                new Petition()
                {
                    Id = 4,
                    AuthorId = UserGuid1,
                    Title = "Test Petition 4",
                    Text = "Text of test petition 4",
                    Goal = 100,
                    CountVoices = 0
                },
                new Petition()
                {
                    Id = 6,
                    AuthorId = UserGuid1,
                    Title = "Test Petition 6",
                    Text = "Text of test petition 6",
                    Goal = 100,
                    CountVoices = 0
                },
                new Petition()
                {
                    Id = 7,
                    AuthorId = UserGuid3,
                    Title = "Test Petition 7",
                    Text = "Text of test petition 7",
                    Goal = 100,
                    CountVoices = 0
                }
                );

            _context.SaveChanges();

            var petitions = petitionService.GetPetitionByAuthor(new User() { Id = UserGuid1 }).ToList();

            Assert.AreEqual(3, petitions.Count);
            Assert.AreEqual(1, petitions[0].Id);
            Assert.AreEqual(4, petitions[1].Id);
            Assert.AreEqual(6, petitions[2].Id);

            petitions = petitionService.GetPetitionByAuthor(new User() { Id = UserGuid2 }).ToList();
            Assert.AreEqual(1, petitions.Count);

            petitions = petitionService.GetPetitionByAuthor(new User() { Id = UserGuid3 }).ToList();
            Assert.AreEqual(2, petitions.Count);

            petitions = petitionService.GetPetitionByAuthor(new User() { Id = Guid.NewGuid() }).ToList();
            Assert.AreEqual(0, petitions.Count);


        }


    }
}