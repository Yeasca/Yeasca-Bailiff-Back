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
        }

        private void mapperLesUtilisateurs()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(Utilisateur)))
            {
                BsonClassMap.RegisterClassMap<Utilisateur>(x =>
                {
                    x.AutoMap();
                    x.UnmapMember(utilisateur => utilisateur.Profile);
                });
            }
        }

        private void mapperLesParties()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof (Partie)))
            {
                BsonClassMap.RegisterClassMap<Partie>();
            }
            if (!BsonClassMap.IsClassMapRegistered(typeof(PartieAvecSociete)))
            {
                BsonClassMap.RegisterClassMap<PartieAvecSociete>();
            } 
            if (!BsonClassMap.IsClassMapRegistered(typeof(Huissier)))
            {
                BsonClassMap.RegisterClassMap<Huissier>();
            }
            if (!BsonClassMap.IsClassMapRegistered(typeof(ClientParticulier)))
            {
                BsonClassMap.RegisterClassMap<ClientParticulier>();
            }
            if (!BsonClassMap.IsClassMapRegistered(typeof(ClientSociete)))
            {
                BsonClassMap.RegisterClassMap<ClientSociete>();
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
    }
}
