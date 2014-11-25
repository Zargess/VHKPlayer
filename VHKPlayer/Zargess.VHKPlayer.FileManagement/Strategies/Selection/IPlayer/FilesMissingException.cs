using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zargess.VHKPlayer.FileManagement.Strategies.Selection.IPlayer {
    public class FilesMissingException : Exception {
        public string Message { get; private set; }
        public FilesMissingException(string message) {
            Message = message;
        }
    }
}
