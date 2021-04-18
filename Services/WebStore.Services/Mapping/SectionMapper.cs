﻿using System.Collections.Generic;
using System.Linq;
using WebStore.Domain.DTO;
using WebStore.Domain.Entities;

namespace WebStore.Services.Mapping
{
    public static class SectionMapper
    {
        public static SectionDTO ToDTO(this Section Section) => Section is null
            ? null
            : new SectionDTO
            {
                Id = Section.Id,
                Name = Section.Name,
                Order = Section.Order,
                ParentId = Section.ParentId,
            };

        public static Section FromDTO(this SectionDTO Section) => Section is null
            ? null
            : new Section
            {
                Id = Section.Id,
                Name = Section.Name,
                Order = Section.Order,
                ParentId = Section.ParentId,
            };

        public static IEnumerable<SectionDTO> ToDTO(this IEnumerable<Section> Section) => Section.Select(ToDTO);
        public static IEnumerable<Section> FromDTO(this IEnumerable<SectionDTO> Section) => Section.Select(FromDTO);
    }
}
