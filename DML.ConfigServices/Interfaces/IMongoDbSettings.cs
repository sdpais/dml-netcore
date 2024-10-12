using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DML.ConfigServices.Interfaces
{
    public interface IMongoDbSettings
    {
        public string ConnectionString { get; }
        public string DatabaseName { get; }
        public string CollectionName { get;  }
    }
}
