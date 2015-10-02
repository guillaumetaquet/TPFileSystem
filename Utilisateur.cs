using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPFileSystem
{
    class Utilisateur
    {
        // Initialisation des variables
        string nomUtilisateur;
        public string saisi; // Variable public afin de pouvoir la récupéré dans les autres classes

        // Constructeur
        public Utilisateur(string nomUtilisateur)
        {
            this.nomUtilisateur = nomUtilisateur;
        }

        // Procédure permettant de demander à l'utilisateur d'effectuer la commande qu'il souhaite
        public string LireSaisi ()
        {
           Console.Write("["+nomUtilisateur +"@localhost ~]$ ");
           saisi = Console.ReadLine();
           return saisi;
        }
    }
}
