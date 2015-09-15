using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace AdgisticsMotorsReport.Web.Hubs
{
    public class DataHub : Hub
    {
        IHubContext context = GlobalHost.ConnectionManager.GetHubContext<DataHub>();

        public void SendProgress(string status)
        {
            context.Clients.All.addQueueStatusToPage(status);
        }

        public void CompleteDataCollection()
        {
            context.Clients.All.completeDataCollection();
        }
    }
}