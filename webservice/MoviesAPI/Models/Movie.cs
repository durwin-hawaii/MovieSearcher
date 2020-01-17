using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Models
{
    public class Movie
    {
        public Int32 movie_id { get; set; }
        public string title { get; set; }
        public string poster_image_url { get; set; }
        public string popularity_summary { get; set; }
    }
}
