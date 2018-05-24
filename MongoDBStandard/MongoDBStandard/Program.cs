﻿using System;
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
            Dictionary<string, Permission> users = new Dictionary<string, Permission>();


            BsonDocument bsonDoc = users.ToBsonDocument();
            byte[] content = new byte[10];


            BsonDocument bfile = new BsonDocument
            {
                {"Users", bsonDoc},
                {"GUID", "23"},
                {"Content", content.ToBsonDocumentArray()},
                {"Extension", ".txt"},
                {"Name", "File1"}
            };

            File file = new File();
            file.Users = users;
            file.GUID = "675";
            file.Content = content;
            file.Extension = ".txt";
            file.Name = "fileOne";

            File file2 = new File();
            file2.Users = users;
            file2.GUID = "13";
            file2.Content = content;
            file2.Extension = ".txt";
            file2.Name = "fileTwo";

            File addingFile = new File();
            addingFile.Users = users;
            addingFile.GUID = "675";
            addingFile.Content = content;
            addingFile.Extension = ".txt";
            addingFile.Name = "I am the new updated file 675, Hurray!";


            List<BsonDocument> listOfGoals = new List<BsonDocument>();
            List<BsonDocument> listOfFiles = new List<BsonDocument>();
            List<BsonDocument> listOfNotes = new List<BsonDocument>();
            string username = "prvora89";
            string phoneNumber = "888-888-888";
            string email = "prvora89@gmail.com";

            BsonDocument user = new BsonDocument

            {
                {"ListOfGoals", listOfGoals.ToBsonDocumentArray()},
                {"ListOfNotes", listOfNotes.ToBsonDocumentArray()},
                {"ListOfFiles", listOfFiles.ToBsonDocumentArray()},
                {"Username", username},
                {"PhoneNumber", phoneNumber},
                {"Email", email}
            };



            string guid = "24";
            string taskName = "Set MongoDB Database on Azure.";
            string description = "Azure is a cloud service provider.";
            string deadline = "5/19/2018";
            int points = 200;
            bool completed = false;
            bool hidden = false;

            BsonDocument bgoal = new BsonDocument

            {
                {"GUID", guid},
                {"TaskName", taskName},
                {"Description", description},
                {"Deadline", deadline},
                {"Points", points},
                {"Completed", completed},
                {"Hidden", hidden}
            };
            BsonDocument bgoal2 = new BsonDocument

            {
                {"GUID", "25"},
                {"TaskName", "Finish Database - Application connection."},
                {"Description", "Mongo Db is the database we are using."},
                {"Deadline", "5/32/12"},
                {"Points", "100"},
                {"Completed", false},
                {"Hidden", false}
            };


            NonRecurringGoal goal = new NonRecurringGoal();
            goal.GUID = "24";
            goal.TaskName = taskName;
            goal.Description = description;
            goal.Deadline = new DateTime();
            goal.Points = points;
            goal.Completed = completed;
            goal.Hidden = false;

            NonRecurringGoal secondGoal = new NonRecurringGoal();
            goal.GUID = "24";
            goal.TaskName = taskName;
            goal.Description = description;
            goal.Deadline = new DateTime();
            goal.Points = points;
            goal.Completed = completed;
            goal.Hidden = false;

            RecurringGoal goal2 = new RecurringGoal();
            goal2.GUID = "25";
            goal2.TaskName = "second task";
            goal2.Description = "second description";
            goal2.Deadline = new DateTime();
            goal2.Points = 100;
            goal2.Completed = false;
            goal2.Hidden = false;


            RecurringGoal secondGoal2 = new RecurringGoal();
            goal2.GUID = "25";
            goal2.TaskName = "second task";
            goal2.Description = "second description";
            goal2.Deadline = new DateTime();
            goal2.Points = 100;
            goal2.Completed = false;
            goal2.Hidden = false;

            NonRecurringGoal addingGoal = new NonRecurringGoal();
            addingGoal.GUID = "1";
            addingGoal.TaskName = "added task";
            addingGoal.Description = "added description";
            addingGoal.Deadline = new DateTime();
            addingGoal.Points = 400;
            addingGoal.Completed = false;
            addingGoal.Hidden = false;

            List<Goal> goalsList = new List<Goal>();
            goalsList.Add(goal);
            goalsList.Add(goal2);
            //goalsList.Add(addingGoal);

            List<Goal> goalsList2 = new List<Goal>();
            goalsList2.Add(secondGoal);
            goalsList2.Add(secondGoal2);

            List<File> filesList = new List<File>();
            filesList.Add(file);
            List<File> filesList2 = new List<File>();
            filesList2.Add(file2);
            UserAccount userAccount = new UserAccount(goalsList, null, null, username, phoneNumber, email);
            UserAccount userAccount2 = new UserAccount(goalsList2, null, null, "Ankita", "415-584-4324", "AnkitaGoradia78@gmail.com");
            UserAccount userAccount3 = new UserAccount(goalsList2, null, null, "Nainesh", "415-465-4324", "NaineshGoradia87@gmail.com");

            users.Add(userAccount.UserName, Permission.Edit);
            users.Add(userAccount2.UserName, Permission.Owner);
            users.Add(userAccount3.UserName,Permission.Edit);

            Note note = new Note(userAccount.UserName, "I am priya!", "My Intro", "1");


            //addingFile.Users = users;
            //cmd.GetUser("prvora89");
            //cmd.CreateUser(userAccount);
            //cmd.CreateUser(userAccount2);
            //cmd.CreateUser(userAccount3);
            //cmd.UploadFile(file);
            //cmd.CreateGoal(addingGoal, "prvora89");
            //cmd.MarkGoalAsComplete("1", "prvora89");
            //MongoCommands.GetUser(username);
            cmd.GetAccountListOfUsersOnAFile("Ankita", "13");
            //cmd.CreateNote(note);

            //cmd.GetFilePreviews("prvora89");
            //cmd.DeleteFile("675");

            //Dictionary<string, Permission> remainingUsers = new Dictionary<string, Permission>();
            //remainingUsers.Add(userAccount.UserName, Permission.Edit);
            //remainingUsers.Add(userAccount3.UserName, Permission.Edit);
            // cmd.RemoveFilesFromUserAccounts(file2, "Ankita");
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