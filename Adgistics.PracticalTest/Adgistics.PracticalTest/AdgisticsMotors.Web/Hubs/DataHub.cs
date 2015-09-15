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

        public void SendTotal(int total)
        {
            context.Clients.All.addTotalToPage(total);
        }

        public void SendProgress(int completed, int processing, int failed)
        {
            context.Clients.All.addQueueStatusToPage(completed, processing, failed);
        }

        public void CompleteDataCollection()
        {
            context.Clients.All.completeDataCollection();
        }
    }
}