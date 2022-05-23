using System.Collections.Generic;

namespace BLL_Domains {
    public class FolderDomain {

        public int Id { get; set; }
        public int IdParent { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }

        public List<FolderDomain> ListFolders { get; set; }
        public List<FileDomain> ListFiles { get; set; }

    }
}