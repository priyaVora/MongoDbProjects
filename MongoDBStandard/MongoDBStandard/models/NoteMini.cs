using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDBStandard.models
{
    public class NoteMini
    {
        public NoteMini(string title = null, string guid = null)
        {
            Title = title;
            GUID = guid;
        }

        public string Title { get; set; }
        public string GUID { get; set; }
    }
}
