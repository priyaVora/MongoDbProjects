﻿using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDBStandard.models;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.IO;

namespace MongoDBStandard.CRUD_COMMANDS
{
    public class MongoCommands
    {
        const string USER_COLLECTION = "UserAccount";
        const string NOTE_COLLECTION = "Note";
        const string FILE_COLLECTION = "File";

        const string MONGO_CONNECTION_STRING = "mongodb://40.114.29.68:27017";
        const string MONGO_DATABASE = "Mongo_Study_App";

        public void UpdatePoints(string username, string goalGuid, string points)
        {
            throw new NotImplementedException();
        }
        private File DeleteFileFromFileCollection(string guid)
        {
            IMongoCollection<BsonDocument> fileCollection = GetCollection(FILE_COLLECTION);
            FilterDefinition<BsonDocument> deleteFileFilter = Builders<BsonDocument>.Filter.Eq("GUID", guid);
            File deletingFile = GetFileFromCollection(guid);
            fileCollection.DeleteOne(deleteFileFilter);

            return deletingFile;
        }

        public File GetFileFromCollection(string guid)
        {
            IMongoCollection<BsonDocument> fileCollection = GetCollection(FILE_COLLECTION);
            FilterDefinition<BsonDocument> deleteFileFilter = Builders<BsonDocument>.Filter.Eq("GUID", guid);

            BsonDocument file = fileCollection.Find(deleteFileFilter).First();

            File deletingFile = BsonSerializer.Deserialize<File>(file.ToJson());
            Console.WriteLine("Grabbed File: " + deletingFile.GUID);
            return deletingFile;
        }

        private void DeleteFileFroUserAccount()
        {
            throw new NotImplementedException();
        }

        private void UploadFileInFileCollection()
        {
            throw new NotImplementedException();
        }
        
        private void UploadFileForAUser()
        {
            throw new NotImplementedException();
        }

        private void UpdateFileUsersForFileInFileCollection()
        {
            throw new NotImplementedException();
        }

        private void UpdateFileUsersInAccounts()
        {
            throw new NotImplementedException();
        }
        
        private void AddNoteInNoteCollections()
        {
            throw new NotImplementedException();
        }

        public void AddNoteInUserAccount()
        {
            throw new NotImplementedException();
        }



        public void DeleteFile(string guid)
        {
           
            File deletingFile = GetFileFromCollection(guid);
            foreach (KeyValuePair<string, Permission> entry in deletingFile.Users)
            {
                string currentAccount = entry.Key;
                Permission currentPermission = entry.Value;
                Console.WriteLine("User account to delete file from: " + currentAccount);
                RemoveFilesFromUserAccounts(deletingFile, currentAccount);
            }
            DeleteFileFromFileCollection(guid);
        
        }

