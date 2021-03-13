using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoginnigTypesLessonHW.Services
{
    class CloudFIleService
    {
        public static CloudFile CreateRootFolder()
        {
            CloudFile root = new CloudFile()
            {
                IsFolder = true,
                Parent = Guid.Empty,
                FileName = "Root"
            };

            return root;
        }

        internal static CloudFile CreatFolder(string folderName, CloudFile currentFile)
        {
            CloudFile folder = new CloudFile()
            {
                FileName = folderName,
                IsFolder = true,
                Parent = currentFile.Id,

            };
            return folder;
        }

        public static string GetFileName(string fullname)
        {
            var list = fullname.Split(@"\");
            return list.Last();
        }

        public static CloudFile CreateFile(string fullName, CloudFile currentFile)
        {
            CloudFile file = new CloudFile()
            {
                FullPath = fullName,
                Parent = currentFile.Id,
                FileName = CloudFIleService.GetFileName(fullName),
                IsFolder = false,
            };

            return file;
        }
    }
}
