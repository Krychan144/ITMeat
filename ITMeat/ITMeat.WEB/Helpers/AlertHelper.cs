using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITMeat.WEB.Models.Common;

namespace ITMeat.WEB.Helpers
{
    public class AlertHelper
    {
        private readonly List<AlertModel> Alerts = new List<AlertModel>();

        public List<AlertModel> GetAlerts()
        {
            return Alerts;
        }

        public void Success(string message = "Action completed", string moreInfo = null)
        {
            Alerts.Add(new AlertModel
            {
                Message = message,
                MoreInfo = moreInfo,
                Type = AlertModel.Class.Success
            });
        }

        public void Info(string message, string moreInfo = null)
        {
            Alerts.Add(new AlertModel
            {
                Message = message,
                MoreInfo = moreInfo,
                Type = AlertModel.Class.Info
            });
        }

        public void Warning(string message = "Your data is invalid", string moreInfo = null)
        {
            Alerts.Add(new AlertModel
            {
                Message = message,
                MoreInfo = moreInfo,
                Type = AlertModel.Class.Warning
            });
        }

        public void Danger(string message, string moreInfo = null)
        {
            Alerts.Add(new AlertModel
            {
                Message = message,
                MoreInfo = moreInfo,
                Type = AlertModel.Class.Error
            });
        }
    }
}