using System;

namespace Catalog.Settings
{
    public class MongoDBSettings
    {
        public String Host { get; set; }
        public int Port { get; set; }

        public String User { get; set; }
        public String Password { get; set; }

        public String ConnectionString 
        { 
            get
        {
            // return $"mongodb://{Host}:{Port}";
            return $"mongodb://{User}:{Password}@{Host}:{Port}";
        } 
        }
    }
}