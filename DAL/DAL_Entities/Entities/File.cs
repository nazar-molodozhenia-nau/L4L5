namespace DAL_Entities {
    public class File {

        public int Id { get; set; }
        public int IdParent { get; set; }

        public string Type { get; set;}

        public string Name { get; set; }
        public string Path { get; set; }

    }
}