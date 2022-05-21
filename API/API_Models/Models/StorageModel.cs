using System.Collections.Generic;

namespace API_Models {
    public class StorageModel : ObservableObject {

        public int Id { get; set; }

        public TypesOfStorage TypesOfStorage { get; set; }

        public SpecificType SpecificType { get; set; }

        public List<FolderModel> ListFolders { get; set; }

    }
}
