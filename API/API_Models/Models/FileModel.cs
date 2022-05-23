namespace API_Models {
    public class FileModel {

        public int Id { get; set; }

        public string Name { get; set; }
        public string Type { get; set; } = "file";
        public string Path { get; set; }

    }
}