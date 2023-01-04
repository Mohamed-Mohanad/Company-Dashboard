namespace Company.PL.Helper
{
    public class DocumentSettings
    {
        public static string UploadFile(IFormFile file, string folderName)
        {
            //Get Located Folder Path
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/files",folderName);
            //Get File Name And Make its Name Unique
            var fileName = $"{Guid.NewGuid()}{Path.GetFileName(file.FileName)}";
            //Get File Path
            var filePath = Path.Combine(folderPath, fileName);
            //Save File As Streams
            using var fileStream = new FileStream(filePath, FileMode.Create);
            file.CopyTo(fileStream);
            return fileName;
        }
    }
}
