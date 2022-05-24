using System.Linq;
using System.Collections.Generic;

namespace API_Models {
    public enum TypeFile { txt, pdf, doc, docx, avi, mp3, mp4 }

    public static class SearchTypeFile {

        public static IEnumerable<T> GetEnumValues<T>() {
            return TypeFile.GetValues(typeof(TypeFile)).Cast<T>();
        }

    }

}