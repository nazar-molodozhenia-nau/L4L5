using System.Collections.Generic;

namespace BLL_Domains {
    public class FolderDomain {

        public int Id { get; set; }
        public int IdFolderParent { get; set; }
        public int IdStorageParent { get; set; }
        public bool OrDirectStorageParent { get; set; }

        public string Name { get; set; }
        public string Path { get; set; }

        public List<FolderDomain> ListFolders { get; set; }
        public List<FileDomain> ListFiles { get; set; }

    }
}