using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities
{
    [BsonIgnoreExtraElements(Inherited = true)]
    public class GameListDetails
    {

        [BsonId] public ObjectId Id { get; set; }

        [BsonElement("TopTenFreeGames")] public List<GameDetails> GameDetailsList;

        [BsonElement("ListCreateDate")] public DateTime CreateDate { get; set; }
    }
}
