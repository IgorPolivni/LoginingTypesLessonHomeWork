using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore;

namespace LoginnigTypesLessonHW
{
    public class CloudFile
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string FullPath { get; set; }
        public string FileName { get; set; }
        public bool IsFolder { get; set; }
        public Guid Parent { get; set; }


    }
}
