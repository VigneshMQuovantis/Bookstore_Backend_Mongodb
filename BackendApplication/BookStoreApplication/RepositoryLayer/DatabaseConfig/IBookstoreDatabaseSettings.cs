using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.DatabaseConfig
{
    public interface IBookstoreDatabaseSettings
    {
        public string UserCollectionName { get; set; }
        public string BookCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
