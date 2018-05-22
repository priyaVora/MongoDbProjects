using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDBStandard.models
{
    public class Note
    {
        public Note(string title = null, string content = null, string guid = null)
        {
            Title = title;
            Content = content;
            GUID = guid;
        }

        public string Title { get; set; }
        public string Content { get; set; }
        public string GUID { get; set; }
    }
}
