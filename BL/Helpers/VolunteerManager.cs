﻿
using DalApi;

namespace Helpers;

internal static class VolunteerManager
{
    private static IDal s_dal = Factory.Get; //stage 4
    internal static ObserverManager Observers = new(); //stage 5 
}
