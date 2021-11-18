using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class GameDetailsDto
    {
        public string TrackId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }

        public string Description { get; set; }
        public string TotalReviewCount { get; set; }
        public string TotalInstallCount { get; set; }
        public string CurrentVersion { get; set; }
        public string LastUpdateDate { get; set; }
        public string Size { get; set; }
        
        //public DateTime AppDetailsCreateDate { get; set; }
    }
}
