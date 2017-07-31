using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCrypt.Source.Structs
{
    class PCryptUser
    {
        private string uuid;
        private string Username;

        public PCryptUser(string uniqueidentifier)
        {
            this.uuid = uniqueidentifier;

            SetupUser();
        }

        private void SetupUser()
        {
            
        }
    }
}
