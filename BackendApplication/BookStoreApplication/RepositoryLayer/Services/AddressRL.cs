using CommonLayer.AddressModels;
using CommonLayer.UserModels;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Driver;
using RepositoryLayer.DatabaseConfig;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RepositoryLayer.Services
{
    public class AddressRL : IAddressRL
    {
        private readonly IMongoCollection<UserEntities> userEntities;

        public AddressRL(IBookstoreDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            userEntities = database.GetCollection<UserEntities>(settings.UserCollectionName);
        }

        public AddressResponseModel AddAddress(AddAddressModel model, string jwtUserId)
        {
            try
            {
                var validateUser = userEntities.Find(x => x.UserId == jwtUserId).FirstOrDefault();
                if (validateUser != null)
                {
                    if (validateUser.UserId == jwtUserId)
                    {
                        AddressEntities addressEntities = new()
                        {
                            AddressId = ObjectId.GenerateNewId().ToString(),
                            FullAddress = model.FullAddress,
                            City = model.City,
                            State = model.State
                        };
                        validateUser.AddressEntities = addressEntities;
                        userEntities.ReplaceOne(e => e.UserId == jwtUserId, validateUser);
                        AddressResponseModel addressResponseModel = new()
                        {
                            UserId = validateUser.UserId,
                            FullName = validateUser.FullName,
                            EmailId = validateUser.EmailId,
                            AddressId = validateUser.AddressEntities.AddressId,
                            FullAddress = validateUser.AddressEntities.FullAddress,
                            City = validateUser.AddressEntities.City,
                            State = validateUser.AddressEntities.State
                        };
                        return addressResponseModel;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
