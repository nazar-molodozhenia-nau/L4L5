using System.Collections.Generic;

namespace DAL_Entities {
    public class Storage {

        public int Id { get; set; }

        public string Owner { get; set; }
        public SpecificType SpecificType { get; set; }

        public List<Folder> ListFolders { get; set; }
        public List<File> ListFiles { get; set; }

    }
}