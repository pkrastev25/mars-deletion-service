using System;
using mars_deletion_svc.Exceptions;
using mars_deletion_svc.Services.Inerfaces;
using Microsoft.AspNetCore.Mvc;

namespace mars_deletion_svc.Services
{
    public class ErrorService : IErrorService
    {
        public StatusCodeResult GetStatusCodeResultForError(Exception error)
        {
            if (error is ResourceConflictException)
            {
                return new StatusCodeResult(409);
            }

            return new StatusCodeResult(500);
        }
    }
}