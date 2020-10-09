using System;
using System.Collections.Generic;
using System.Text;

namespace USocketNet.Model
{
    public class USNOptions
    {
        /// <summary>
        /// Production or Development instance.
        /// </summary>
        public bool production = false;

        /// <summary>
        /// https or http on connection.
        /// </summary>
        public bool isSecure = false;

        /// <summary>
        /// Hostname, IP Address, or Domain Name.
        /// </summary>
        public string hostname = string.Empty;

        /// <summary>
        /// Timeout in seconds.
        /// </summary>
        public int timeout = 10;

        public USNOptions(bool _isSecure, string _hostname, int _timeout)
        {
            isSecure = _isSecure;
            hostname = _hostname;
            timeout = _timeout;
        }

        public USNOptions(bool _isSecure, string _hostname, int _timeout, bool isProduction)
        {
            isSecure = _isSecure;
            hostname = _hostname;
            timeout = _timeout;
            production = isProduction;
        }
    }
}
