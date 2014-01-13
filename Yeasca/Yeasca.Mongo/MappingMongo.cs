using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Options;
using Yeasca.Metier;

namespace Yeasca.Mongo
{
    public class MappingMongo
    {
        public void enregistrer()
        {
            mapperLesUtilisateurs();
            mapperLesParties();
            mapperLesConstats();
            mapperLesJetons();
        }

        private void mapperLesUtilisateurs()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(Utilisateur)))
            {
                BsonClassMap.RegisterClassMap<Utilisateur>();
            }
        }

        private void mapperLesParties()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof (Profile)))
            {
                BsonClassMap.RegisterClassMap<Profile>();
            }
            if (!BsonClassMap.IsClassMapRegistered(typeof(Huissier)))
            {
                BsonClassMap.RegisterClassMap<Huissier>();
            }
            if (!BsonClassMap.IsClassMapRegistered(typeof(Client)))
            {
                BsonClassMap.RegisterClassMap<Client>();
            }
            if (!BsonClassMap.IsClassMapRegistered(typeof(Secretaire)))
            {
                BsonClassMap.RegisterClassMap<Secretaire>();
            } 
        }

        private void mapperLesConstats()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof (Constat)))
            {
                BsonClassMap.RegisterClassMap<Constat>(x =>
                {
                    x.AutoMap();
                    x.GetMemberMap(constat => constat.Date).SetSerializationOptions(DateTimeSerializationOptions.LocalInstance);
                });
            }
        }

        private void mapperLesJetons()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(Jeton)))
            {
                BsonClassMap.RegisterClassMap<Jeton>();
            } 
        }
    }
}
