using Application.DataTransfer;
using DataAccess.Context;
using FluentValidation;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Implementation.Validators
{
    public class CreateTrackValidator : AbstractValidator<TrackDto>
    {
        private readonly ChinookContext _context;

        public CreateTrackValidator(ChinookContext context)
        {
            _context = context;

            RuleFor(x => x.Milliseconds).GreaterThanOrEqualTo(90000).WithMessage("Pesma ne moze biti kraca od 90 sekundi");

            RuleFor(x => x.UnitPrice).GreaterThanOrEqualTo(0).WithMessage("Cena ne moze biti manja od 0");

            RuleFor(x => x.Bytes).GreaterThanOrEqualTo(1000000).WithMessage("Velicina ne moze biti manja od 1mb");

            RuleFor(x => x.Name).NotEmpty().WithMessage("Name je obavezan parametar");

            RuleFor(x => x.MediaTypeId).NotEmpty().WithMessage("MediaTypeId je obavezan parametar")
                .Must(x => _context.MediaType.Any(m => m.MediaTypeId == x))
                .WithMessage(x => $"Ne postoji mediatype koji ima id : {x.MediaTypeId}");

            RuleFor(x => x.Composer).NotEmpty().WithMessage("Composer je obavezan parametar");
        }
    }
}
