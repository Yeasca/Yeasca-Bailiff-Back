using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Yeasca.Metier;

namespace Yeasca.Requete
{
    public class BusRequete
    {
        public static ReponseRequete exécuter(IMessageRequete message)
        {
            Type typeDuMessage = trouverLeTypeDuMessage(message);
            if (typeDuMessage != null)
            {
                Type typeDeLaCommande = _listeDesCommandes[typeDuMessage];
                return exécuterLaCommandeDeType(typeDeLaCommande, message);
            }
            return ReponseRequete.générerUnEchec(Ressource.Commandes.ERREUR_TYPE_MESSAGE);
        }

        private static readonly IDictionary<Type, Type> _listeDesCommandes = new Dictionary<Type, Type>()
        {
            {typeof(IUtilisateurConnecteMessage), typeof(UtilisateurConnecteRequete)},
            {typeof(IRechercheGeneraleConstatMessage), typeof(RechercheGeneraleConstatRequete)},
            {typeof(IRechercheAvanceConstatMessage), typeof(RechercheAvanceConstatRequete)},
            {typeof(IDetailConstatMessage), typeof(DetailConstatRequete)},
            {typeof(IRechercheClientMessage), typeof(RechercheClientRequete)},
            {typeof(IDetailClientMessage), typeof(DetailClientRequete)},
            {typeof(IRechercheUtilisateurMessage), typeof(RechercheUtilisateurRequete)},
            {typeof(IDetailUtilisateurMessage), typeof(DetailUtilisateurRequete)},
            {typeof(IParametrageMessage), typeof(ParametrageRequete)},
            {typeof(IGenererJetonMessage), typeof(GenererJetonRequete)},
            {typeof(ITelechargerFichierMessage), typeof(TelechargerFichierRequete)}
        };

        private static Type trouverLeTypeDuMessage(IMessageRequete message)
        {
            Type[] interfaces = message.GetType().GetInterfaces();
            return interfaces.FirstOrDefault(x => _listeDesCommandes.ContainsKey(x));
        }

        private static ReponseRequete exécuterLaCommandeDeType(Type typeDeLaCommande, IMessageRequete message)
        {
            try
            {
                object commande = Activator.CreateInstance(typeDeLaCommande);
                string nomDeLaMéthode = typeof(IRequete<IMessageRequete>).GetMethods().FirstOrDefault().Name;
                MethodInfo methodInfo = typeDeLaCommande.GetMethod(nomDeLaMéthode);
                object[] paramètres = new object[] { message };
                object réponse = methodInfo.Invoke(commande, paramètres);
                return (ReponseRequete)réponse;
            }
            catch (Exception)
            {
                //TODO : log
                return ReponseRequete.générerUnEchec(Ressource.Commandes.ERREUR_EXCEPTION_BUS_COMMANDE);
            }
        }
    }
}
