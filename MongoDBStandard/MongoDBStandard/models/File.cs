using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDBStandard.models
{
    public class File
    {
        public string GUID { get; set; }
        public string Extension { get; set; }
        public string Name { get; set; }
        public Dictionary<UserAccount, Permission> Users { get; set; }
        public byte[] Content { get; set; }
    }
}
