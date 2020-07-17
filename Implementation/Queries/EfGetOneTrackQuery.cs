using Application.DataTransfer;
using Application.Exceptions;
using Application.Queries;
using AutoMapper;
using DataAccess.Context;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.Queries
{
    public class EfGetOneTrackQuery : IGetOneTrackQuery
    {
        private readonly ChinookContext _context;
        private readonly IMapper _mapper;

        public EfGetOneTrackQuery(ChinookContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public int Id => 3;

        public string Name => "Get track by id";

        public TrackDto Execute(int id)
        {
            var track = _context.Track.Find(id);

            if(track == null)
            {
                throw new EntityNotFoundException(id, typeof(Track));
            }

            return _mapper.Map<TrackDto>(track);
        }
    }
}
