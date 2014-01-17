function openMoreFilter() {
    $('#filtreplus').slideToggle();

}

function AfficherListeClients() {
    $('#fichierclient').slideToggle();

}

function AfficherUsers(lediv) {
    if ($(lediv).next(".Utilisateur").attr('style') == 'display: block;') {
        $(lediv).next(".Utilisateur").slideUp(200);
    } else {
        $(".Utilisateur").each(function () {
            $(this).slideUp(200);
        });
        $(lediv).next(".Utilisateur").slideToggle(200);
    }
    $(".Utilisateur").on("click", function () {
        $(this).slideUp(200);
    });
}

function AfficherConstat(lediv) {
    if ($(lediv).next(".Fichier").attr('style') == 'display: block;') {
        $(lediv).next(".Fichier").slideUp(200);
    } else {
        $(".Fichier").each(function () {
            $(this).slideUp(200);
        });
        $(lediv).next(".Fichier").slideToggle(200);
    }
}

function AfficherContratDuClient(lediv) {
    if ($(lediv).next(".fichierclientid").attr('style') == 'display: block;') {
        $(lediv).next(".fichierclientid").slideUp(200);
    } else {
        $(".fichierclientid").each(function () {
            $(this).slideUp(200);
        });
        $(lediv).next(".fichierclientid").slideToggle(200);
    }
    $(".fichierclientid").on("click", function () {
        $(lediv).slideUp(200);
    });
}

function AfficherUtilisateurs() {
    $('#utilisateurs').slideToggle();

}

function AfficherCompte() {
    $('#compte').slideToggle();

}

function ChargerListeClients(NbPage) {
    var données = {};
    var réponseEnJson;
    données.Texte = '';
    données.NuméroPage = NbPage;
    données.NombreDElémentsParPage = 10;
    $.get('/Api/Client/Recherche', données, function (réponse) {
        réponseEnJson = JSON.parse(réponse);
        console.log(réponseEnJson);
        for (var i = 0; i < réponseEnJson.Résultat.length; i++) {
            var texte='<div class="clientgeneral" onclick="AfficherContratDuClient(this)"><li class="client_list list-group-item">' + réponseEnJson.Résultat[i].NomClient + ' ' + réponseEnJson.Résultat[i].PrénomClient + '<span class="badge">' + réponseEnJson.Résultat[i].Constats.length + '</span></li></div>' +
                '<div class="fichierclientid" style="display:none">' 
            if (réponseEnJson.Résultat[i].Constats.length == 0) {
                texte += '<il class="list-group-item">Pas de constat</li>'
            } else {
                for (var j = 0; j < réponseEnJson.Résultat[i].Constats.length; j++) {

                    texte += '<il class="list-group-item">' + réponseEnJson.Résultat[i].Constats[j].Date + '</li>'
                    
                }
                texte += '</div>'
                console.log(texte);
            }
                
            $('div#lesclients').append(texte);
        }
    });

}

function SelectListeClients() {
    var données = {};
    var réponseEnJson;
    données.Texte = '';
    données.NuméroPage = 1;
    données.NombreDElémentsParPage = 100;
    $.get('/Api/Client/Recherche', données, function (réponse) {
        réponseEnJson = JSON.parse(réponse);
        for (var i = 0; i < réponseEnJson.Résultat.length; i++) {
            $('select#Clients').append(
            '<option value="' + réponseEnJson.Résultat[i].IdClient + '">' + réponseEnJson.Résultat[i].NomClient + ' ' + réponseEnJson.Résultat[i].PrénomClient + '</option>'
            )
        }
    });

}

