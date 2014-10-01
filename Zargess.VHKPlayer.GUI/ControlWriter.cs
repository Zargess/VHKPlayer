using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Zargess.VHKPlayer.GUI {
    public class ControlWriter : TextWriter {
        private Terminal _term;
        private Dispatcher _disp;

        public ControlWriter(Terminal t, Dispatcher d) {
            _term = t;
            _disp = d;
        }

        public override void Write(object value) {
            try {
                _term.AppendText(value);
            } catch (InvalidOperationException ie) {
                _disp.Invoke(() => _term.AppendText(value));
            }
        }

        public override void WriteLine(object value) {
            try {
                _term.AppendText(value);
            } catch (InvalidOperationException ie) {
                _disp.Invoke(() => _term.AppendText(value));
            }
        }

        public override void WriteLine(string value) {
            try {
                _term.AppendText(value);
            } catch (InvalidOperationException ie) {
                _disp.Invoke(() => _term.AppendText(value));
            }
        }

        public override Encoding Encoding {
            get { throw new NotImplementedException(); }
        }
    }
}
