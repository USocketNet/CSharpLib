﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using USocketNet.EventArguments;
using USocketNet.Model;

namespace USocketNet
{
    public class USNDelivery
    {
        #region Fields

        /// <summary>
        /// Constant target port for this USocketNet module.
        /// </summary>
        private const int protocol = 5050;

        /// <summary>
        /// Name of this USN Module.
        /// </summary>
        private const string moduleName = "USocketNet Delivery Module";

        /// <summary>
        /// Check and Set if Logging is enabled.
        /// </summary>
        private bool isProduction = false;

        /// <summary>
        /// Get or privately Set this Module instance setup status.
        /// </summary>
        private bool isInitialized = false;

        /// <summary>
        /// Singleton SocketIO client instance.
        /// </summary>
        private SocketIO socket;

        #endregion

        #region Helpers

        /// <summary>
        /// Locally get and Process USNOptions to Uri.
        /// </summary>
        /// <param name="option"></param>
        /// <returns></returns>
        private Uri GetUri(USNOptions option)
        {
            string sslString = option.isSecure ? "https://" : "http://";
            return new Uri(sslString + option.hostname + ":" + protocol);
        }

        /// <summary>
        /// Console writeline if instance is on production.
        /// </summary>
        /// <param name="message"></param>
        public void Log(string message)
        {
            if(!isProduction)
            {
                Console.WriteLine( moduleName + " - " + message);
            }
        }

        #endregion

        #region Variables

        /// <summary>
        /// Check if this USN Module is connected or not.
        /// </summary>
        public bool IsConnected
        {
            get
            {
                return socket.Connected;
            }
        }

        /// <summary>
        /// Get the current Socket ID.
        /// </summary>
        public string GetSocketID
        {
            get
            {
                if(!socket.Connected)
                {
                    return "failed";
                }

                return socket.Id;
            }
        }

        private double latestPingMS = 0.00;
        public double GetPingMS
        {
            get
            {
                return latestPingMS;
            }
        }

        #endregion

        #region Singleton

        /// <summary>
        /// Single instance of USocketNet Messaging modules.
        /// </summary>
        public static USNDelivery Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new USNDelivery();
                }

                return instance;
            }
        }
        private static USNDelivery instance = null;

        #endregion

        #region Constructors

        /// <summary>
        /// Initialized the current instance of USocketNet module.
        /// </summary>
        /// <param name="ssl"></param>
        /// <param name="hostname"></param>
        /// <param name="port"></param>
        /// <param name="production"></param>
        public void Initialize(USNOptions option, USNCreds creds)
        {
            isProduction = option.production;
            
            //Create Socket Connection with Credentials.
            socket = new SocketIO(GetUri(option), new SocketIOOptions
            {
                Query = new Dictionary<string, string>
                {
                    {"wpid", creds.wpid },
                    {"snid", creds.snky }
                },
                ConnectionTimeout = TimeSpan.FromSeconds(option.timeout)
            });

            socket.OnConnected += SocketIO_OnConnected;
            socket.OnReconnecting += SocketIO_OnReconnecting;
            socket.OnDisconnected += SocketIO_OnDisconnected;
            socket.OnReceivedEvent += SocketIO_EventReceived;
            socket.OnPing += SocketIO_OnPing;
            socket.OnPong += SocketIO_OnPong;
            socket.OnError += SocketIO_OnError;

            isInitialized = true;
        }

        /// <summary>
        /// Connect the Message module if previously initialized.
        /// </summary>
        public async void Connect()
        {
            if(isInitialized)
            {
                if(!IsConnected)
                {
                    try
                    {
                        await socket.ConnectAsync();
                    }

                    catch (Exception ex)
                    {
                        Log("Exception: " + ex);
                    }
                }
                
                else
                {
                    Log("You are currently connected to server.");
                }
            }
            
            else
            {
                Log("Instance is not yet initialized.");
            }
        }

        /// <summary>
        /// Forcibly disconnect client from the server.
        /// </summary>
        public async void Disconnect()
        {
            if(IsConnected)
            {
                await socket.DisconnectAsync();
            }

            else
            {
                Log("Youre not connected yet!");
            }
        }

        #endregion

        #region Methods

        public async void JoinOrderChannel(string orderKey, Action<USNResponse> callback)
        {
            await socket.EmitAsync("join", response =>
            {
                Log("Callback - " + response.ToString());

                USNResponse returning = response.GetValue<USNResponse>(0);

                if (callback != null)
                {
                    callback(returning);
                }
            }, orderKey);
        }

        /// <summary>
        /// Send message privately to an existing wpid.
        /// </summary>
        /// <param name="orderItem"></param>
        /// <param name="callback"></param>
        public async void SetOrderStatus(OrderItem orderItem, Action<USNResponse> callback)
        {
            string sending = JsonConvert.SerializeObject(orderItem);

            await socket.EmitAsync("status", response =>
            {
                Log("Callback - " + response.ToString());

                USNResponse returning = response.GetValue<USNResponse>(0);

                if(callback != null)
                {
                    callback(returning);
                }
            }, sending);
        }

        #endregion

        #region Events

        private void SocketIO_EventReceived(object sender, ReceivedEventArgs e)
        {
            if (e.Event.ToString() == "notify")
            {
                // Do something...
            }

            if (e.Event.ToString() == "status")
            {
                OrderItem orderStatus = e.Response.GetValue<OrderItem>(0);

                if (OnOrderStatus != null)
                {
                    OnOrderStatus(orderStatus);
                    Log($"Event=>{ orderStatus.ToString() } ");
                }
            }

            Log($"Event=>{ e.Event.ToString() } ");
        }
        public Action<OrderItem> OnOrderStatus = null;

        #endregion

        #region Listener

        private void SocketIO_OnConnected(object sender, EventArgs e)
        {
            if (OnConnected != null)
            {
                OnConnected(e.ToString());
            }

            Log($"Connected: response = { e }");
        }
        public Action<string> OnConnected = null;

        private void SocketIO_OnReconnecting(object sender, int e)
        {
            if (OnReconnecting != null)
            {
                OnReconnecting(e.ToString());
            }

            Log($"Reconnecting: attempt = { e }");
        }
        public Action<string> OnReconnecting = null;

        private void SocketIO_OnDisconnected(object sender, string e)
        {
            if (OnDisconnected != null)
            {
                OnDisconnected(e.ToString());
            }

            Log($"Disconnected: response = { e }");
        }
        public Action<string> OnDisconnected = null;

        private void SocketIO_OnPing(object sender, EventArgs e)
        {
            if (OnPingSent != null)
            {
                OnPingSent(e.ToString());
            }

            Log($"PING: sent = { e }");
        }
        public Action<string> OnPingSent = null;

        private void SocketIO_OnPong(object sender, TimeSpan e)
        {
            latestPingMS = e.TotalMilliseconds;

            if (OnPingReceived != null)
            {
                OnPingReceived(latestPingMS);
            }

            Log($"PING: received = { e.TotalMilliseconds } ms");
        }
        public Action<double> OnPingReceived = null;

        private void SocketIO_OnError(object sender, string e)
        {
            if (OnError != null)
            {
                OnError(e.ToString());
            }

            Log($"Error: response = { e }");
        }
        public Action<string> OnError = null;

        #endregion
    }
}
