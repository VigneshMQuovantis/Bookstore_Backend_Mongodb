using CommonLayer.UserModels;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using RepositoryLayer.DatabaseConfig;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Services
{
    public class UserRL : IUserRL
    {
        private readonly IMongoCollection<UserEntities> userEntities;

        private readonly IConfiguration config;

        public UserRL(IBookstoreDatabaseSettings settings, IConfiguration config)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            userEntities = database.GetCollection<UserEntities>(settings.UserCollectionName);
            this.config = config;
        }

        public string JwtTokenGenerate(string emailId, string userId)
        {
            try
            {
                var loginTokenHandler = new JwtSecurityTokenHandler();
                var loginTokenKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(this.config[("Jwt:key")]));
                var loginTokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Email, emailId),
                        new Claim("userId", userId)
                    }),
                    Expires = DateTime.UtcNow.AddMinutes(15),
                    SigningCredentials = new SigningCredentials(loginTokenKey, SecurityAlgorithms.HmacSha256Signature)
                };
                var token = loginTokenHandler.CreateToken(loginTokenDescriptor);
                return loginTokenHandler.WriteToken(token);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public LoginResponseModel Login(LoginModel model)
        {

            try
            {
               var login = userEntities.Find(e=>e.EmailId == model.EmailId && e.Password == model.Password).FirstOrDefault();
                if (login != null)
                {
                    var token = this.JwtTokenGenerate(login.EmailId, login.UserId.ToString());
                    LoginResponseModel loginResponseModel = new LoginResponseModel()
                   {
                       UserId = login.UserId.ToString(),
                       EmailId = login.EmailId,
                       FullName = login.Password,
                       JwtToken = token
                   };
                    return loginResponseModel;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SignupResponseModel UserSignup(UserEntities model)
        {
            try
            {
                userEntities.InsertOne(model);
                SignupResponseModel signupResponseModel = new()
                {
                    FullName = model.FullName,
                    EmailId = model.EmailId
                };
                return signupResponseModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