function SelectListeUsers() {
    var donnéesHuissier = {};
    var donnéesSecretaire = {};
    var réponseHuissier;
    var réponseSecretaire;
    donnéesHuissier.Nom = '';
    donnéesHuissier.Type = 1;
    donnéesHuissier.NuméroPage = 1;
    donnéesHuissier.NombreDElémentsParPage = 100;
    $.get('/Api/Utilisateur/Rechercher', donnéesHuissier, function (réponse) {

        réponseHuissier = JSON.parse(réponse);
        for (var i = 0; i < réponseHuissier.Résultat.length; i++) {
            $('select#mod-use-id').append(
            '<option value="' + réponseHuissier.Résultat[i].IdUtilisateur + '">' + réponseHuissier.Résultat[i].Nom + ' ' + réponseHuissier.Résultat[i].Prénom + '</option>'
            )
        }
    });
    donnéesSecretaire.Nom = '';
    donnéesSecretaire.Type = 2;
    donnéesSecretaire.NuméroPage = 1;
    donnéesSecretaire.NombreDElémentsParPage = 100;
    $.get('/Api/Utilisateur/Rechercher', donnéesSecretaire, function (réponse) {
        réponseSecretaire = JSON.parse(réponse);
        for (var i = 0; i < réponseSecretaire.Résultat.length; i++) {
            $('select#mod-use-id').append(
            '<option value="' + réponseSecretaire.Résultat[i].IdUtilisateur + '">' + réponseSecretaire.Résultat[i].Nom + ' ' + réponseSecretaire.Résultat[i].Prénom + '</option>'
            )
        }
    });

}



function SelectListeConstats() {
    var données = {};
    var réponseConstat;
    données.Texte = '';
    données.NuméroPage = 1;
    données.NombreDElémentsParPage = 100;
    $.get('/Api/Constat/Recherche', données, function (réponseConstat) {
        réponseConstat = JSON.parse(réponseConstat);
        for (var i = 0; i < réponseConstat.Résultat.length; i++) {
            $('select#IdConstat').append(
            '<option value="' + réponseConstat.Résultat[i].IdConstat + '">' + réponseConstat.Résultat[i].Date + '  ' + réponseConstat.Résultat[i].NomClient + ' ' + réponseConstat.Résultat[i].PrénomClient + '</option>'
            )
        }
    });

}
function deconnexion() {
    $.post('../Api/Authentification/Deconnecter', function (réponse) {
        console.log(réponse);
        window.location = "/";
    });
}
function AfficherModifConstat(lediv){
        $(".constatgeneral").each(function () {
            $(this).slideUp(200);
        });
        $('.ModificationConstat').slideToggle(200);
};

function AfficherModifUserApplication(lediv) {
    $(".constatgeneral").each(function () {
        $(this).slideUp(200);
    });
    $('.ModificationConstat').slideToggle(200);
};

function TelechargerFichierAudio(IdConstat,IdFichier) {
    var xhr = new XMLHttpRequest();
    xhr.open('GET', '/Api/Constat/Telecharger?IdConstat=' + IdConstat + '&IdFichier=' + IdFichier, true);
    xhr.responseType = 'arraybuffer';

    xhr.onload = function (e) {
        if (this.status == 200) {;
            var blob = new Blob([this.response], { type: "application/octet-stream" });
            var url = URL.createObjectURL(blob);
            var audio = document.createElement("audio");
            audio.id = 'audio1';
            audio.src = url;
            audio.height = 60;
            audio.onload = function (e) {
                window.URL.revokeObjectURL(this.src);
            };
            $('div#audio' + IdFichier).append("<audio controls><source src='"+url+"' /><p class='warning'>Le format *.wav n'est pas pris en charge par votre navigateur</p></audio>");
            $(audio).prop('controls', true);

        }
    };
    xhr.send();

};


function TelechargerFichier(IdConstat, IdFichier) {
    var données = {};
    données.IdConstat = IdConstat;
    données.IdFichier = IdFichier;
    var URL = '/Api/Constat/Telecharger?IdConstat=' + données.IdConstat + '&IdFichier=' + données.IdFichier;
    $('div#fichier' + IdFichier).append("<a href=" + URL + " source='_blank'>Téléchargez</a>");
    
    return URL;
};

