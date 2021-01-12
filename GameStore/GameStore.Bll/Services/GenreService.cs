using AutoMapper;
using GameStore.Bll.Dto;
using GameStore.Bll.Interfaces;
using GameStore.Dal.Entities;
using GameStore.Dal.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameStore.Bll.Services
{
    public class GenreService : IGenreService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GenreService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public void AddGenre(GenreDto genreDto)
        {
            if (!NewGenreNameIsAvailable(genreDto.Name))
            {
                throw new ArgumentException($"{genreDto.Name} genre Name is already taken");
            }

            var genre = _mapper.Map<Genre>(genreDto);
            _uow.GenreRepository.Add(genre);
            _uow.Save();
        }

        public void EditGenre(GenreDto genreDto)
        {
            if (!NewGenreNameIsAvailable(genreDto.Name, genreDto.Id))
            {
                throw new ArgumentException($"{genreDto.Name} genre Name is already taken");
            }

            var genre = _mapper.Map<Genre>(genreDto);
            _uow.GenreRepository.Update(genre);
            _uow.Save();
        }

        public GenreDto GetGenre(int genreId)
        {
            if (!_uow.GenreRepository.Any(g => g.Id == genreId))
            {
                throw new InvalidOperationException($"No Genre with Id = {genreId} in the Database");
            }

            var genreInDb = _uow.GenreRepository.Single(g => g.Id == genreId);
            var genreDto = _mapper.Map<GenreDto>(genreInDb);
            return genreDto;
        }

        public IEnumerable<GenreDto> GetGenres()
        {
            var genresInDb = _uow.GenreRepository
                .Find(g => g.IsDeleted == false);

            if (!genresInDb.Any())
            {
                throw new ArgumentException("No Genres found in the Database");
            }

            var genresDto = _mapper.Map<IEnumerable<GenreDto>>(genresInDb);
            return genresDto;
        }

        public IEnumerable<GenreDto> GetStructuredGenres()
        {
            var genresInDb = _uow.GenreRepository
                .Find(g => g.IsDeleted == false);

            if (!genresInDb.Any())
            {
                throw new ArgumentException("No Genres found in the Database");
            }

            var genresDto = _mapper.Map<IEnumerable<GenreDto>>(genresInDb);
            var structuredGenres = GetStructuredGenresTree(genresDto);
            return structuredGenres;
        }

        public void SoftDelete(int genreId)
        {
            if (!_uow.GenreRepository.Any(p => p.Id == genreId))
            {
                throw new ArgumentException($"No Genre with Id = {genreId} in the Database");
            }

            var genreInDb = _uow.GenreRepository.Single(p => p.Id == genreId);
            _uow.GenreRepository.SoftDelete(genreInDb);
            _uow.Save();
        }

        private IEnumerable<GenreDto> GetStructuredGenresTree(IEnumerable<GenreDto> genresDto)
        {
            var structuredGenresTree = genresDto.ToList();
            foreach (var genreDto in structuredGenresTree)
            {
                var childrenGenresDto = structuredGenresTree.Where(c => c.ParentGenreId == genreDto.Id).ToList();

                if (childrenGenresDto.Any())
                {
                    genreDto.ChildrenGenres = childrenGenresDto;
                    structuredGenresTree = structuredGenresTree.Except(childrenGenresDto).ToList();

                    foreach (var childrenGenreDto in genreDto.ChildrenGenres.ToList())
                    {
                        GetStructuredGenresTree(childrenGenreDto.ChildrenGenres);
                    }
                }
            }
            return structuredGenresTree;
        }

        public bool NewGenreNameIsAvailable(string newGenreName, int genreId = 0)
        {
            var genreNameIsAvailable = !_uow.GenreRepository.Any(g => g.Name == newGenreName);

            if (genreId == 0)
            {
                return genreNameIsAvailable;
            }

            if (!_uow.GenreRepository.Any(g => g.Id == genreId))
            {
                throw new ArgumentException($"No genre with Id = {genreId} in the Database");
            }

            var oldGenreName = _uow.GenreRepository.Single(g => g.Id == genreId).Name;
            return oldGenreName.Equals(newGenreName) || genreNameIsAvailable;
        }
    }
}
