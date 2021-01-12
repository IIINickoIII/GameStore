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
    public class GenreViewModel
    {
        [Required]
        public int Id { get; set; }

        [Display(Name = "Parent Genre")]
        public int? ParentGenreId { get; set; }

        public IEnumerable<GenreDto> AllGenresList { get; set; }

        public IEnumerable<SelectListItem> GenreSelectListItems
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
                    .Add(new SelectListItem("No parent Genre", "0", true));
                return ParentGenreId.HasValue ? SetSelectedItems(selectList, new List<int>() { ParentGenreId.Value }) : selectList;
            }
        }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Genre Name is required")]
        public string Name { get; set; }

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