function ChargerListeConstats(NbPage) {
    var donnéesFichier = {};
    var donnéesConstat = {};
    var réponseConstat;
    var réponseFichier;
    donnéesConstat.Texte = '';
    donnéesConstat.NuméroPage = NbPage;
    donnéesConstat.NombreDElémentsParPage = 10;


    $.get('/Api/Constat/Recherche', donnéesConstat, function (réponseConstat) {
        réponseConstat = JSON.parse(réponseConstat);
        for (var i = 0; i < réponseConstat.Résultat.length; i++) {
            var texte = '';
            var IdClient = réponseConstat.Résultat[i].IdConstat;
            var compteur = i;
            donnéesFichier.IdConstat = réponseConstat.Résultat[i].IdConstat;
            $.get('/Api/Constat', donnéesFichier, function (réponseFichier) {
                réponseFichier = JSON.parse(réponseFichier);
                for (var i = 0; i < réponseConstat.Résultat.length; i++) {
                    
                    if (réponseConstat.Résultat[i].IdConstat == réponseFichier.Résultat.IdConstat) {
                        texte = ''
                        texte += '<div class="constatgeneral" onclick="AfficherConstat(this)"><li class="constat_list list-group-item" >' + réponseConstat.Résultat[i].NomClient + ' ' + réponseConstat.Résultat[i].Date + '</li></div>' +
'<div class="Fichier" style="display:none">' +
'<ul class="constat_fichier_list list-group">'
                     //   console.log(réponseFichier.Résultat.Fichiers.length);
                        if (réponseFichier.Résultat.Fichiers.length == 0) {
                            
                            texte+='<li class="list-group-item"> Pas de fichier</li>';
                        }
                        else {
                            for (var j = 0; j < réponseFichier.Résultat.Fichiers.length; j++) {

                                if (réponseFichier.Résultat.Fichiers[j].Type == "Audio") {
                                    TelechargerFichierAudio(réponseFichier.Résultat.IdConstat, réponseFichier.Résultat.Fichiers[j].Id);
                                    texte += '<li class="list-group-item">' + réponseFichier.Résultat.Fichiers[j].Nom
                                    texte += "<div id='audio" + réponseFichier.Résultat.Fichiers[j].Id + "'></div>"
                                }
                                else {
                                    var url = TelechargerFichier(réponseFichier.Résultat.IdConstat, réponseFichier.Résultat.Fichiers[j].Id);
                                    var IdConstat = réponseFichier.Résultat.IdConstat;
                                    var IdFichier = réponseFichier.Résultat.Fichiers[j].Id;
                                    texte += '<li class="list-group-item" ><a href="'+url+'" target="_blank" >' + réponseFichier.Résultat.Fichiers[j].Nom +'</a>'
                                }
                                texte +='</li>'
                            }

                        }
                        texte += '</ul>' +
                        '</div>'
                    }
                    
                }
                $('div#lesconstats').append(texte);
            })
        }
    });
}

function AfficherModifUsers(lediv) {
    $(".usergeneral").each(function () {
        $(this).slideUp(200);
    });


    $('.ModificationUtilisateur').slideToggle(200);
    $(lediv).next(".Utilisateur").slideUp(200);
}



function ChargerListeUsers(NbPage) {
    var données = {};
    var réponseEnJson;
    données.Nom = '';
    données.Type = 2;
    données.NuméroPage = NbPage;
    données.NombreDElémentsParPage = 10;
    $.get('/Api/Utilisateur/Rechercher', données, function (réponse) {

        réponseEnJson = JSON.parse(réponse);
        for (var i = 0; i < réponseEnJson.Résultat.length; i++) {
            $('div#lesusers').append(
                    '<div class="usergeneral" onclick="AfficherUsers(this)"><li class="client_list list-group-item" >' + réponseEnJson.Résultat[i].Nom + ' ' + réponseEnJson.Résultat[i].Prénom + '</li></div>' +
                        '<div class="Utilisateur">' +
                        '<ul class="client_fichier_list list-group">' +
                        '<li class="list-group-item">' + réponseEnJson.Résultat[i].Type + '</li>' +
                        '</ul>' +
                        '</div>'
                )
        }
    });
}

