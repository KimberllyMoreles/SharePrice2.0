﻿using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharePrice.Authenticate
{
    public interface IAuthenticate
    {
        Task<MobileServiceUser> AuthenticateAsync(MobileServiceClient client,
            MobileServiceAuthenticationProvider provedor);
    }
}
