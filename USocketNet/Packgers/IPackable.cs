﻿namespace USocketNet.Packgers
{
    public interface IPackable
    {
        string Pack(SocketIO client, string text);
    }
}
