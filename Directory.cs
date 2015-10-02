using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPFileSystem
{
    public class Directory : File
    {
        public List<File> ListeFiles;

        // Constructeurs
        public Directory(string nom, Directory parent) : base (nom,parent)
        {
            this.Nom = nom;
            ListeFiles = new List<File>();

        }


        public bool Mkdir(string name)
        {
            bool retour = false;
            if (this.CanWrite())
            {
                
                ListeFiles.Add( new Directory (name, this ));
                retour = true;
            }
            return retour;
        }


        public List<File> Ls()
        {
            return this.ListeFiles;
        }

        public bool createNewFile(string name)
        {
            bool retour = false;
            if (this.CanWrite())
            {
                ListeFiles.Add(new File(name, this));
                retour = true;
            }
            return retour;
        }


        public List<File> Search(string name)
        {
            List<File> resultats = new List<File>();

            foreach (File file in this.Ls())
            {
                if (file.GetName() == name)
                {
                    resultats.Add(file);
                }

                if (file is Directory)
                {
                    foreach (File subFile in ((Directory)file).Search(name))
                    {
                        resultats.Add(subFile);
                    }
                }
            }
            return resultats;
        }


        public bool Delete(string name)
        {
            bool deleted = false;
            foreach (File f in this.Ls())
            {
                if (f.GetName() == name)
                {
                    this.ListeFiles.Remove(f);
                    deleted = true;
                    break;
                }
            }

            return deleted;
        }

    }
}
