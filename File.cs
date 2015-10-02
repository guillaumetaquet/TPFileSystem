using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPFileSystem
{
    public class File
    {
        public string Nom { get; set; }
        Directory parent;
        public int permission = 4;

        // Constructeur

        public File(string nom, Directory parent)
        {
            this.Nom = nom;
            this.parent = parent;
        }


        public File Cd(string name)
        {
             File retour = null;

            if (this is Directory)
            {
                foreach (File file in ((Directory)this).Ls())
                {
                    if (file.Nom == name)
                    {
                        retour = file;
                    }
                }
            }
       
            return retour;
        }

        public string GetPath()
        {
            String path = "";
            if (this.parent != null)
            {
                path += this.parent.GetPath();
            }
            if (this.GetType() != typeof(File))
            {
                path += this.Nom + "/";
            }
            else
            {
                path += this.Nom;
            }
            
            return path;
        }
        
        public File GetRoot()
        {
            File parent = null;
            if (this.parent != null)
            {
                parent = this.parent.GetRoot();
            }
            else
            {
                parent = this;
            }
            return parent;
        }


        public bool RenameTo(string newName)
        {
            this.Nom = newName;
            return true;
        }


        public File GetParent()
        {
            File parent = null;
            if (this.parent != null)
            {
                parent = this.parent;
            }
            else
            {
                Console.WriteLine("Je suis à la racine et je veux pas exploser.");
                return this;
            }
            return parent;
        }


        public bool IsDirectory()
        {
            return (this.GetType() == typeof(Directory));
        }


        public bool IsFile()
        {
            return (this.GetType() == typeof(File));
        }


        public string GetName()
        {
            return this.Nom;
        }


        public void Chmod(int permission)
        {
            this.permission = permission;
        }


        public bool CanWrite()
        {
            return (this.permission & 2) > 0;
        }


        public bool CanExecute()
        {
            return (this.permission & 1) > 0;
        }


        public bool CanRead()
        {
            return (this.permission & 4) > 0;
        }

        public string GetPermissions()
        {
            string permissions = "";

            
            if (this.IsDirectory()) {
                permissions += "d";
            }
            else {
                permissions += "-";
            }


            // ternaire
            //permissions += (this.isDirectory()) ? "d" : "-";
            permissions += (this.CanRead()) ? "r" : "-"; 
            permissions += (this.CanWrite()) ? "w" : "-";
            permissions += (this.CanExecute()) ? "x" : "-";
            
            return permissions;
        }
    }
}
