using CommonLayer.AddressModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces
{
    public interface IAddressRL
    {
        AddressResponseModel AddAddress(AddAddressModel model, string jwtUserId);
    }
}
