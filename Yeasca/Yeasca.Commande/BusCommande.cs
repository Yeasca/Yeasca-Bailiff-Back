using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Yeasca.Metier;

namespace Yeasca.Commande
{
    public class BusCommande
    {
        public static ReponseCommande exécuter(IMessageCommande message)
        {
            Type typeDuMessage = trouverLeTypeDuMessage(message);
            if (typeDuMessage != null)
            {
                Type typeDeLaCommande = _listeDesCommandes[typeDuMessage];
                return exécuterLaCommandeDeType(typeDeLaCommande, message);
            }
            return ReponseCommande.générerUnEchec(Ressource.Commandes.ERREUR_TYPE_MESSAGE);
        }

        private static readonly IDictionary<Type, Type> _listeDesCommandes = new Dictionary<Type, Type>()
        {
            {typeof(ICreerConstatMessage), typeof(CreerConstatCommande)},
            {typeof(IAjouterFichierConstatMessage), typeof(AjouterFichierConstatCommande)},
            {typeof(IValiderConstatMessage), typeof(ValiderConstatCommande)},
            {typeof(ICreerClientMessage), typeof(CreerClientCommande)},
            {typeof(IModifierClientMessage), typeof(ModifierClientCommande)},
            {typeof(ICreerHuissierMessage), typeof(CreerHuissierCommande)},
            {typeof(IModifierHuissierMessage), typeof(ModifierHuissierCommande)},
            {typeof(ICreerSecretaireMessage), typeof(CreerSecretaireCommande)},
            {typeof(IModifierSecretaireMessage), typeof(ModifierSecretaireCommande)},
            {typeof(ICreerAdministrateurMessage), typeof(CreerAdministrateurCommande)},
            {typeof(IConnexionMessage), typeof(ConnexionCommande)},
            {typeof(IDeconnexionMessage), typeof(DeconnexionCommande)}
        };

        private static Type trouverLeTypeDuMessage(IMessageCommande message)
        {
            Type[] interfaces = message.GetType().GetInterfaces();
            return interfaces.FirstOrDefault(x => _listeDesCommandes.ContainsKey(x));
        }

        private static ReponseCommande exécuterLaCommandeDeType(Type typeDeLaCommande, IMessageCommande message)
        {
            try
            {
                object commande = Activator.CreateInstance(typeDeLaCommande);
                string nomDeLaMéthode = typeof(ICommande<IMessageCommande>).GetMethods().FirstOrDefault().Name;
                MethodInfo methodInfo = typeDeLaCommande.GetMethod(nomDeLaMéthode);
                object[] paramètres = new object[] { message };
                object réponse = methodInfo.Invoke(commande, paramètres);
                return (ReponseCommande)réponse;
            }
            catch (Exception)
            {
                //TODO : log
                return ReponseCommande.générerUnEchec(Ressource.Commandes.ERREUR_EXCEPTION_BUS_COMMANDE);
            }
        }
    }
}
