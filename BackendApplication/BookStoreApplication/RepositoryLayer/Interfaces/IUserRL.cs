using CommonLayer.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces
{
    public interface IUserRL
    {
        SignupResponseModel UserSignup(UserEntities model);
        LoginResponseModel Login(LoginModel model);
    }
}
