using System.Collections.Generic;

namespace DAL_Entities {
    public class Folder {

        public int Id { get; set; }

        public string Name { get; set; }
        public string Path { get; set; }

        public List<Folder> ListFolders { get; set; }
        public List<File> ListFiles { get; set; }
    }
}