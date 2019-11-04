using MongoDB.Bson;
using MongoDB.Driver;
using SuperThings.Data.Models;
using SuperThings.Data.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace SuperThings.Data.Repository.Mongo
{
    public class RegistrationRepository_Mongo : IRegistrationRepository
    {
        private readonly IMongoCollection<Registrant> _registrants;

        public RegistrationRepository_Mongo(ISuperThingsDatabaseSettings settings)
        {
            var client = new MongoClient(settings.MongoConnectionString);
            var database = client.GetDatabase(settings.MongoDatabaseName);

            _registrants = database.GetCollection<Registrant>(settings.MongoRegistrantCollectionName);
        }

        public async Task<int> CreateRegistration(Registrant entry)
        {
            var maxId = _registrants.Find(new BsonDocument()).SortByDescending(x => x.Id).FirstOrDefault()?.Id;
            entry.Id = maxId == null ? 1 : maxId.Value + 1; ;
            await _registrants.InsertOneAsync(entry);
            return entry.Id;
        }

        public async Task<int> GetCount()
        {
            var res = await _registrants.CountDocumentsAsync(new BsonDocument());
            return (int)res;
        }

        public async Task<ICollection<FavoriteInteger>> GetFavoriteIntegersStats(int num)
        {
            //this could be made more efficient
            var res = await _registrants.Find(new BsonDocument()).ToListAsync();

            return res.SelectMany(x => x.FavoriteIntegers).GroupBy(x => x.IntegerValue)
                .Select(g => new
                {
                    intVal = g.Key,
                    count = g.Count(),
                })
                .OrderByDescending(x => x.count)
                .Take(num)
                .Select(x => new FavoriteInteger
                {
                    Count = x.count,
                    IntegerValue = x.intVal
                }).ToList();
        }

        public async Task<ICollection<Registrant>> GetMostRecent(int num)
        {
            var res = await _registrants.Find(new BsonDocument()).SortByDescending(x => x.Id).Limit(10).ToListAsync();
            return res;
        }

        public async Task<Registrant> GetRegistrant(int id)
        {
            return await _registrants.Find(x => x.Id == id).FirstOrDefaultAsync();
        }
    }
}
