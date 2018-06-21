using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;


namespace MongoDBStandard.models
{
    public class UserAccount
    {
        private string usernameStr;
        private string phoneNumberStr;
        private string emailStr;

        public UserAccount(List<Goal> listOfGoals, List<Note> listOfNotes, List<File> listOfFiles, string usernameStr, string phoneNumberStr, string emailStr)
        {
            ListOfGoals = listOfGoals;
            ListOfNotes = listOfNotes;
            ListOfFiles = listOfFiles;
            UserName = usernameStr;
            PhoneNumber = phoneNumberStr;
            Email = emailStr;
        }

        [BsonId]
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int Points { get; set; }
        public List<Goal> ListOfGoals { get; set; }
        public List<Note> ListOfNotes { get; set; }
        public List<File> ListOfFiles { get; set; }
    }
}