        public void RemoveFilesFromUserAccounts(File file, string username)
        {
            UserAccount account = GetUser(username);
            IMongoCollection<BsonDocument> userCollection = GetCollection(USER_COLLECTION);
            FilterDefinition<BsonDocument> getUserFilter = Builders<BsonDocument>.Filter.Eq("UserName", username);
            BsonDocument user = userCollection.Find(getUserFilter).First();

            BsonType typeOfFiles = user["ListOfFiles"].BsonType;
            List<File> listOfFiles = new List<File>();
            if (typeOfFiles != BsonType.Null)
            {
                var userFilesList = user["ListOfFiles"].AsBsonArray;
                Console.WriteLine("Files List is not null");

                foreach (var element in userFilesList)
                {
                    File f = null;
                    f = BsonSerializer.Deserialize<File>(element.ToJson());
                    if (f.GUID != file.GUID)
                    {
                        listOfFiles.Add(f);
                    }
                    else
                    {
                        Console.WriteLine("Removed File: " + f.GUID);
                    }
                }
            }

            UserAccount updatedAccount = GetUser(username);
            userCollection.DeleteOne(updatedAccount.ToBsonDocument());
            updatedAccount.ListOfFiles = listOfFiles;
            userCollection.InsertOne(updatedAccount.ToBsonDocument());

            Dictionary<string, Permission> users = new Dictionary<string, Permission>();
            IMongoCollection<BsonDocument> fileCollection = GetCollection(FILE_COLLECTION);
            FilterDefinition<BsonDocument> deleteFileFilter = Builders<BsonDocument>.Filter.Eq("GUID", file.GUID);

            BsonDocument fileToUpdateUsers = fileCollection.Find(deleteFileFilter).First();
            File sharingFile = BsonSerializer.Deserialize<File>(file.ToJson());

            file.Users = sharingFile.Users;
            Dictionary<string, Permission> remainingUsers = new Dictionary<string, Permission>();
            foreach (KeyValuePair<string, Permission> entry in sharingFile.Users)
            {
                string currentAccount = entry.Key;
                Permission currentPermission = entry.Value;

                if (currentAccount.Equals(username))
                {
                    Console.WriteLine("Username to be removed from file collection: " + currentAccount);

                }
                else
                {
                    Console.WriteLine("Username to remain in the file collection: " + currentAccount);
                    remainingUsers.Add(currentAccount, currentPermission);
                    //update user
                }
            }

            DeleteFileFromFileCollection(sharingFile.GUID);
            sharingFile.Users = remainingUsers;
            fileCollection.InsertOne(sharingFile.ToBsonDocument());

        }
        public void DeleteNote(string guid)
        {
            IMongoCollection<BsonDocument> noteCollection = GetCollection(NOTE_COLLECTION);
            FilterDefinition<BsonDocument> deleteNoteFilter = Builders<BsonDocument>.Filter.Eq("GUID", guid);

            BsonDocument note = noteCollection.Find(deleteNoteFilter).First();
            var owner = note["Owner"];
            var noteGuid = note["GUID"].AsBsonValue;
            string guidDoc = BsonSerializer.Deserialize<string>(noteGuid.ToJson());

            // a.UserName = owner;

            var strName = BsonSerializer.Deserialize<string>(owner.ToJson());

            Note deletingNote = new Note(null, null, null, guidDoc);
            RemoveNotesFromUserAccounts(deletingNote, strName);
            noteCollection.DeleteOne(deleteNoteFilter);

        }
        public void CreateNote(Note note)
        {
            BsonDocument bNote = note.ToBsonDocument();
            IMongoCollection<BsonDocument> noteCollection = GetCollection(NOTE_COLLECTION);
            FilterDefinition<BsonDocument> getNoteFilter = Builders<BsonDocument>.Filter.Eq("GUID", note.GUID);


            noteCollection.InsertOne(bNote);

            UserAccount account = GetUser(note.Owner);
            IMongoCollection<BsonDocument> userCollection = GetCollection(USER_COLLECTION);
            FilterDefinition<BsonDocument> getUserFilter = Builders<BsonDocument>.Filter.Eq("UserName", note.Owner);
            BsonDocument user = userCollection.Find(getUserFilter).First();


            BsonType typeOfNote = user["ListOfNotes"].BsonType;

            List<Note> listOfNotes = new List<Note>();
            if (typeOfNote != BsonType.Null)
            {
                var userNotesList = user["ListOfNotes"].AsBsonArray;
                Console.WriteLine("Notes List is not null");
                foreach (var element in userNotesList)
                {
                    Note n = null;
                    n = BsonSerializer.Deserialize<Note>(element.ToJson());
                    if (n.GUID != note.GUID)
                    {
                        listOfNotes.Add(n);
                    }
                    else
                    {
                        Console.WriteLine("Note already exist in the collection.");
                    }
                }
                listOfNotes.Add(note);
            }
            else
            {
                listOfNotes.Add(note);
            }
            UserAccount updatedAccount = GetUser(note.Owner);
            userCollection.DeleteOne(updatedAccount.ToBsonDocument());
            updatedAccount.ListOfNotes = listOfNotes;
            userCollection.InsertOne(updatedAccount.ToBsonDocument());
            Console.WriteLine("End ");

        }



        private void AddFilesToUsersHelper(File file, string username)
        {
            UserAccount account = GetUser(username);
            IMongoCollection<BsonDocument> userCollection = GetCollection(USER_COLLECTION);
            FilterDefinition<BsonDocument> getUserFilter = Builders<BsonDocument>.Filter.Eq("UserName", username);
            BsonDocument user = userCollection.Find(getUserFilter).First();

            Console.WriteLine(username + ": file added.");

            BsonType typeOfFiles = user["ListOfFiles"].BsonType;
            List<File> listOfFiles = new List<File>();
            if (typeOfFiles != BsonType.Null)
            {
                var userFilesList = user["ListOfFiles"].AsBsonArray;
                Console.WriteLine("Files List is not null");

                foreach (var element in userFilesList)
                {
                    File f = null;
                    f = BsonSerializer.Deserialize<File>(element.ToJson());
                    if (f.GUID != file.GUID)
                    {

                        listOfFiles.Add(f);

                    }
                    else
                    {
                        Console.WriteLine("File already exist in the collection.");
                    }
                }
                listOfFiles.Add(file);
            }
            else
            {
                listOfFiles.Add(file);
            }

            UserAccount updatedAccount = GetUser(username);
            userCollection.DeleteOne(updatedAccount.ToBsonDocument());
            updatedAccount.ListOfFiles = listOfFiles;
            userCollection.InsertOne(updatedAccount.ToBsonDocument());
            Console.WriteLine("End ");

        }

