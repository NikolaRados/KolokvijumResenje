using Application;
using Application.Exceptions;
using Microsoft.AspNetCore.Routing.Template;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api
{
    public class UseCaseExecutor
    {
        private readonly IApplicationActor _actor;

        public UseCaseExecutor(IApplicationActor actor)
        {
            _actor = actor;
        }

        public void ExecuteCommand<TRequest>(ICommand<TRequest> command, TRequest request)
        {
            if(!_actor.AllowUseCases.Contains(command.Id))
            {
                throw new UnauthorizedUseCaseException(command, _actor);
            }

            command.Execute(request);
        }

        public TResponse ExecuteQuery<TSearch, TResponse>(IQuery<TSearch, TResponse> query, TSearch search)
        {
            if(!_actor.AllowUseCases.Contains(query.Id))
            {
                throw new UnauthorizedUseCaseException(query, _actor);
            }

            return query.Execute(search);
        }
    }
}
