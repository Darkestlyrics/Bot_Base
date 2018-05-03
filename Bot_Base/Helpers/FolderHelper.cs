using Bot_Base.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot_Base.Helpers {
    static class FolderHelper {

        public static void CreateDirStructure() {
            createDirStructure();
        }
        private static void  createDirStructure() {
            string path = Path.Combine(AppState.Path, "Plugins");
           if (!Directory.Exists(path)) Directory.CreateDirectory(path);
        }
        
    }
}
