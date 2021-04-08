using EFCore.Repositories;
using EFCore.Data;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Linq;

namespace EFCore.Repositories.Tests
{
    [TestFixture()]
    public class AuthorRepositoryTests
    {
        private static readonly DbContextOptions<EFCoreDbContext> _options = new DbContextOptionsBuilder<EFCoreDbContext>()
            .UseInMemoryDatabase(databaseName: "EFCoreInMemoryDatabase")
            .Options;
        private static readonly AuthorRepository _repository = new AuthorRepository(new EFCoreDbContext(_options));

        [SetUp]
        public void NUnitSetUp()
        {
            // Insert seed data into the database using one instance of the context
            _repository.Insert(new Models.Author { Id = 1, FirstName = "Hung", LastName = "Bui", CreatedDate = DateTime.Now });
            _repository.Insert(new Models.Author { Id = 2, FirstName = "Dat", LastName = "Bui", CreatedDate = DateTime.Now });
            _repository.Insert(new Models.Author { Id = 3, FirstName = "Duc", LastName = "Bui", CreatedDate = DateTime.Now });
        }

        [Test()]
        public void GetAllTest()
        {
            //Act
            var authors = _repository.GetAll();

            //Assert
            Assert.IsNotNull(authors);
            Assert.AreEqual(3, authors.Count());
        }

        [Test()]
        public void GetTest()
        {
            //Arrange
            const long id = 1;
            const string expected = "Hung Bui";

            //Act
            var author = _repository.Get(id);
            var result = $"{author.FirstName} {author.LastName}";

            //Assert
            Assert.IsNotNull(result);           //assert that a result was returned
            Assert.AreEqual(expected, result);  //assert that actual result was as expected
        }
    }
}