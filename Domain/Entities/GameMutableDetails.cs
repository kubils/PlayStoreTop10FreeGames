using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class GameMutableDetails
    {
        [BsonElement("Description")] public string Description { get; set; }
        [BsonElement("TotalReviewCount")] public string TotalReviewCount { get; set; }
        [BsonElement("TotalInstallCount")] public string TotalInstallCount { get; set; }

        [BsonElement("CurrentVersion")] public string CurrentVersion { get; set; }

        [BsonElement("LastUpdateDate")] public string LastUpdateDate { get; set; }

        [BsonElement("Size")] public string Size { get; set; }

        [BsonElement("AppDetailsCreateDate")] public DateTime AppDetailsCreateDate { get; set; }

    }
}
