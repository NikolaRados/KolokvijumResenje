using Application.Commands;
using Application.DataTransfer;
using AutoMapper;
using DataAccess.Context;
using DataAccess.Models;
using FluentValidation;
using Implementation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Commands
{
    public class EfCreateTrackCommand : ICreateTrackCommand
    {
        private readonly ChinookContext _context;
        private readonly IMapper _mapper;
        private readonly CreateTrackValidator _validator;

        public EfCreateTrackCommand(ChinookContext context, IMapper mapper, CreateTrackValidator validator)
        {
            _context = context;
            _mapper = mapper;
            _validator = validator;
        }

        public int Id => 5;

        public string Name => "Create tracks";

        public void Execute(TrackDto dto)
        {
            _validator.ValidateAndThrow(dto);

            var tracks = _context.Track.ToList();

            dto.Id = 0;

            foreach(var t in tracks)
            {
                if(t.TrackId > dto.Id)
                {
                    dto.Id = t.TrackId;
                }
            }

            dto.Id = dto.Id + 1;

            var track = _mapper.Map<Track>(dto);

            track.TrackId = dto.Id;

            _context.Track.Add(track);

            _context.SaveChanges();
        }
    }
}
