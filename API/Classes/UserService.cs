using API.Interfaces;
using API.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using API.Classes.Utility;

namespace API.Classes
{
    public class UserService : IUserService
    {
        private IConfiguration configuration;
        private MongoClient dbClient;
        private IMongoDatabase database;
        private IMongoCollection<User> collection;
        private String MongoDatabase;

        public UserService(IConfiguration config)
        {
            this.configuration = config;
            MongoDatabase = configuration.GetConnectionString("Database");
            dbClient = new MongoClient(MongoDatabase);
            database = dbClient.GetDatabase("IMS");
            collection = database.GetCollection<User>("users");
        }

        public User AddUser(User user)
        {
            HashPassword(user);
            try
            {
                collection.InsertOne(user);
                return user;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public User DeleteUser(User user)
        {
            try
            {
                return collection.FindOneAndDelete(f => f.id == user.id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<User> FindUser(User user)
        {
            PropertyInfo[] ItemProperties = user.GetType().GetProperties();
            FilterDefinitionBuilder<User> FilterBuilder = new FilterDefinitionBuilder<User>();
            List<FilterDefinition<User>> filters = new List<FilterDefinition<User>>();
            FilterDefinition<User> finalFilter;

            for (int i = 0; i < ItemProperties.Length; i++)
            {
                if (ItemProperties[i].GetValue(user) != null)
                {
                    filters.Add(FilterBuilder.Eq(ItemProperties[i].Name, ItemProperties[i].GetValue(user)));
                }
            }

            finalFilter = FilterBuilder.And(filters);

            return collection.Find(finalFilter).ToList();
        }

        public User UpdateUser(User user)
        {
            User OldUser = collection.Find(f => f.id == user.id).FirstOrDefault();
            UpdateDefinitionBuilder<User> UpdateBuilder = Builders<User>.Update;
            PropertyInfo[] UserProperties = user.GetType().GetProperties();
            PropertyInfo[] OldUserProperties = OldUser.GetType().GetProperties();
            UpdateDefinition<User> finalUpdate;
            List<UpdateDefinition<User>> updates = new List<UpdateDefinition<User>>();

            for (int i = 0; i < OldUserProperties.Length; i++)
            {
                if (UserProperties[i].GetValue(user) != null)
                {
                    if (!UserProperties[i].GetValue(user).Equals(OldUserProperties[i].GetValue(OldUser)))
                    {
                        if(UserProperties[i].Name == "password")
                        {
                            HashPassword(user);
                        }
                        updates.Add(UpdateBuilder.Set(UserProperties[i].Name, UserProperties[i].GetValue(user)));
                    }
                }
            }

            finalUpdate = UpdateBuilder.Combine(updates);

            if (updates.Count > 0)
            {
                UpdateResult result = collection.UpdateOne(f => f.id == user.id, finalUpdate);
                if (result.ModifiedCount > 0)
                {
                    return collection.Find(f => f.id == user.id).FirstOrDefault();
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        private void HashPassword(User user)
        {
            User TempUser = collection.Find(f => f.id == user.id).FirstOrDefault();
            if(TempUser == null)
            {
                user.salt = HashService.CreateSalt();
            }
            else
            {
                // It is possible to reach this statement from UpdateUser method
                // the user would still have a salt in the database, but it would not have been passed through JSON, so user.salt would be null
                user.salt = TempUser.salt;
            }
            string PassSaltCombination = user.password + user.salt;
            user.password = HashService.HashString(PassSaltCombination);
        }

    }
}
