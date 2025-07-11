﻿using System;
using System.Net.Sockets;
using GameCore;
using LabApi.Features.Console;

namespace WebPlayer.Server.Web.WebSocketServer
{
    ///<summary>
    /// Object for all connectecd clients
    /// </summary>
    public partial class Client
    {

        #region Fields

        ///<summary>The socket of the connected client</summary>
        private Socket _socket;

        ///<summary>The guid of the connected client</summary>
        private string _guid;

        /// <summary>The server that the client is connected to</summary>
        private Server _server;

        /// <summary>If the server has sent a ping to the client and is waiting for a pong</summary>
        private bool _bIsWaitingForPong;

        private string _path; // "GET /path HTTP/1.1"

        #endregion

        #region Class Events

        /// <summary>Create a new object for a connected client</summary>
        /// <param name="Server">The server object instance that the client is connected to</param>
        /// <param name="Socket">The socket of the connected client</param>
        public Client(Server Server, Socket Socket, string Path)
        {
            this._server = Server;
            this._socket = Socket;
            this._guid = Helpers.CreateGuid("client");
            this._path = Path;

            // Start to detect incoming messages 
            GetSocket().BeginReceive(new byte[] { 0 }, 0, 0, SocketFlags.None, messageCallback, null);
        }

        #endregion

        #region Field Getters

        /// <summary>Gets the guid of the connected client</summary>
        /// <returns>The GUID of the client</returns>
        public string GetGuid()
        {
            return _guid;
        }

        ///<summary>Gets the socket of the connected client</summary>
        ///<returns>The socket of the client</return>
        public Socket GetSocket()
        {
            return _socket;
        }

        /// <summary>The socket that this client is connected to</summary>
        /// <returns>Listen socket</returns>
        public Server GetServer()
        {
            return _server;
        }

        /// <summary>Gets if the server is waiting for a pong response</summary>
        /// <returns>If the server is waiting for a pong response</returns>
        public bool GetIsWaitingForPong()
        {
            return _bIsWaitingForPong;
        }
        
        public string GetPath()
        {
            return _path.Split('?')[0];
        }

        #endregion

        #region Field Setters

        /// <summary>Sets if the server is waiting for a pong response</summary>
        /// <param name="bIsWaitingForPong">If the server is waiting for a pong response</param>
        public void SetIsWaitingForPong(bool bIsWaitingForPong)
        {
            _bIsWaitingForPong = bIsWaitingForPong;
        }

        #endregion

        #region Methods

        /// <summary>Called when a message was received from the client</summary>
        private void messageCallback(IAsyncResult AsyncResult)
        {
            try
            {
                GetSocket().EndReceive(AsyncResult);
                Logger.Debug("Message received!");
                // Read the incomming message 
                byte[] messageBuffer = new byte[WebPlayerPlugin.Instance.Config.WsBufferSize];
                int bytesReceived = GetSocket().Receive(messageBuffer);
                Logger.Debug("Buffer received!");

                // Resize the byte array to remove whitespaces 
                if (bytesReceived < messageBuffer.Length) Array.Resize<byte>(ref messageBuffer, bytesReceived);
                Logger.Debug("Buffer resized!");
                
                Logger.Debug(messageBuffer.Length);

                // Log the first few bytes for debugging
                string hexBytes = BitConverter.ToString(messageBuffer, 0, Math.Min(messageBuffer.Length, 10));
                Logger.Debug($"First bytes: {hexBytes}");
                
                // Get the opcode of the frame
                EOpcodeType opcode = Helpers.GetFrameOpcode(messageBuffer);
                Logger.Debug($"Opcode received: {opcode}");

                // If the connection was closed
                if(opcode == EOpcodeType.ClosedConnection)
                {
                    Logger.Debug("Connection closed!");
                    GetServer().ClientDisconnect(this);
                    return;
                }
                Logger.Debug("Connection good!");

                // Pass the message to the server event to handle the logic
                GetServer().ReceiveMessage(this, Helpers.GetDataFromFrame(messageBuffer));
                Logger.Debug("Message passed!");

                // Start to receive messages again
                GetSocket().BeginReceive(new byte[] { 0 }, 0, 0, SocketFlags.None, messageCallback, null);
                Logger.Debug("Listening for new messages!");

            }
            catch(Exception Exception)
            {
                Logger.Error(Exception.Message);
                GetSocket().Close();
                GetSocket().Dispose();
                GetServer().ClientDisconnect(this);
            }
        }

        #endregion

    }
}