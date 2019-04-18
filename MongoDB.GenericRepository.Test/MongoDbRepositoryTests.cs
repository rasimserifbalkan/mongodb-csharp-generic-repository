using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.GenericRepository.Models;
using MongoDB.GenericRepository.MongoRepository;
namespace MongoDB.GenericRepository.Test
{
    [TestClass]
    public class MongoDbRepositoryTests
    {
        [TestMethod]
        public void Insert()
        {
            User usr = new User()
            {
                Email = "a@a.com",
                Password = "2222"
            };
            MongoDbRepository<User> userRepository = new MongoDbRepository<User>();
            User result = userRepository.Insert(usr);

            Assert.IsTrue(userRepository != null);



        }
        [TestMethod]
        public void Update()
        {
            string beforeUpdate = null;
            string afterUpdate = null;

            User usr = new User()
            {
                Email = "a@a.com",
                Password = "2222"
            };
            beforeUpdate = usr.Email;
            MongoDbRepository<User> userRepository = new MongoDbRepository<User>();
            User result = userRepository.Insert(usr);

            result.Email = "a@b.com";

            userRepository.Update(result);

            beforeUpdate = result.Email;

            Assert.AreNotEqual(beforeUpdate, afterUpdate);

        }
        [TestMethod]
        public void Delete()
        {
            User usr = new User()
            {
                Email = "a@c.com",
                Password = "2222"
            };
            MongoDbRepository<User> userRepository = new MongoDbRepository<User>();
            User result = userRepository.Insert(usr);

            var deletedUser = userRepository.Get(x => x.Email == usr.Email);
            userRepository.Delete(deletedUser);
            var user = userRepository.Get(x => x.Email == usr.Email);
            Assert.IsNull(user);

        }
        [TestMethod]
        public void Get()
        {
           
            MongoDbRepository<User> userRepository = new MongoDbRepository<User>();
           
            var user = userRepository.Get(x => x.Email == "a@b.com");
           
            Assert.IsNotNull(user);

        }
        [TestMethod]
        public void GetAll()
        {

            MongoDbRepository<User> userRepository = new MongoDbRepository<User>();

            var users = userRepository.GetAll();

            Assert.IsTrue(users.Count > 0);

        }
        [TestMethod]
        public void SearchFor()
        {
            MongoDbRepository<User> userRepository = new MongoDbRepository<User>();
            var users = userRepository.SearchFor(x => x.Email.Contains("@"));

            Assert.IsTrue(users.Count > 0);
        }


    }
}
