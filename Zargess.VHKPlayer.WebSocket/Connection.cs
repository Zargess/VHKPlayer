using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Alchemy.Classes;

namespace Zargess.VHKPlayer.WebSocket {
    public class Connection {
        public Timer Timer { get; set; }
        public UserContext Context { get; set; }
        public Connection() {
            //Timer = new Timer(TimerCallback, null, 0, 1000);
        }

        private void TimerCallback(object state) {
            try {
                // Sending Data to the Client
                //Context.Send("[" + Context.ClientAddress + "] " + DateTime.Now);
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }

        }

        public void SendMessage(string msg) {
            try {
                Context.Send(msg);
            } catch (Exception e) {
                Console.WriteLine(e.Message);
            }
        }
    }
}
