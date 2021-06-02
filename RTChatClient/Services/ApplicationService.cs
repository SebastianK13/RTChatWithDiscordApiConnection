using System;
using System.Collections.Generic;
using System.Text;

namespace RTChatClient.Services
{
    public class ApplicationService
    {
        private SignalRConnection connection;

        public ApplicationService(SignalRConnection connection)
        {
            this.connection = connection;
        }

        public void OnExitEventHandler(object sender, EventArgs args) =>
            connection.BreakConnection();
    }
}