        public void RemoveNotesFromUserAccounts(Note note, string username)
        {
            UserAccount account = GetUser(username);
            IMongoCollection<BsonDocument> userCollection = GetCollection(USER_COLLECTION);
            FilterDefinition<BsonDocument> getUserFilter = Builders<BsonDocument>.Filter.Eq("UserName", username);
            BsonDocument user = userCollection.Find(getUserFilter).First();

            BsonType typeOfNotes = user["ListOfNotes"].BsonType;
            List<Note> listOfNotes = new List<Note>();

            if (typeOfNotes != BsonType.Null)
            {
                var userNotesList = user["ListOfNotes"].AsBsonArray;
                Console.WriteLine("Notes List is not null");

                foreach (var element in userNotesList)
                {
                    Note n = null;
                    n = BsonSerializer.Deserialize<Note>(element.ToJson());
                    if (n.GUID != note.GUID)
                    {

                        listOfNotes.Add(n);

                    }
                    else
                    {
                        Console.WriteLine("Removed Notes: " + n.GUID);
                    }
                }
            }

            UserAccount updatedAccount = GetUser(username);
            userCollection.DeleteOne(updatedAccount.ToBsonDocument());
            updatedAccount.ListOfNotes = listOfNotes;
            userCollection.InsertOne(updatedAccount.ToBsonDocument());
        }

        





        public void UploadFile(File file)
        {
            IMongoCollection<BsonDocument> fileCollection = GetCollection(FILE_COLLECTION);
            FilterDefinition<BsonDocument> getFileFilter = Builders<BsonDocument>.Filter.Eq("GUID", file.GUID);
            Dictionary<string, Permission> fileUsers = new Dictionary<string, Permission>();


            foreach (KeyValuePair<string, Permission> entry in file.Users)
            {
                string currentAccount = entry.Key;
                Permission currentPermission = entry.Value;
                fileUsers.Add(currentAccount, currentPermission);

            }

            byte[] fileContent = file.Content;
            string guid = file.GUID;
            string extension = file.Extension;
            string fileName = file.Name;

            string guidDoc = BsonSerializer.Deserialize<string>(guid.ToJson());
            string extensionDoc = BsonSerializer.Deserialize<string>(extension.ToJson());
            string nameDoc = BsonSerializer.Deserialize<string>(fileName.ToJson());

            Dictionary<UserAccount, Permission> users = new Dictionary<UserAccount, Permission>();
            BsonDocument bsonDoc = users.ToBsonDocument();
            byte[] content = new byte[10];
            ;
            BsonDocument bFileFormat = new BsonDocument
            {
              {"Users", fileUsers.ToBsonDocument()},
              {"GUID", guidDoc},
              {"Content", fileContent},
              {"Extension", extensionDoc},
              {"Name", nameDoc}
            };

            fileCollection.InsertOne(bFileFormat);

            foreach (KeyValuePair<string, Permission> entry in file.Users)
            {
                string currentUsername = entry.Key;
                Permission currentPermission = entry.Value;

                AddFilesToUsersHelper(file, currentUsername);
            }
        }

        public void CreateUser(UserAccount user)
        {
            BsonDocument bUserAccount = user.ToBsonDocument();
            IMongoCollection<BsonDocument> userCollection = GetCollection(USER_COLLECTION);

            userCollection.InsertOne(bUserAccount);
        }

        public void UpdateUser(UserAccount user)
        {
            throw new NotImplementedException();
        }

        public void CreateGoal(Goal goal, string username)

