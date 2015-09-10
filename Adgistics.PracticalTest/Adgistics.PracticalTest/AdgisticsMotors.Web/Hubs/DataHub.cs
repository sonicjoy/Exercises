using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace AdgisticsMotors.Web.Hubs
{
    public class DataHub : Hub
    {
        public void SendProgress(string name, string message)
        {
            // Call the addNewMessageToPage method to update clients.
            Clients.Caller.addNewMessageToPage(name, message);
        }
    }
}