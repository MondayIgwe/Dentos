using Boa.Constrictor.Screenplay;
using System;
using System.IO;
using System.Linq;

namespace Elite3E.PageObjects.Interaction.CommonInteraction
{
    public class DownloadFile : IQuestion<bool>
    {
        public string FileName { get; }

        private DownloadFile(string fileName)
        {
            FileName = fileName;
        }

        public static DownloadFile isFIleDownloaded(string filename) => new(filename);

        public bool RequestAs(IActor actor)
        {
            string downloadPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\Downloads\";

            var firstFile = Directory.GetFiles(downloadPath)
                           .FirstOrDefault(fp => fp.Contains(FileName));
            if (firstFile != default)
            {
                var fileInfo = new FileInfo(firstFile);
                var isFresh = DateTime.Now - fileInfo.LastWriteTime < TimeSpan.FromMinutes(3);
                File.Delete(firstFile);
                //Check the file that are downloaded in the last 3 minutesreturn isFresh;
                return isFresh;
            }

            return false;
            ;

        }
    }
}
