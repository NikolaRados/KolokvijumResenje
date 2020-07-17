using Application.Commands;
using Application.Exceptions;
using DataAccess.Context;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Implementation.Commands
{ 
    public class EfDeletePlaylistCommand : IDeletePlaylistCommand
    {
        private readonly ChinookContext context;

        public EfDeletePlaylistCommand(ChinookContext context)
        {
            this.context = context;
        }

        public int Id => 2;

        public string Name => "Delete playlist";

        public void Execute(int request)
        {
            var playlist = context.Playlist.Find(request);

            if(playlist == null)
            {
                throw new EntityNotFoundException(request, typeof(Playlist));
            }

            if(context.PlaylistTrack.Any(x => x.PlaylistId == request))
            {
                throw new Exception("Playlist has tracks");
            }

            context.Remove(playlist);

            context.SaveChanges();
        }
    }
}
