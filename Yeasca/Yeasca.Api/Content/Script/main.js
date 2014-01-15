function openMoreFilter() {
        $('#filtreplus').slideToggle();

}

function AfficherListeClients() {
    $('#fichierclient').slideToggle();

}

function AfficherUsers(lediv) {
        if($(lediv).next(".Utilisateur").attr('style') == 'display: block;'){
            $(lediv).next(".Utilisateur").slideUp(200);
        } else {
            $(".Utilisateur").each(function(){
                $(this).slideUp(200);
            });
            $(lediv).next(".Utilisateur").slideToggle(200);
        }
    $(".Utilisateur").on("click", function() {
        $(this).slideUp(200);
    });
}

function AfficherConstat(lediv) {
         if($(lediv).next(".Fichier").attr('style') == 'display: block;'){
            $(lediv).next(".Fichier").slideUp(200);
        } else {
            $(".Fichier").each(function(){
                $(this).slideUp(200);
            });
            $(lediv).next(".Fichier").slideToggle(200);
        }
}

function AfficherContratDuClient(lediv) {
        if($(lediv).next(".fichierclientid").attr('style') == 'display: block;'){
            $(lediv).next(".fichierclientid").slideUp(200);
        } else {
            $(".fichierclientid").each(function(){
                $(this).slideUp(200);
            });
            $(lediv).next(".fichierclientid").slideToggle(200);
        }
    $(".fichierclientid").on("click", function() {
        $(lediv).slideUp(200);
    });
}

function AfficherUtilisateurs() {
    $('#utilisateurs').slideToggle();

}

function AfficherCompte() {
    $('#compte').slideToggle();

}

function ChargerListeClients() {
    $.getJSON('FichierTestClients.json', function(données) {
        $.each(données.ListeClient, function(key,val){
            $('div#lesclients').append(
                '<div class="clientgeneral" onclick="AfficherContratDuClient(this)"><li class="client_list list-group-item">'+val.nom+'<button class="btn btn-default col-xs-0 col-sm-0 col-md-0 col-lg-0" style="float:right;font-size: 10px;"><span class="glyphicon glyphicon-pencil"></span></button></li></div>' +
                    '<div class="fichierclientid" style="display:none">'+
                    '<ul class="list-group">' +
                    '<a class="client_fichier_list list-group-item" href="constatexemple.html" target="_blank">'+val.Constat[0].IDConstat+'</a>' +
                    '<a class="client_fichier_list list-group-item" href="constatexemple.html" target="_blank">'+val.Constat[1].IDConstat+'</a>' +
                    '</ul>' +
                    '</div>'
            );
        })
    })
}

function ChargerListeConstatsOld() {
    $.getJSON('FichierTestConstat.json', function(données) {
        $.each(données.ListeConstats, function(key,val){
            $('div#lesconstats').append(
                '<div class="constatgeneral" onclick="AfficherConstat(this)"><li class="constat_list list-group-item" >'+val.ref +'</li></div>'+
                '<div class="Fichier" style="display:none">'+
                '<ul class="constat_fichier_list list-group">'+
                '<li class="list-group-item"><video controls src="Rings.mp4" controls="controls">Ici la description alternative</video>'+ val.FichierConstat[0].IDFichier +'</li>'+
                '<li class="list-group-item"><audio controls src="Rings.mp4" controls="controls">Ici la description alternative</audio>'+ val.FichierConstat[1].IDFichier +'</li>'+
                '<li class="list-group-item">'+ val.FichierConstat[2].IDFichier +'</li>'+
                '<li class="list-group-item">'+ val.FichierConstat[3].IDFichier +'</li>'+
                '</ul>'+
                '</div>'
            );
        })
    })
}

function ChargerListeConstats() {
    $.getJSON('FichierTestConstat.json', function(données) {
        var texte='';
        $.each(données.ListeConstats, function(key,val){
                texte+='<div class="constatgeneral" onclick="AfficherConstat(this)"><li class="constat_list list-group-item" >'+val.ref +'</li></div>'+
                    '<div class="Fichier" style="display:none">'+
                    '<ul class="constat_fichier_list list-group">'+
                    '<li class="list-group-item"><video controls src="Rings.mp4" controls="controls">Ici la description alternative</video>'+ val.FichierConstat[0].IDFichier +'</li>'+
                    '<li class="list-group-item"><audio controls src="Rings.mp4" controls="controls">Ici la description alternative</audio>'+ val.FichierConstat[1].IDFichier +'</li>'+
                    '<li class="list-group-item">'+ val.FichierConstat[2].IDFichier +'</li>'+
                    '<li class="list-group-item">'+ val.FichierConstat[3].IDFichier +'</li>'+
                    '</ul>'+
                    '</div>'
            $('div#lesconstats').append(texte);
        })
    })
}

