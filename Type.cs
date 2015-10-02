using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPFileSystem
{
    class Type
    {
        public List<Directory> Dossiers { get; set; }
        public List<File> Fichiers { get; set; }

        public string Substring(int startIndex, int length);
    }
}
