using System;

namespace MasterMealWA.Client.Services.Interfaces
{
    public interface IRerenderService
    {
        event Action RefreshRequested;

        void CallRequestRefresh();
    }
}