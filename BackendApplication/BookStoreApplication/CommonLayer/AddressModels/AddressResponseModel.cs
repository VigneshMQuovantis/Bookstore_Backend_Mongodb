using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLayer.AddressModels
{
    public class AddressResponseModel
    {
        public string UserId { get; set; }
        public string FullName { get; set; }
        public string EmailId { get; set; }
        public string AddressId { get; set; }
        public string FullAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
    }
}
