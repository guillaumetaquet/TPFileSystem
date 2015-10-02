using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPFileSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            string utilisateur = "Néo";
            string saisiUtilisateur = "";
            // Initialisation du répertoire par défaut lors du démarrage de l'application
            File RepertoireCourant = new Directory("",null);


            // Affichage du texte lors du démarrage de l'application
            Console.WriteLine("Welcome to FileSystem called XUNIL");
            Console.WriteLine("Write what you want to do when it\'s requested");
            Console.WriteLine("If you want to close CMD window, please write : exit");
            Console.WriteLine("Try to find the easter-egg. It's related to 'Matrix' \n");

            while (saisiUtilisateur != "exit")
            {
                Console.Write(utilisateur +" "+RepertoireCourant.GetPath() + " $ ");
                saisiUtilisateur = Console.ReadLine();
                string[] saisiDecoupe = saisiUtilisateur.Split(' '); // Permet de ranger chaque argument dans une case d'un tableau, permettant notamment de distinguer chaque commande

                // Chaque case fera appel à la commande saisi par l'utilisateur. Si la commande n'existe pas, un message d'erreur apparaitra, sinon la commande sera exécuté.
                switch (saisiDecoupe[0])
                {
                    case "cd":
                        if (saisiDecoupe.Length == 1)
                        {
                            Console.WriteLine("Vous n'avez spécifié aucun fichier ou dossier à  joindre.");
                        }
                        else
                        {
                            File file = RepertoireCourant.Cd(saisiDecoupe[1]);
                            if (file != null)
                            {
                                RepertoireCourant = file;
                            }
                            else
                            {
                                Console.WriteLine("Aucun fichier ou dossier ne porte ce nom.");
                            }
                        }
                        break;


                    case "ls":
                        String liste = "";
                        if (RepertoireCourant.CanRead())
                        {
                            if (RepertoireCourant.IsDirectory())
                            {
                                foreach (File f in ((Directory)RepertoireCourant).Ls())
                                {
                                    liste += f.GetPermissions();
                                    liste += " " + f.Nom + "\n";
                                }
                            }
                            else
                            {
                                liste += RepertoireCourant.GetPermissions();
                                liste += " " + RepertoireCourant.Nom + "\n";
                                Console.WriteLine("C'est un fichier.");
                            }
                        }else
                        {
                            Console.WriteLine("Vous n'avez pas la permission de lire dans le répertoire " + RepertoireCourant.GetPath());
                        }
                        Console.Write(liste);
                        break;


                    case "mkdir":                    
                        if (saisiDecoupe.Length < 2)
                        {
                            Console.WriteLine("Commande incomplète.");
                        }
                        else
                        {
                            if (RepertoireCourant.CanWrite())
                            {
                                if (saisiDecoupe[1] == string.Empty)
                                {
                                    Console.WriteLine("Vous n'avez spécifié aucun nom de dossier.");
                                }
                                else
                                {
                                    if (RepertoireCourant.IsDirectory())
                                    {
                                        if (!((Directory)RepertoireCourant).Mkdir(saisiDecoupe[1]))
                                        {
                                            Console.WriteLine("Erreur lors de la création du dossier.");
                                        }
                                        else
                                        {
                                            Console.WriteLine("Dossier crée avec succès.");
                                        }
                                    }
                                    else if (RepertoireCourant.IsFile())
                                    {
                                        Console.WriteLine("Vous ne pouvez pas créer de dossier dans un fichier.");
                                    }
                                }
                            }else
                            {
                                Console.WriteLine("Vous n'avez pas la permission d'écrire dans "+ RepertoireCourant.GetPath());
                            }
                        }
                        break;


                    case "path":
                        Console.WriteLine(RepertoireCourant.GetPath());
                        break;


                    case "root":
                        RepertoireCourant = RepertoireCourant.GetRoot();
                        break;


                    case "rename":
                        if (saisiDecoupe.Length < 3)
                        {
                            Console.WriteLine("Il manque un ou plusieurs arguments pour que le renommage s'effectue correctement.");
                        }
                        else
                        {
                            if (RepertoireCourant.CanWrite())
                            {
                                bool renamed = false;
                                foreach (File f in ((Directory)RepertoireCourant).Ls())
                                {
                                    if (f.GetName() == saisiDecoupe[1])
                                    {
                                        renamed = f.RenameTo(saisiDecoupe[2]);
                                        break;
                                    }
                                }
                                if (renamed == false)
                                {
                                    Console.WriteLine("Aucun fichier ou dossier ayant ce nom.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Vous n'avez pas la permission de renommer " + saisiDecoupe[1] + ".");
                            }


                        }
                        break;


                    case "create":
                        if (saisiDecoupe.Length < 2)
                        {
                            Console.WriteLine("Il manque le nom du fichier que vous voulez créer.");
                        }
                        else
                        {
                            if(RepertoireCourant.CanWrite())
                            {
                                if (!((Directory)RepertoireCourant).createNewFile(saisiDecoupe[1])) // Le if exécute la méthode createNewFile et si la valeur 
                                                                                                    // retourné est true; ça crée le fichier sinon ça affiche l'erreur
                                {
                                    Console.WriteLine("Erreur lors de la création du fichier.");
                                }
                                else
                                {
                                    Console.WriteLine("Fichier crée avec succès.");
                                }
                            }else
                            {
                                Console.WriteLine("Vous n'avez pas la permission d'écrire dans " + RepertoireCourant.GetPath());
                            }
                            
              
                        }
                        break;


                    case "parent":
                        RepertoireCourant = RepertoireCourant.GetParent();
                        break;

                    case "search":
                        if (saisiDecoupe.Length < 2)
                        {
                            Console.WriteLine("Il manque l'argument pour que la commande soit exécuté.");
                        }
                        else
                        {
                            if (RepertoireCourant.CanRead())
                            {
                                if (RepertoireCourant.IsDirectory())
                                {
                                    foreach (File result in ((Directory)RepertoireCourant).Search(saisiDecoupe[1]))
                                    {
                                        Console.WriteLine(result.GetPath());
                                    }
                                }
                                else if (RepertoireCourant.IsFile())
                                {
                                    Console.WriteLine("Vous ne pouvez pas rechercher dans un fichier.");
                                }
                            }
                        }
                        break;


                    case "file":
                        if (RepertoireCourant.IsFile())
                        {
                            Console.WriteLine("C'est un fichier.");
                        }
                        else
                        {
                            Console.WriteLine("Ce n'est pas un fichier.");
                        }
                        break;


                    case "directory":
                        if (RepertoireCourant.IsDirectory())
                        {
                            Console.WriteLine("C'est un dossier.");
                        }
                        else
                        {
                            Console.WriteLine("Ce n'est pas un dossier.");
                        }
                        break;


                    case "name":
                        Console.WriteLine(RepertoireCourant.GetName());
                        break;


                    case "delete":
                        if (saisiDecoupe.Length < 2)
                        {
                            Console.WriteLine("Il n'y pas pas assez d'arguments dans votre commande.");
                        }
                        else
                        {
                            bool deleted = false;
                            deleted = ((Directory)RepertoireCourant).Delete(saisiDecoupe[1]);
                            if (!deleted)
                            {
                                Console.WriteLine("Aucun fichier ou dossier ayant ce nom.");
                            }
                        }
                        break;


                    case "chmod":
                        if (saisiDecoupe.Length < 2 || saisiDecoupe.Length == 1)
                        {
                            Console.WriteLine("Il n'y pas pas assez d'arguments dans votre commande.");
                        }
                        else
                        {

                                RepertoireCourant.Chmod(int.Parse(saisiDecoupe[1]));
                            
                        }
                        break;


                    case "matrix": // Petit easter-egg sympatique :-)
                        int aleatoire;
                        Random r = new Random();
                        Console.WriteLine("Bienvenue dans la matrice de XUNIL. Ceci est un easter-egg totalement inutile et qui bloquera l'éxécutation de l'OS le plus abouti du monde.");
                        for (int i = 5; i >= 0; i--)
                        {
                            Console.Write(i + "\r");
                            System.Threading.Thread.Sleep(1000);
                        }
                        Console.WriteLine("");

                        while (true)
                        {

                            aleatoire = r.Next(0, 2);
                            Console.Write(aleatoire);
                        }


                    default:
                        Console.WriteLine("Votre commande n'est pas reconnu par le système XUNIL.");
                        break;

                }
            }
        }
    }
}
