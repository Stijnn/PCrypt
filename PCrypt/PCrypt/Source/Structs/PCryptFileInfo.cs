using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCrypt.Source.Structs
{
    class PCryptFileInfo
    {
        private string fpath;
        private string passkey;

        public PCryptFileInfo(string fpath, string passkey)
        {
            this.fpath = fpath;
            this.passkey = passkey;
        }

        public string FPath { get => fpath; set => fpath = value; }
        public string PassKey { get => passkey; set => passkey = value; }
    }
}
