//using MongoDB.Bson;
//using MongoDB.Driver;
//using MongoDB.Driver.Builders;
//using MongoDB.Driver.GridFS;
//using MongoDB.Driver.Linq;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace MongoDBStandard.CRUD_COMMANDS
//{

//    public class Commands
//    {
//        string ConnectionString = null;
//        MongoDatabase Database = null;
//        MongoServer Server = null;
//        MongoClient Client = null;
//        public Commands(string DatabaseName)
//        {
//            Start_Mongo_Connection(DatabaseName);
//        }

//        public void Start_Mongo_Connection(string DatabaseName)
//        {
//            Console.WriteLine("Mongo DB Test Application");
//            Console.WriteLine("====================================================");
//            Console.WriteLine("Configuration Setting: 40.114.29.68:27017");
//            Console.WriteLine("====================================================");
//            Console.WriteLine("Initializaing connection");
//            ConnectionString = "mongodb://40.114.29.68:27017";
//            Console.WriteLine("Creating Client..........");

//            CreateClient();

//            Console.WriteLine("Initianting Mongo Db Server.......");
//            CreateServer();

//            Console.WriteLine("Initianting Mongo Databaser.........");
//            CreateDataBase(DatabaseName);
//        }

//        private void CreateClient()
//        {
//            Client = null;
//            try
//            {
//                Client = new MongoClient(ConnectionString);
//                Console.WriteLine("Client Created Successfuly........");
//                Console.WriteLine("Client: " + Client.ToString());
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine("Filed to Create Client.......");
//                Console.WriteLine(ex.Message);
//            }
//        }
//        private void CreateServer()
//        {
//            Server = null;
//            try
//            {
//                Console.WriteLine("Getting Servicer object......");
//#pragma warning disable CS0618 // Type or member is obsolete
//                Server = Client.GetServer();
//#pragma warning restore CS0618 // Type or member is obsolete

//                Console.WriteLine("Server object created Successfully....");
//                Console.WriteLine("Server :" + Server.ToString());
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine("Filed to getting Server Details");
//                Console.WriteLine(ex.Message);
//            }
//        }
//        public void CreateDataBase(string DatabaseName)
//        {
//            Database = null;
//            try
//            {
//                Console.WriteLine("Getting reference of database.......");
//                Database = Server.GetDatabase(DatabaseName);
//                Console.WriteLine("Database Name : " + Database.Name);
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine("Failed to Get reference of Database");
//                Console.WriteLine("Error :" + ex.Message);
//            }

//        }
//        public void CreateCollection(string CollectionName)
//        {
//            try
//            {
//                Console.WriteLine("Creating Collection : " + CollectionName);
//                Database.CreateCollection(CollectionName);
                
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine("Failed to create collection from Database");
//                Console.WriteLine("Error :" + ex.Message);
//            }
//        }

//        public IEnumerable<String> GetListOfCollectionNames(string Database_Name)
//        {
//            Console.WriteLine("Getting List of Collection Names: " + Database_Name);
//            try
//            {
//                IEnumerable<String> collectionNames = Database.GetCollectionNames();

//                foreach (String element in collectionNames)
//                {
//                    Console.WriteLine("Collection: " + element);
                   
//                }
//            } 
//            catch (Exception ex)
//            {
//                Console.WriteLine("Failed to get list of collection names from Database");
//                Console.WriteLine("Error :" + ex.Message);
//            }
            

//            return null;
//        }

//        public void AddDocumentToCollection(BsonDocument Data, string CollectionName)
//        {
//            Console.WriteLine("Add: " + Data + " to collection: " + CollectionName);
//            MongoCollection CurrentCollection = Database.GetCollection(CollectionName);
//            CurrentCollection.Insert(Data); 
//        }


//        public void DeleteCollection(string CollectionName)
//        {
//            try
//            {
//                Console.WriteLine("Deleting Collection : " + CollectionName);
//                Database.DropCollection(CollectionName);
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine("Failed to delete collection from Database");
//                Console.WriteLine("Error :" + ex.Message);
//            }
//        }

//        public void DeleteDocumentFromCollection(BsonDocument Data, string CollectionName)
//        {
//            try
//            {
                
//                Console.WriteLine("Delete: " + Data + " to collection: " + CollectionName);
//                MongoCollection CurrentCollection = Database.GetCollection(CollectionName);
//                //"5b00a0e99ad2803ca0267ef8"
//                String NameOfDocument = (String)Data["name"];
//                Console.WriteLine("\nID OF THE DOCUMENT IS: " + NameOfDocument);
//                var query = Query.EQ("name", NameOfDocument);
               
//                var items = CurrentCollection.FindAs<BsonDocument>(query).ToList();

//                Console.WriteLine("\nWrite here is the item: " + items);
//                CurrentCollection.Remove(new QueryDocument("_id", new BsonObjectId(items.)));

//            }
//            catch (Exception ex)
//            {

//            }
//        }

        
//    }
//}
