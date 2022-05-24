namespace API_Models {
    public class FileModel {

        public int Id { get; set; }
        public int IdFolderParent { get; set; }
        public int IdStorageParent { get; set; }
        public bool OrDirectStorageParent { get; set; }

        public string Type { get; set; }

        public string Name { get; set; }
        public string Path { get; set; }

    }
}