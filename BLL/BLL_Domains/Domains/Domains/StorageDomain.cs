using System.Collections.Generic;

namespace BLL_Domains {
    public class StorageDomain {

        public int Id { get; set; }

        public string Owner { get; set; }
        public SpecificType SpecificType { get; set; }

        public List<FolderDomain> ListFolders { get; set; }
        public List<FileDomain> ListFiles { get; set; }

    }
}