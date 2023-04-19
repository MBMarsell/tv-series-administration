namespace webapi.Models
{
    public class UploadFileModel
    {
        public static string[] ReadFile()
        {
            var result = System.IO.File.ReadAllLines("./tv_shows.txt");
            return result;
        }
    }
}
