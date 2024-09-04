using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DML.ConfigServices.MongoDb
{
    public interface IEntityDbSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string PermissionCollectionName { get; set; }
    }
}
