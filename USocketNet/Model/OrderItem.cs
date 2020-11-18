using System;
using System.Collections.Generic;
using System.Text;

namespace USocketNet.Model
{
    /// <summary>
    /// Order Status
    /// </summary>
    public enum OrderStatus
    {
        Pending = 0,
        Accepted = 1,
        Ongoing = 2,
        Preparing = 3,
        Shipping = 4,
        Completed = 5,
        Cancelled = 6,
    }

    /// <summary>
    /// Notification Instance
    /// </summary>
    public class OrderItem
    {
        /// <summary>
        /// Id of the WP user.
        /// </summary>
        public string key = string.Empty;

        /// <summary>
        /// Order Status
        /// </summary>
        public OrderStatus status = OrderStatus.Pending;

        /// <summary>
        /// Data as string. 
        /// </summary>
        public string data = string.Empty;
    }
}
