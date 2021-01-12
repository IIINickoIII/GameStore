using GameStore.Bll.Dto;
using System.Collections.Generic;

namespace GameStore.Bll.Interfaces
{
    public interface IGenreService
    {
        void AddGenre(GenreDto genreDto);

        void EditGenre(GenreDto genreDto);

        GenreDto GetGenre(int genreId);

        IEnumerable<GenreDto> GetGenres();

        IEnumerable<GenreDto> GetStructuredGenres();

        bool NewGenreNameIsAvailable(string newGenreName, int genreId = 0);

        void SoftDelete(int genreId);
    }
}
