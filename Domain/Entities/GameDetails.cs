using Domain.Entities;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain
{
    public class GameDetails
    {

        [BsonElement("TrackId")] public string TrackId { get; set; }
        [BsonElement("Title")] public string Title { get; set; }
        [BsonElement("Author")] public string Author { get; set; }
        [BsonElement("GameMutableDetails")] public GameMutableDetails GameMutableDetails { get; set; }


    }
}