        {
            UserAccount account = GetUser(username);
            IMongoCollection<BsonDocument> userCollection = GetCollection(USER_COLLECTION);
            FilterDefinition<BsonDocument> getUserFilter = Builders<BsonDocument>.Filter.Eq("UserName", username);
            BsonDocument user = userCollection.Find(getUserFilter).First();


            BsonType typeOfGoal = user["ListOfGoals"].BsonType;
            List<Goal> listOfGoals = new List<Goal>();
            if (typeOfGoal != BsonType.Null)
            {
                var userGoalsList = user["ListOfGoals"].AsBsonArray;
                Console.WriteLine("Goals List is not null");

                foreach (var element in userGoalsList)
                {
                    Goal g = null;
                    var type = element["_t"].AsString;
                    if (type == "NonRecurringGoal")
                    {
                        g = BsonSerializer.Deserialize<NonRecurringGoal>(element.ToJson());
                    }
                    else if (type == "RecurringGoal")
                    {
                        g = BsonSerializer.Deserialize<RecurringGoal>(element.ToJson());
                    }
                    listOfGoals.Add(g);
                }
                listOfGoals.Add(goal);
            }
            else
            {
                listOfGoals.Add(goal);
            }

            UserAccount updatedAccount = GetUser(username);
            userCollection.DeleteOne(updatedAccount.ToBsonDocument());
            updatedAccount.ListOfGoals = listOfGoals;
            userCollection.InsertOne(updatedAccount.ToBsonDocument());
        }

        public void MarkGoalAsComplete(string goalGuid, string username)
        {

            IMongoCollection<BsonDocument> userCollection = GetCollection(USER_COLLECTION);
            FilterDefinition<BsonDocument> getUserFilter = Builders<BsonDocument>.Filter.Eq("UserName", username);
            BsonDocument user = userCollection.Find(getUserFilter).First();
            BsonType typeOfGoal = user["ListOfGoals"].BsonType;
            List<Goal> listOfGoals = new List<Goal>();

            if (typeOfGoal != BsonType.Null)
            {
                var userGoalsList = user["ListOfGoals"].AsBsonArray;
                Console.WriteLine("Goals List is not null");

                foreach (var element in userGoalsList)
                {
                    Goal g = null;
                    var type = element["_t"].AsString;
                    if (type == "NonRecurringGoal")
                    {
                        g = BsonSerializer.Deserialize<NonRecurringGoal>(element.ToJson());
                        if (g.GUID.Equals(goalGuid))
                        {
                            Console.WriteLine("Matched non recurring goal");
                            g.Completed = true;
                        }
                    }
                    else if (type == "RecurringGoal")
                    {
                        g = BsonSerializer.Deserialize<RecurringGoal>(element.ToJson());
                        if (g.GUID.Equals(goalGuid))
                        {
                            Console.WriteLine("Matched recurring goal");
                            g.Completed = true;
                        }
                    }
                    listOfGoals.Add(g);
                }

                UserAccount updatedAccount = GetUser(username);
                userCollection.DeleteOne(updatedAccount.ToBsonDocument());
                updatedAccount.ListOfGoals = listOfGoals;
                userCollection.InsertOne(updatedAccount.ToBsonDocument());
            }
        }


        public void UpdateFile(File file)
        {

            Dictionary<string, Permission> users = new Dictionary<string, Permission>();
            IMongoCollection<BsonDocument> userCollection = GetCollection(USER_COLLECTION);
            IMongoCollection<BsonDocument> fileCollection = GetCollection(FILE_COLLECTION);
            FilterDefinition<BsonDocument> deleteFileFilter = Builders<BsonDocument>.Filter.Eq("GUID", file.GUID);

            BsonDocument fileFromCollection= fileCollection.Find(deleteFileFilter).First();
            File fileToUpdate= BsonSerializer.Deserialize<File>(fileFromCollection.ToJson());
            file.Users = fileToUpdate.Users;
            DeleteFile(fileToUpdate.GUID);

            fileCollection.InsertOne(file.ToBsonDocument());
            ShareFile(file.GUID,file.Users);
        }

        public bool AuthenticateUser(string Username, string Password)
        {
            throw new NotImplementedException();
        }

