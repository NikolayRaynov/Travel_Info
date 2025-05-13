using Travel_Info.Services.Data.Interfaces;

namespace Travel_Info.Services.Data
{
    public class FileService : IFileService
    {
        public string SanitizeFolderName(string folderName)
        {
            char[] invalidFileNameChars = Path.GetInvalidFileNameChars();
            char[] invalidPathChars = Path.GetInvalidPathChars();

            string sanitized = new string(folderName
                .Where(c => !invalidFileNameChars.Contains(c) && !invalidPathChars.Contains(c))
                .ToArray());

            sanitized = System.Text.RegularExpressions.Regex.Replace(sanitized, @"[^a-zA-Z0-9_-]", "");

            return sanitized;
        }
    }
}
