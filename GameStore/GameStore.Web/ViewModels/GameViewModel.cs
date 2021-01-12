using GameStore.Bll.Dto;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace GameStore.Web.ViewModels
{
    [ExcludeFromCodeCoverage]
    public class GameViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Key")]
        [Required(ErrorMessage = "Please, enter the Key")]
        public string Key { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Please, enter the game name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Please, add the Description")]
        public string Description { get; set; }

        [Display(Name = "Price")]
        [Required(ErrorMessage = "Please, enter the price")]
        public decimal Price { get; set; }

        [Display(Name = "Discount (percents)")]
        public float Discount { get; set; }

        [Display(Name = "Units in stock")]
        public short UnitInStock { get; set; }

        [Display(Name = "Discontinued")]
        public bool Discontinued { get; set; }

        [RegularExpression("^[1-9][0-9]*$", ErrorMessage = "Please, select the Publisher")]
        [Display(Name = "Publisher")]
        public int PublisherId { get; set; }

        public PublisherDto Publisher { get; set; }

        public IEnumerable<PublisherDto> AllPublishersList { get; set; }

        public IEnumerable<SelectListItem> PublisherSelectList
        {
            get
            {
                if (AllPublishersList == null)
                {
                    return new List<SelectListItem>();
                }

                var selectList = AllPublishersList
                    .Select(c => new SelectListItem(c.CompanyName, c.Id.ToString()))
                    .ToImmutableList()
                    .Add(new SelectListItem("Select Publisher", "0", true, true));
                return Publisher == null ? selectList : SetSelectedItems(selectList, new List<int>() { PublisherId });
            }
        }

        [Required(ErrorMessage = "Please, select Genres")]
        [Display(Name = "Genres")]
        public IEnumerable<int> GenreIds { get; set; }

        public IEnumerable<GenreDto> Genres { get; set; }

        public IEnumerable<GenreDto> AllGenresList { get; set; }

        public IEnumerable<SelectListItem> GenresSelectList
        {
            get
            {
                if (AllGenresList == null)
                {
                    return new List<SelectListItem>();
                }

                var selectList = AllGenresList
                    .Select(g => new SelectListItem(g.Name, g.Id.ToString()))
                    .ToImmutableList()
                    .Insert(0, new SelectListItem("Select genres", "0", true, true));
                return Genres == null ? selectList : SetSelectedItems(selectList, Genres.Select(g => g.Id));
            }
        }

        [Required(ErrorMessage = "Please, select the Platform Types")]
        [Display(Name = "Platform Types")]
        public IEnumerable<int> PlatformTypeIds { get; set; }

        public IEnumerable<PlatformTypeDto> PlatformTypes { get; set; }

        public IEnumerable<PlatformTypeDto> AllPlatformTypesList { get; set; }

        public IEnumerable<SelectListItem> PlatformTypesSelectList
        {
            get
            {
                if (AllPlatformTypesList == null)
                {
                    return new List<SelectListItem>();
                }

                var selectList = AllPlatformTypesList
                    .Select(p => new SelectListItem(p.Type, p.Id.ToString()))
                    .ToImmutableList()
                    .Insert(0, new SelectListItem("Select Platform Types", "0", true, true));
                return PlatformTypes == null ? selectList : SetSelectedItems(selectList, PlatformTypes.Select(p => p.Id));
            }
        }

        public static IEnumerable<SelectListItem> SetSelectedItems(IEnumerable<SelectListItem> list, IEnumerable<int> itemIds)
        {
            foreach (var item in list)
            {
                item.Selected = itemIds.Contains(Convert.ToInt32(item.Value));
            }

            return list;
        }
    }
}
