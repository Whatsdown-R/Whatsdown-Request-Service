using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using Whatsdown_Friend_Service;
using Whatsdown_Friend_Service.Data;
using Whatsdown_Friend_Service.Exceptions;
using Whatsdown_Friend_Service.Models;
using Whatsdown_Friend_Service.Views;
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
     

      
        [Fact(DisplayName = "Fail to Request a friendship")]
        public void Friend_Request_Already_Exist_Throw_Exception()
        {
            string userOneID = "1";
            string userTwoID = "2";
            using (var context = new FriendContext(options))
            {
                Relationship relationship = new Relationship(Guid.NewGuid().ToString(), userOneID, userTwoID, userOneID, "PENDING");
                context.Relationships.Add(relationship);
                context.SaveChanges();
            }

            using (var context = new FriendContext(options))
            {
                RelationshipLogic logic = new RelationshipLogic(context);

                Exception ex = Assert.Throws<RequestAlreadyExistException>(() => logic.RequestFriend(userOneID, userTwoID));

                context.Dispose();
                Assert.Equal("Invalid friend request. It already exists with the users:1 and 2", ex.Message);

            }
        }


        [Theory(DisplayName = "Throw argumant exception with  null arguments")]
        [InlineData(null, "2")]
        [InlineData("1", null)]
        public void Friend_Null_Arguments_Throw_Exception(string userOneID, string userTwoID)
        {
            using (var context = new FriendContext(options))
            {
                RelationshipLogic logic = new RelationshipLogic(context);

                Exception ex = Assert.Throws<ArgumentNullException>(() => logic.RequestFriend(userOneID, userTwoID));

                context.Dispose();
                Assert.Equal("Value cannot be null.", ex.Message);

            }
        }

        [Theory(DisplayName = "Throw argumant exception with  empty arguments")]
        [InlineData("", "2")]
        [InlineData("1", "")]
        [InlineData("", "")]
        public void Friend_Empty_Arguments_Throw_Exception(string userOneID, string userTwoID)
        {
            using (var context = new FriendContext(options))
            {
                RelationshipLogic logic = new RelationshipLogic(context);

                Exception ex = Assert.Throws<ArgumentException>(() => logic.RequestFriend(userOneID, userTwoID));

                context.Dispose();
                Assert.Equal("Value does not fall within the expected range.", ex.Message);

            }
        }

     

        [Fact(DisplayName = "Accept friend request throws exception")]
        public void Accept_Friend_Request()
        {
            string userOneID = "1";
            string userTwoID = "2";
            string relationId = "231";
            using (var context = new FriendContext(options))
            {
                Relationship relationship = new Relationship(relationId, userOneID, userTwoID, userTwoID, "PENDING");
                context.Relationships.Add(relationship);
                context.SaveChanges();
            }

            using (var context = new FriendContext(options))
            {
                RelationshipLogic logic = new RelationshipLogic(context);

                logic.AcceptFriend(userOneID, relationId);

                Relationship rel = logic.GetFriend(userOneID, userTwoID);

                Assert.Equal("ACCEPTED", rel.Status);
                context.Dispose();

            }
        }
        [Fact(DisplayName = "Refuse Accept Request cuz it does not exist")]
        public void Accept_Friend_Request_Throw_Exception()
        {

            using (var context = new FriendContext(options))
            {
                string userOneID = "1";
                string userTwoID = "2";
                RelationshipLogic logic = new RelationshipLogic(context);

                Exception ex = Assert.Throws<RequestDoesNotExistException>(() => logic.AcceptFriend(userOneID, userTwoID));

                context.Dispose();
                Assert.Equal("Friend request does not exist", ex.Message);

            }
        }


        [Fact(DisplayName = "Deny friend request throws exception")]
        public void Deny_Friend_Request()
        {
            string userOneID = "1";
            string userTwoID = "2";
            string relationId = "231";
            using (var context = new FriendContext(options))
            {
                Relationship relationship = new Relationship(relationId, userOneID, userTwoID, userTwoID, "PENDING");
                context.Relationships.Add(relationship);
                context.SaveChanges();
            }

            using (var context = new FriendContext(options))
            {
                RelationshipLogic logic = new RelationshipLogic(context);

                logic.DenyFriend(userOneID, relationId);

                Relationship rel = logic.GetFriend(userOneID, userTwoID);

                Assert.Equal("DENIED", rel.Status);
                context.Dispose();

            }
        }

        [Fact(DisplayName = "Refuse Deny Request cuz it does not exist")]
        public void Deny_Friend_Request_Throw_Exception()
        {

            using (var context = new FriendContext(options))
            {
                string userOneID = "1";
                string userTwoID = "2";
                RelationshipLogic logic = new RelationshipLogic(context);

                Exception ex = Assert.Throws<RequestDoesNotExistException>(() => logic.DenyFriend(userOneID, userTwoID));

                context.Dispose();
                Assert.Equal("Friend request does not exist", ex.Message);

            }
        }
    }
}
