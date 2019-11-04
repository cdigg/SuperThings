using System;
using System.Collections.Generic;
using System.Text;

namespace SuperThings.Data.Repository
{
    public interface ISuperThingsDatabaseSettings
    {
        bool UseSql { get; set; }
        string MongoRegistrantCollectionName { get; set; }
        string MongoRegistrantIntegerCollectionName { get; set; }
        string MongoConnectionString { get; set; }
        string MongoDatabaseName { get; set; }
        string SqlConnectionString { get; set; }

    }
    public class SuperThingsDatabaseSettings : ISuperThingsDatabaseSettings
    {
        public bool UseSql { get; set; }
        public string MongoRegistrantCollectionName { get; set; }
        public string MongoRegistrantIntegerCollectionName { get; set; }
        public string MongoConnectionString { get; set; }
        public string MongoDatabaseName { get; set; }
        public string SqlConnectionString { get; set; }
    }
}
