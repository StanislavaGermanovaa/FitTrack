﻿using FitTrack.Models.DTO;
using FitTrack.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTrack.BL.Interfaces
{
    public interface IBlUserService
    {
        List<Subscription> GetUserWithSubscriptions(string userId);
        bool UpdateSubscriptionForUser(string userId, SubscriptionRequest request);
    }
}
