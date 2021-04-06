using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using Whatsdown_Friend_Service;
using Whatsdown_Friend_Service.Data;
using Whatsdown_Friend_Service.Models;
using Xunit;
using Xunit.Abstractions;

namespace Friend_Tests
{
    public class UnitTest1
    {
        private DbContextOptions<FriendContext> options = new DbContextOptionsBuilder<FriendContext>()
            .UseInMemoryDatabase(databaseName: "UserDatabase")
            .Options;

        private readonly ITestOutputHelper _output;

        public UnitTest1(ITestOutputHelper output)
        {
            var builder = new DbContextOptionsBuilder<FriendContext>();
            builder.UseInMemoryDatabase(databaseName: "UserDatabase");

            var dbContextOptions = builder.Options;
            var friendContext = new FriendContext(dbContextOptions);
            // Delete existing db before creating a new one
            friendContext.Database.EnsureDeleted();
            friendContext.Database.EnsureCreated();
            
            _output = output;
        }


        [Theory(DisplayName = "Get Single relationships from database using logic")]
        [InlineData("1", "2", true)]
        [InlineData("1", "3", false)]
        public void GetUserFromDatabase(string UserOneID, string UserTwoID, bool expected)
        {
            using (var context = new FriendContext(options))
            {
                Relationship relationship = new Relationship(Guid.NewGuid().ToString(), "1", "2", "1", "PENDING");
                context.Relationships.Add(relationship);
                context.SaveChanges();
            }

            using (var context = new FriendContext(options))
            {
                bool actual = false;
                RelationshipLogic logic = new RelationshipLogic(context);
                Relationship relationship = null;

                relationship = logic.GetFriend(UserOneID, UserTwoID);
               

                

                if (relationship != null) actual = true;



                Assert.Equal(expected, actual);
                context.Dispose();

            }

       
        }
        [Theory(DisplayName = "Get Multiple relationships from database using logic")]
        [InlineData("1",  3)]
        [InlineData("2",  2)]
        [InlineData("5", 1)]
        [InlineData("3", 2)]
        [InlineData("7", 0)]
        public void GetUsersFromLogic(string UserOneID, int expectedAmountOfFriends)
        {
            using (var context = new FriendContext(options))
            {
                List<Relationship> relationships = new List<Relationship>();
                Relationship relationship = new Relationship("ID1", "1", "2", "2", "PENDING");
                relationships.Add(relationship);
                relationship = new Relationship("ID2", "1", "3", "1", "PENDING");
                relationships.Add(relationship);
                relationship = new Relationship("ID3", "1", "5", "1", "PENDING");
                relationships.Add(relationship);
                relationship = new Relationship("ID4", "2", "3", "2", "PENDING");
                relationships.Add(relationship);


                context.Relationships.AddRange(relationships);
                context.SaveChanges();
  
            }

            using (var context = new FriendContext(options))
            {
                bool actual = false;
                RelationshipLogic logic = new RelationshipLogic(context);
                List<Relationship> relationship = null;
                _output.WriteLine("Current list of relationships: 0");
                relationship = logic.GetFriends(UserOneID);

                foreach(Relationship ship in relationship)
                {
                    _output.WriteLine(ship.ToString());
                }
                if (relationship != null)
                    _output.WriteLine("Current list of relationships: " + relationship.Count);
                else
                    _output.WriteLine("Current list of relationships: null");

                 context.Dispose();
                Assert.Equal(expectedAmountOfFriends, relationship.Count);
               

            }


        }

        [Theory(DisplayName = "Succesfull Request a friendship")]
        [InlineData("1","2", 2)]
        [InlineData("1", "3", 1)]
        public void Create_Friend_Request(string UserOneID, string UserTwoID, int expectedAmount)
        {
            using (var context = new FriendContext(options))
            {
                Relationship relationship = new Relationship(Guid.NewGuid().ToString(), "1", "3", "1", "PENDING");
                context.Relationships.Add(relationship);
                context.SaveChanges();
            }

            using (var context = new FriendContext(options))
            {
                
                RelationshipLogic logic = new RelationshipLogic(context);
                Relationship ship = null;
                List<Relationship> relationships = null;

                relationships = logic.GetFriends(UserOneID);
        
                logic.RequestFriend(UserOneID, UserTwoID);
                relationships = logic.GetFriends(UserOneID);
  
                context.Dispose();
                Assert.Equal(expectedAmount, relationships.Count);
              
            }
        }
        [Theory(DisplayName = "Fail to Request a friendship")]
        [InlineData("1", "2", true)]
        public void Fail_Friend_Request(string UserOneID, string UserTwoID, bool expected)
        {
            using (var context = new FriendContext(options))
            {
                Relationship relationship = new Relationship(Guid.NewGuid().ToString(), "1", "2", "1", "PENDING");
                context.Relationships.Add(relationship);
                context.SaveChanges();
            }

            using (var context = new FriendContext(options))
            {
                bool actual = false;





                Assert.Equal(actual, expected);
            }
        }

    }
}
