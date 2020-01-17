using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Models;
using Newtonsoft.Json;

namespace MoviesAPI.Controllers
{
    [Produces("application/json")]
    [Route("Movies")]
    public class MoviesController : Controller
    {
        private string ApiKey = "de697ba5f35bb4bfefc3b9678b6e438c";
        [HttpGet]
        public ContentResult Get(string search, int page = 1)
        {
            // get 10 of the 20 results returned;
            int i, j, k, p;
            if (page % 2 == 1)
            {
                i = 0;
                j = 10;
                p = page / 2 + 1;
            }
            else
            {
                i = 10;
                j = 20;
                p = page / 2;
            }

            string json = WebService.Search("https://api.themoviedb.org/3/search/movie?api_key=" + ApiKey + "&query=" + search + "&page=" + p.ToString() + "&adult=false");

            if (json == null)
                return new ContentResult { StatusCode = 204 };

            dynamic result = JsonConvert.DeserializeObject(json);

            List<Movie> movies = new List<Movie>();

            for (k = i; k < j; k++)
            {
                try
                {
                    Movie movie = new Movie();
                    movie.movie_id = Int32.Parse(result["results"][k]["id"].ToString());
                    movie.title = result["results"][k]["title"].ToString();
                    if (result["results"][k]["poster_path"].ToString().Equals(""))
                        movie.poster_image_url = "assets/img/film_not_found.jpg";
                    else
                        movie.poster_image_url = "https://image.tmdb.org/t/p/w92" + result["results"][k]["poster_path"].ToString();
                    movie.popularity_summary = "Rating " + result["results"][k]["vote_average"].ToString() + " from " + result["results"][k]["vote_count"].ToString() + " votes";
                    movies.Add(movie);
                }
                catch
                {
                    break;
                }
            }

            base.Response.Headers.Add("access-control-allow-origin", "*");

            return new ContentResult
            {
                Content = JsonConvert.SerializeObject(movies),
                ContentType = "application/json",
                StatusCode = 200

            };
        }
    }
}