using System;
using System.Collections.Generic;
using System.Text;

namespace USocketNet.Model
{
    public enum MsgTypes
    {
        Default = 0,
        Server = 1,
        Private = 2
    }

    public class Message
    {
        /// <summary>
        /// Status of a message.
        /// </summary>
        public string status = string.Empty;

        /// <summary>
        /// Message type.
        /// </summary>
        public MsgTypes types = MsgTypes.Default;

        /// <summary>
        /// Username of Sender
        /// </summary>
        public string u = string.Empty;

        /// <summary>
        /// Sender
        /// </summary>
        public string s = string.Empty;

        /// <summary>
        /// Message
        /// </summary>
        public string m = string.Empty;

        /// <summary>
        /// Receiver
        /// </summary>
        public string r = string.Empty;

        /// <summary>
        /// Date
        /// </summary>
        public string d = string.Empty;

        /// <summary>
        /// Debuggable string.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "status:"+status+",type:"+types+ ",username:"+u+",sender:"+s+",message:"+",receiver:"+r+",datetime:"+d;
        }
    }
}
