using System;
using Microsoft.AspNetCore.Mvc;

namespace mars_deletion_svc.Services.Inerfaces
{
    public interface IErrorService
    {
        StatusCodeResult GetStatusCodeResultForError(Exception error);
    }
}