using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class User
    {
        public User()
        {
            CreatedDate = DateTime.Now;
        }
        [BsonId] public ObjectId Id { get; set; }
        [BsonElement("Name")] public string Name { get; set; }
        [BsonElement("Surname")] public string Surname { get; set; }
        [BsonElement("Email")] public string Email { get; set; }
        [BsonElement("Password")] public string Password { get; set; }
        [BsonElement("CreatedDate")] public DateTime CreatedDate { get; set; }
    }
}