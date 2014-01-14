﻿
using Yeasca.Commande;

namespace Yeasca.Api
{
    public class CreerSecretaireMessage : ICreerSecretaireMessage
    {
        public string Email { get; set; }
        public string MotDePasse { get; set; }
        public int Civilité { get; set; }
        public string Nom { get; set; }
        public string Prénom { get; set; }
    }
}