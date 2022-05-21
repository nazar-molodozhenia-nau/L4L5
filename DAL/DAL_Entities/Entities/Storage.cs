using System.Collections.Generic;

namespace DAL_Entities {
    public class Storage {

        public int Id { get; set; }

        public TypesOfStorage TypesOfStorage { get; set; }

        public SpecificType SpecificType { get; set; }

        public List<Folder> ListFolders { get; set; }

    }
}