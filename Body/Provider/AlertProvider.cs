using GlassBoard.Abstractions.Provider;
using GlassBoard.Abstractions.Service;
using GlassBoard.Models.Alerts;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlassBoard.Providers
{
    public class AlertProvider : IAlertProvider
    {
        private readonly IAlertService _alertService;
        
        public List<AlertModel> Alerts { get; private set; } = new();
        public bool IsLoading { get; private set; }
        
        public event Action? OnChange;

        public AlertProvider(IAlertService alertService)
        {
            _alertService = alertService;
        }

        public async Task RefreshAlertsAsync()
        {
            if (IsLoading) return;

            IsLoading = true;
            NotifyStateChanged();

            try 
            {
                Alerts = await _alertService.GetAlertsAsync();
            }
            finally 
            {
                IsLoading = false;
                NotifyStateChanged();
            }
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}