function AfficherModifUsers(p) {
    if($(p).next(".Utilisateur").attr('style') == 'display: block;'){
        $(p).next(".Utilisateur").slideUp(200);
    }
    console.log($(p).next(".ModificationUtilisateur"));

    $(p).next(".ModificationUtilisateur").slideToggle(200);
}
function ChargerListeUsers() {
    $.getJSON('FichierTestUsers.json', function(données) {
        $.each(données.ListeUsers, function(key,val){
            $('div#lesusers').append(
                '<div class="usergeneral" onclick="AfficherUsers(this)"><li class="client_list list-group-item" >'+val.nom+'<button class="btn btn-default col-xs-0 col-sm-0 col-md-0 col-lg-0" style="float:right;font-size: 10px;" onclick="AfficherModifUsers(this)"><span class="glyphicon glyphicon-pencil"></span></button></li></div>'+
                    '<div class="Utilisateur">'+
                    '<ul class="client_fichier_list list-group">'+
                    '<li class="list-group-item">'+val.mail+'</li>'+
                    '<li class="list-group-item">'+val.adresse+'</li>'+
                    '<li class="list-group-item">'+val.ville+'</li>'+
                    '<li class="list-group-item">'+val.CodePostal+'</li>'+
                    '<li class="list-group-item">'+val.Téléphone+'</li>'+
                    '</ul>'+
                    '</div>'+
                    '<div class="ModificationUtilisateur">'+
                        '<button type="button" class="btn-ok btn btn-default  col-xs-5 col-sm-5 col-md-5 col-lg-5" style="float:right">'+
                        '<span class="glyphicon glyphicon glyphicon-plus-sign" onclick=""></span> Valider'+
                        '</button>'+
                        '<button type="button" class="btn-return-user btn btn-default  col-xs-5 col-sm-5 col-md-5 col-lg-5" >'+
                        '<span class="glyphicon glyphicon-chevron-left"></span> Retour'+
                        '</button>'+
                        '<select id="roleUtilisateurModification" class="form-control">'+
                        '<option>Secrétaire</option>'+
                        '<option>Huissier</option>'+
                        '</select>'+
                        '<select id="civiliteUtilisateurModification" class="form-control">'+
                        '<option></option>'+
                        '<option>M</option>'+
                        '<option>Mme</option>'+
                        '<option>Mlle</option>'+
                        '</select>'+
                        '<input id="nomUtilisateurModification" type="text" class="form-control " placeholder="Nom">'+
                        '<input id="prénomUtilisateurModification" type="text" class="form-control " placeholder="Prénom">'+
                        '<input id="emailUtilisateurModification" type="email" class="form-control " placeholder="Email">'+
                    '</div>'
            )
        })
    })
}

function CreationClient() {
    $.post("/urlWS", {
        civilite : $("#civiliteClientCreation").val(),
        nom : $("#nomClientCreation").val(),
        prenom : $("#prenomClientCreation").val(),
        numeroVoie : $("#numVoieClientCreation").val(),
        repetitionVoie : $("#repetitionVoieClientCreation").val(),
        typeVoie : $("#typeVoieClientCreation").val(),
        nomVoie : $("#nomVoieClientCreation").val(),
        complementVoie : $("#complementVoieClientCreation").val(),
        cp : $("#cpClientCreation").val(),
        bp : $("#bpClientCreation").val(),
        ville : $("#villeClientCreation").val()
    }, function(reponse) {
        if(reponse.Reussite) {
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
    },"json");
}

function ModificationClient() {
    $.post("/urlWS", {
        civilite : $("#civiliteClientModification").val(),
        nom : $("#nomClientModification").val(),
        prenom : $("#prenomClientModification").val(),
        numeroVoie : $("#numVoieClientModification").val(),
        repetitionVoie : $("#repetitionVoieClientModification").val(),
        typeVoie : $("#typeVoieClientModification").val(),
        nomVoie : $("#nomVoieClientModification").val(),
        complementVoie : $("#complementVoieClientModification").val(),
        cp : $("#cpClientModification").val(),
        bp : $("#bpClientModification").val(),
        ville : $("#villeClientModification").val()
    }, function(reponse) {
        if(reponse.Reussite) {
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
    },"json");
}

function CreationUtilisateur() {
    $.post("/urlWS", {
        civilite : $("#civiliteUtilisateurCreation").val(),
        nom : $("#nomUtilisateurCreation").val(),
        prenom : $("#prénomUtilisateurCreation").val(),
        email : $("#emailUtilisateurCreation").val(),
        motDePasse : $("#motDePasseUtilisateurCreation").val()
    }, function(reponse) {
        if(reponse.Reussite) {
            $("#civiliteUtilisateurCreation").empty();
            $("#nomUtilisateurCreation").empty();
            $("#prénomUtilisateurCreation").empty();
            $("#emailUtilisateurCreation").empty();
            $("#motDePasseUtilisateurCreation").empty();
            $("#messageCreationUtilisateur").html("<span class='label label-success'>Création effectuée avec succès</span>");
        }
    },"json");
}

function ModificationUtilisateur() {
    $(".clientgeneral").on("click", function() {
        if($(this).next(".fichierclientid").attr('style') == 'display: block;'){
            $(this).next(".fichierclientid").slideUp(200);
        } else {
            $(".fichierclientid").each(function(){
                $(this).slideUp(200);
            });
            $(this).next(".fichierclientid").slideToggle(200);
        }
    });
    $(".fichierclientid").on("click", function() {
        $(this).slideUp(200);
    });

    $.post("/urlWS", {
        civilite : $("#civiliteUtilisateurModification").val(),
        nom : $("#nomUtilisateurModification").val(),
        prenom : $("#prénomUtilisateurModification").val(),
        email : $("#emailUtilisateurModification").val(),
        motDePasse : $("#motDePasseUtilisateurModification").val()
    }, function(reponse) {
        if(reponse.Reussite) {
            $("#civiliteUtilisateurModification").empty();
            $("#nomUtilisateurModification").empty();
            $("#prénomUtilisateurModification").empty();
            $("#emailUtilisateurModification").empty();
            $("#motDePasseUtilisateurModification").empty();
            $("#messageModificationUtilisateur").html("<span class='label label-success'>Modification effectuée avec succès</span>");
        }
    },"json");
}
