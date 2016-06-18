﻿using ElGitano.DAL;
using ElGitano.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ElGitano.Apis
{
    public class HomeApiController : ApiController
    {
        [HttpGet]
        public List<Publicacion> GetPublicaciones()
        {            
            var HDataAccess = new HomeDataAccess();
            return HDataAccess.GetPublicaciones();            
        }
    }
}
