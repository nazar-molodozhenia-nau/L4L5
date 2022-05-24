using System.Collections.Generic;

namespace API_Models {
    public class FolderModel {

        public int Id { get; set; }
        public int IdFolderParent { get; set; }
        public int IdStorageParent { get; set; }

        public string Name { get; set; }
        public string Type { get; set; } = "folder";
        public string Path { get; set; }

        public List<FolderModel> ListFolders { get; set; }
        public List<FileModel> ListFiles { get; set; }

    }
}