using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.GridFS;
using MongoDB.Driver.Linq;
using System.Data.SqlClient;
using MongoDBStandard;
using MongoDBStandard.CRUD_COMMANDS;
using MongoDBStandard.models;


namespace MongodbRND
{
    class Program
    {
        static void Main(string[] args)
        {
            MongoCommands cmd = new MongoCommands();

            UserAccount userAccount2 = new UserAccount(new List<Goal>(), new List<Note>(),new List<File>(), "Amica", "415-584-4324", "Amica78@gmail.com");
            //cmd.CreateUser(userAccount2, "Hash", "Salt");

            Note addNote = new Note();
            addNote.GUID = "1";
            addNote.Title = "2";
            addNote.Owner = userAccount2.UserName;
            addNote.Content = "Hey this is me";


            Note addNote2 = new Note();
            addNote2.GUID = "2";
            addNote2.Title = "second";
            addNote2.Owner = userAccount2.UserName;
            addNote2.Content = "Hey this is me again";


            //cmd.CreateNote(addNote2);
            Note addNoteUpate = new Note();
            addNoteUpate.GUID = "2";
            addNoteUpate.Title = "secondUpdatefill";
            addNoteUpate.Owner = userAccount2.UserName;
            addNoteUpate.Content = "Hey this is me againfill";


            Note addNote3 = new Note();
            addNote3.GUID = "Update1";
            addNote3.Title = "Update";
            addNote3.Owner = userAccount2.UserName;
            addNote3.Content = "This is the update note.";
            // cmd.CreateNote(addNote2);
            //cmd.DeleteNote(addNote2.GUID);
           cmd.UpdateNote(addNoteUpate);





            // cmd.CreateUser(userAccount2, "Hash", "Salt");
            //Dictionary<string, Permission> users = new Dictionary<string, Permission>();
            //users.Add(userAccount2.UserName, Permission.Owner);

            ////BsonDocument bsonDoc = users.ToBsonDocument();
            //byte[] content = new byte[10];


            //BsonDocument bfile = new BsonDocument
            //{
            //    {"Users", bsonDoc},
            //    {"GUID", "23"},
            //    {"Content", content.ToBsonDocumentArray()},
            //    {"Extension", ".txt"},
            //    {"Name", "File1"}
            //};

            //File file = new File();
            //file.Users = users;
            //file.GUID = "675";
            //file.Content = content;
            //file.Extension = ".txt";
            //file.Name = "fileOne";

            //cmd.UploadFile(file);

            //File file2 = new File();
            //file2.Users = users;
            //file2.GUID = "13";
            //file2.Content = content;
            //file2.Extension = ".txt";
            //file2.Name = "fileTwo";

            //File addingFile = new File();
            //addingFile.Users = users;
            //addingFile.GUID = "675";
            //addingFile.Content = content;
            //addingFile.Extension = ".txt";
            //addingFile.Name = "I am the new updated file 675, Hurray!";


            //List<BsonDocument> listOfGoals = new List<BsonDocument>();
            //List<BsonDocument> listOfFiles = new List<BsonDocument>();
            //List<BsonDocument> listOfNotes = new List<BsonDocument>();
            //string username = "prvora89";
            //string phoneNumber = "888-888-888";
            //string email = "prvora89@gmail.com";

            //BsonDocument user = new BsonDocument

            //{
            //    {"ListOfGoals", listOfGoals.ToBsonDocumentArray()},
            //    {"ListOfNotes", listOfNotes.ToBsonDocumentArray()},
            //    {"ListOfFiles", listOfFiles.ToBsonDocumentArray()},
            //    {"Username", username},
            //    {"PhoneNumber", phoneNumber},
            //    {"Email", email}
            //};


            //List<File> filesList = new List<File>();
            //filesList.Add(file);
            //List<File> filesList2 = new List<File>();
            //filesList2.Add(file2);

            //string guid = "24";
            //string taskName = "Set MongoDB Database on Azure.";
            //string description = "Azure is a cloud service provider.";
            //string deadline = "5/19/2018";
            //int points = 200;
            //bool completed = true;
            //bool notCompleted = false;
            //bool hidden = false;

            //DateTime datePast = new DateTime(2017, 1, 18);
            //DateTime dateTommorow = new DateTime(2018, 5, 26);
            //DateTime dateNow = new DateTime(2018, 5, 24);
            //DateTime dateFuture = new DateTime(2019, 2, 13);
            //DateTime dateFuture2 = new DateTime(2018, 6, 1);

            //NonRecurringGoal passedGoal = new NonRecurringGoal();
            //passedGoal.GUID = "1";
            //passedGoal.TaskName = "Passed Goal";
            //passedGoal.Description = "description";
            //passedGoal.Deadline = datePast;
            //passedGoal.Points = 100;
            //passedGoal.Completed = completed;
            //passedGoal.Hidden = true;

            //NonRecurringGoal goalForTommrow = new NonRecurringGoal();
            //goalForTommrow.GUID = "2";
            //goalForTommrow.TaskName = "Tommorow's Goal";
            //goalForTommrow.Description = "description";
            //goalForTommrow.Deadline = dateTommorow;
            //goalForTommrow.Points = 200;
            //goalForTommrow.Completed = notCompleted;
            //goalForTommrow.Hidden = false;

            //RecurringGoal goalForToday = new RecurringGoal();
            //goalForToday.GUID = "3";
            //goalForToday.TaskName = "Today's Goal";
            //goalForToday.Description = "description";
            //goalForToday.Deadline = dateNow;
            //goalForToday.Points = 300;
            //goalForToday.Completed = notCompleted;
            //goalForToday.Hidden = false;


            //RecurringGoal goalFuture1 = new RecurringGoal();
            //goalFuture1.GUID = "4";
            //goalFuture1.TaskName = "Future Goal 1";
            //goalFuture1.Description = "description";
            //goalFuture1.Deadline = dateFuture;
            //goalFuture1.Points = 400;
            //goalFuture1.Completed = notCompleted;
            //goalFuture1.Hidden = false;

            //NonRecurringGoal goalFuture2 = new NonRecurringGoal();
            //goalFuture2.GUID = "5";
            //goalFuture2.TaskName = "Future Goal 2";
            //goalFuture2.Description = "description";
            //goalFuture2.Deadline = dateFuture2;
            //goalFuture2.Points = 500;
            //goalFuture2.Completed = notCompleted;
            //goalFuture2.Hidden = false;

            //List<Goal> goalsList = new List<Goal>();
            //goalsList.Add(passedGoal);
            //goalsList.Add(goalForTommrow);
            //goalsList.Add(goalForToday);
            //goalsList.Add(goalFuture1);
            //goalsList.Add(goalFuture2);


            //UserAccount userAccount = new UserAccount(goalsList, null, null, username, phoneNumber, email);
            //UserAccount userAccount2 = new UserAccount(goalsList, null, null, "Ankita", "415-584-4324", "AnkitaGoradia78@gmail.com");
            //UserAccount userAccount3 = new UserAccount(goalsList, null, null, "Nainesh", "415-465-4324", "NaineshGoradia87@gmail.com");
            //UserAccount userAccount4 = new UserAccount(new List<Goal>(), null, null, "Natasha", "510-465-4324", "Natasha67@gmail.com");
            //UserAccount userAccount5 = new UserAccount(null, null, null, "Raj", "234-465-4324", "Raj123@gmail.com");

            //users = new Dictionary<string, Permission>();
            ////users.Add(userAccount.UserName, Permission.Edit);
            //users.Add(userAccount2.UserName, Permission.Owner);
            //users.Add(userAccount3.UserName, Permission.Edit);

            //Note note = new Note(userAccount.UserName, "I am priya!", "My Intro", "1");




            //  cmd.GetUpcomingGoals("prvora89", DateTime.Now);
            //cmd.GetLoginUser("prvdfgdgdora89");
            //addingFile.Users = users;
            //cmd.GetUser("prvora89");
            //cmd.CreateUser(userAccount, "HashedPassword", "Salt");
            //cmd.CreateUser(userAccount2, "HashedPassword", "Salt");
            //cmd.CreateUser(userAccount3, "HashedPassword", "Salt");
            //cmd.CreateUser(userAccount4, "HashedPassword", "Salt");
            //cmd.CreateUser(userAccount5, "HashedPassword", "Salt");
            //NonRecurringGoal goal = new NonRecurringGoal();
            //goal.GUID = "20";
            //goal.TaskName = "Empty Goal For Test";
            //cmd.CreateGoal(goal, "prvora89");
            //Console.WriteLine("User Valid: " + cmd.AuthenticateUser("prvo", "HashPassword"));

            // cmd.MarkGoalAsComplete("5", "prvora89");
            //cmd.GetUpcomingGoals("Natasha", DateTime.Now);
            //cmd.GetUpcomingGoals("Raj", DateTime.Now);
            //cmd.GetUpcomingGoals("prvora89", DateTime.Now);
            //cmd.UploadFile(file2);
            //cmd.CreateGoal(addingGoal, "prvora89");
            //cmd.MarkGoalAsComplete("1", "prvora89");
            //MongoCommands.GetUser(username);
            //cmd.GetAccountListOfUsersOnAFile("Ankita", "13");

            //cmd.CreateNote(note);

            //cmd.GetFilePreviews("prvora89");
            //cmd.DeleteFile("675");

            //Dictionary<string, Permission> remainingUsers = new Dictionary<string, Permission>();
            //remainingUsers.Add(userAccount.UserName, Permission.Edit);
            //remainingUsers.Add(userAccount3.UserName, Permission.Edit);

            //cmd.RemoveUserFromAccountFileUsers("Ankita", "13", remainingUsers);

            // cmd.RemoveFilesFromUserAccounts(addingFile, "prvora89");
            //  cmd.GetUsersListOFFiles("Nainesh");
            //cmd.RemoveNotesFromUserAccounts(note, "prvora89");
            //cmd.DeleteFile("13");

            //cmd.GetFileFromCollection("13");
            //cmd.DeleteNote(note.GUID);
            //cmd.ShareFile("675",users);
            //cmd.UpdateFile(addingFile);


        }

    }

}