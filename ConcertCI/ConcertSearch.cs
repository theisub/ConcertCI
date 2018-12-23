using ConcertCI;
using System.Collections.Generic;


namespace ConcertSearchApi.Api
{
    public interface ConcertSearch
    {
        List<Concert> GetAllConcertsByGroup(string group);
        List<Concert> GetCityConcertsByGroup(string group, string city);
        List<Concert> GetAllConcertsByGenre(string genre);
        List<Concert> GetCityConcertsByGenre(string genre, string city);
    }
}