        public void ShareFile(string guid, Dictionary<string, Permission> sharers)
        {
            Dictionary<string, Permission> users = new Dictionary<string, Permission>();
            IMongoCollection<BsonDocument> userCollection = GetCollection(USER_COLLECTION);
            IMongoCollection<BsonDocument> fileCollection = GetCollection(FILE_COLLECTION);
            FilterDefinition<BsonDocument> deleteFileFilter = Builders<BsonDocument>.Filter.Eq("GUID", guid);

            BsonDocument file = fileCollection.Find(deleteFileFilter).First();
            File sharingFile = BsonSerializer.Deserialize<File>(file.ToJson());
            DeleteFile(sharingFile.GUID);
            sharingFile.Users = sharers;
            foreach (KeyValuePair<string, Permission> entry in sharers)
            {
                string currentAccount = entry.Key;
                Permission currentPermission = entry.Value;
                Console.WriteLine("Username to Share to: " + currentAccount);
                Console.WriteLine("Permission Type for User: " + currentPermission);


                FilterDefinition<BsonDocument> getUserFilter = Builders<BsonDocument>.Filter.Eq("UserName", currentAccount);
                BsonDocument user = userCollection.Find(getUserFilter).First();

                BsonType typeOfFile = user["ListOfFiles"].BsonType;
                List<File> listOfFiles = new List<File>();
                if (typeOfFile != BsonType.Null)
                {
                    var userFileList = user["ListOfFiles"].AsBsonArray;

                    foreach (var element in userFileList)
                    {
                        File f = null;
                        f = BsonSerializer.Deserialize<File>(element.ToJson());
                        listOfFiles.Add(f);
                    }
                   AddFilesToUsersHelper(sharingFile, currentAccount);
                }
                else
                {

                    AddFilesToUsersHelper(sharingFile, currentAccount);
                }
            }
            sharingFile.Users = sharers;
           fileCollection.InsertOne(sharingFile.ToBsonDocument());
        }

        public UserAccount GetUser(string userName)
        {
            Console.WriteLine("Started method..." + " " + userName);
            IMongoCollection<BsonDocument> userCollection = GetCollection(USER_COLLECTION);
            FilterDefinition<BsonDocument> getUserFilter = Builders<BsonDocument>.Filter.Eq("UserName", userName);
            BsonDocument user = userCollection.Find(getUserFilter).First();
            BsonType typeOfGoal = user["ListOfGoals"].AsBsonValue.BsonType;

            List<Goal> listOfGoals = new List<Goal>();
            if (typeOfGoal != BsonType.Null)
            {
                var userGoalsList = user["ListOfGoals"].AsBsonArray;

                foreach (var element in userGoalsList)
                {
                    Goal g = null;
                    var type = element["_t"].AsString;
                    if (type == "NonRecurringGoal")
                    {
                        g = BsonSerializer.Deserialize<NonRecurringGoal>(element.ToJson());
                    }
                    else if (type == "RecurringGoal")
                    {
                        g = BsonSerializer.Deserialize<RecurringGoal>(element.ToJson());
                    }
                    listOfGoals.Add(g);
                }
            }
            else
            {
                listOfGoals = null;
            }
            var userFilesList = user["ListOfFiles"].AsBsonValue;
            var userNotesList = user["ListOfNotes"].AsBsonValue;
            var username = user["UserName"].AsBsonValue;
            var phoneNumber = user["PhoneNumber"].AsBsonValue;
            var email = user["Email"].AsBsonValue;
            List<File> listOfFiles = BsonSerializer.Deserialize<List<File>>(userFilesList.ToJson());
            List<Note> listOfNotes = BsonSerializer.Deserialize<List<Note>>(userNotesList.ToJson());
            string usernameStr = BsonSerializer.Deserialize<string>(username.ToJson());
            string phoneNumberStr = BsonSerializer.Deserialize<string>(phoneNumber.ToJson());
            string emailStr = BsonSerializer.Deserialize<string>(email.ToJson());

            UserAccount userAccount = new UserAccount(listOfGoals, listOfNotes, listOfFiles, usernameStr, phoneNumberStr, emailStr);
            return userAccount;
        }

        public IMongoCollection<BsonDocument> GetUpcomingGoals(string username)
        {
            throw new NotImplementedException();
        }

        public List<FileMini> GetFilePreviews(string username)
        {
            IMongoCollection<BsonDocument> userCollection = GetCollection(USER_COLLECTION);
            FilterDefinition<BsonDocument> getUserFilter = Builders<BsonDocument>.Filter.Eq("UserName", username);
            BsonDocument user = userCollection.Find(getUserFilter).First();
            var userFilesList = user["ListOfFiles"].AsBsonArray;
            List<FileMini> fileMiniList = new List<FileMini>();
            File f = null;
            foreach (var element in userFilesList)
            {
                f = BsonSerializer.Deserialize<File>(element.ToJson());
                FileMini mini = new FileMini();
                mini.GUID = f.GUID;
                mini.Name = f.Name;
                mini.Extension = f.Extension;
                fileMiniList.Add(mini);
            }

            return fileMiniList;
        }

        private IMongoCollection<BsonDocument> GetCollection(string name)
        {
            MongoUrl url = new MongoUrl(MONGO_CONNECTION_STRING);
            MongoClient client = new MongoClient(url);
            IMongoDatabase db = client.GetDatabase(MONGO_DATABASE);
            return db.GetCollection<BsonDocument>(name);

        }
    }
}



