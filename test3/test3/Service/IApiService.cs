﻿using System;
using System.Collections.Generic;
using System.Text;
using test3.Models;
using System.Threading.Tasks;

namespace test3.Service
{
    public interface IApiService
    {
        Task<Response> GetProblemAsync(string urlBase, string servicePrefix, string controller, UserRequest request);
    }
}