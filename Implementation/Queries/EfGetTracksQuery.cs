using Application.DataTransfer;
using Application.Queries;
using Application.Search;
using DataAccess.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Queries
{
    public class EfGetTracksQuery : IGetTracksQuery
    {
        private readonly ChinookContext _context;

        public EfGetTracksQuery(ChinookContext context)
        {
            _context = context;
        }

        public int Id => 4;

        public string Name => "Search tracks";

        public PagedResponse<TrackDto> Execute(TrackSearch search)
        {
            var query = _context.Track.AsQueryable();

            if(!string.IsNullOrEmpty(search.Name) || !string.IsNullOrWhiteSpace(search.Name))
            {
                query = query.Where(x => x.Name.ToLower().Contains(search.Name.ToLower()));
            }

            var skipCount = search.PerPage * (search.Page - 1);

            var response = new PagedResponse<TrackDto>
            {
                CurrentPage = search.Page,
                ItemsPerPage = search.PerPage,
                TotalCount = query.Count(),
                Items = query.Skip(skipCount).Take(search.PerPage).Select(x => new TrackDto
                { 
                    Id = x.TrackId,
                    AlbumId = x.AlbumId,
                    Name = x.Name,
                    Bytes = x.Bytes,
                    Composer = x.Composer,
                    GenreId = x.GenreId,
                    MediaTypeId = x.MediaTypeId,
                    Milliseconds = x.Milliseconds,
                    UnitPrice = x.Milliseconds
                }).ToList()
            };

            return response;
        }
    }
}
