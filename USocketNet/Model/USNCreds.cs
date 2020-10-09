using System;
using System.Collections.Generic;
using System.Text;

namespace USocketNet.Model
{
    /// <summary>
    /// USocketNet credential Model based on DataVice WP plugin.
    /// </summary>
    public class USNCreds
    {
        public string wpid = string.Empty;
        public string snky = string.Empty;

        public USNCreds(string wordpressId, string sessionKey)
        {
            wpid = wordpressId;
            snky = sessionKey;
        }
    }
}