function CreationClient() {
    $.post("/urlWS", {
        civilite: $("#civiliteClientCreation").val(),
        nom: $("#nomClientCreation").val(),
        prenom: $("#prenomClientCreation").val(),
        numeroVoie: $("#numVoieClientCreation").val(),
        repetitionVoie: $("#repetitionVoieClientCreation").val(),
        typeVoie: $("#typeVoieClientCreation").val(),
        nomVoie: $("#nomVoieClientCreation").val(),
        complementVoie: $("#complementVoieClientCreation").val(),
        cp: $("#cpClientCreation").val(),
        bp: $("#bpClientCreation").val(),
        ville: $("#villeClientCreation").val()
    }, function (reponse) {
        if (reponse.Reussite) {
            $("#civiliteClientCreation").empty();
            $("#nomClientCreation").empty();
            $("#prenomClientCreation").empty();
            $("#numVoieClientCreation").empty();
            $("#repetitionVoieClientCreation").empty();
            $("#typeVoieClientCreation").empty();
            $("#nomVoieClientCreation").empty();
            $("#complementVoieClientCreation").empty();
            $("#cpClientCreation").empty();
            $("#bpClientCreation").empty();
            $("#villeClientCreation").empty();
            $("#messageCreationClient").html("<span class='label label-success'>Création effectuée avec succès</span>");
        }
    }, "json");
}

function ModificationClient() {
    $.post("/urlWS", {
        civilite: $("#civiliteClientModification").val(),
        nom: $("#nomClientModification").val(),
        prenom: $("#prenomClientModification").val(),
        numeroVoie: $("#numVoieClientModification").val(),
        repetitionVoie: $("#repetitionVoieClientModification").val(),
        typeVoie: $("#typeVoieClientModification").val(),
        nomVoie: $("#nomVoieClientModification").val(),
        complementVoie: $("#complementVoieClientModification").val(),
        cp: $("#cpClientModification").val(),
        bp: $("#bpClientModification").val(),
        ville: $("#villeClientModification").val()
    }, function (reponse) {
        if (reponse.Reussite) {
            $("#civiliteClientModification").empty();
            $("#nomClientModification").empty();
            $("#prenomClientModification").empty();
            $("#numVoieClientModification").empty();
            $("#repetitionVoieClientModification").empty();
            $("#typeVoieClientModification").empty();
            $("#nomVoieClientModification").empty();
            $("#complementVoieClientModification").empty();
            $("#cpClientModification").empty();
            $("#bpClientModification").empty();
            $("#villeClientModification").empty();
            $("#messageModificationClient").html("<span class='label label-success'>Modification effectuée avec succès</span>");
        }
    }, "json");
}

function CreationUtilisateur() {
    $.post("/urlWS", {
        civilite: $("#civiliteUtilisateurCreation").val(),
        nom: $("#nomUtilisateurCreation").val(),
        prenom: $("#prénomUtilisateurCreation").val(),
        email: $("#emailUtilisateurCreation").val(),
        motDePasse: $("#motDePasseUtilisateurCreation").val()
    }, function (reponse) {
        if (reponse.Reussite) {
            $("#civiliteUtilisateurCreation").empty();
            $("#nomUtilisateurCreation").empty();
            $("#prénomUtilisateurCreation").empty();
            $("#emailUtilisateurCreation").empty();
            $("#motDePasseUtilisateurCreation").empty();
            $("#messageCreationUtilisateur").html("<span class='label label-success'>Création effectuée avec succès</span>");
        }
    }, "json");
}

function ModificationUtilisateur() {
    $(".clientgeneral").on("click", function () {
        if ($(this).next(".fichierclientid").attr('style') == 'display: block;') {
            $(this).next(".fichierclientid").slideUp(200);
        } else {
            $(".fichierclientid").each(function () {
                $(this).slideUp(200);
            });
            $(this).next(".fichierclientid").slideToggle(200);
        }
    });
    $(".fichierclientid").on("click", function () {
        $(this).slideUp(200);
    });

    $.post("/urlWS", {
        civilite: $("#civiliteUtilisateurModification").val(),
        nom: $("#nomUtilisateurModification").val(),
        prenom: $("#prénomUtilisateurModification").val(),
        email: $("#emailUtilisateurModification").val(),
        motDePasse: $("#motDePasseUtilisateurModification").val()
    }, function (reponse) {
        if (reponse.Reussite) {
            $("#civiliteUtilisateurModification").empty();
            $("#nomUtilisateurModification").empty();
            $("#prénomUtilisateurModification").empty();
            $("#emailUtilisateurModification").empty();
            $("#motDePasseUtilisateurModification").empty();
            $("#messageModificationUtilisateur").html("<span class='label label-success'>Modification effectuée avec succès</span>");
        }
    }, "json");
}
