using OnionChatBot;
using OnionBot;
using System;
using TwitchLib.Api;
using System.Runtime.CompilerServices;
using TwitchLib.Client;
using TwitchLib.Client.Events;
using TwitchLib.Client.Models;

namespace OnionBot
{
    // Create class called Bot, and giving arguments for its functionality 
    internal class Bot
    {
        /* Here we are using the ConnectionCredentials string from TwitchLib.Api, making a new string and naming it Creds.
         * We then call to the information stored in a resource explorer file, called ImportantDoc which contains our twitch Key and Username for the stream chat
         * Lastly we make a new string from the TwitchClient class used from TwitchLib.Api, naming it client, which functions to connect to a client from Twitch
         */
        ConnectionCredentials Creds = new ConnectionCredentials(ImportantDoc.ChannelName, ImportantDoc.BotToken);
        TwitchClient? client;

        // Establish and define the connection function
        internal void Connect(bool isLogging)
        {

            // Create a new TwitchClient then initialize that client using Creds, and ChannelName from the ImportantDoc
            client = new TwitchClient();
            client.Initialize(Creds, ImportantDoc.ChannelName);
            client.OnConnected += Client_OnConnected;

            Console.WriteLine("[Bot]: is connecting...");

            // If the isLogging boolean is true, then it'll add to the log dynamically using the chat
            if (isLogging)
                client.OnLog += Client_OnLog;
            /*
             * This section is used for adding commands to the bot.
             * if something goes wrong, it will log the error.
             */
            client.OnError += Client_OnError;
            client.OnMessageReceived += Client_OnMessageReceived;
            client.OnChatCommandReceived += Client_OnChatCommandReceived;


            // This will establish a connection to the chat using the information.
            client?.Connect();
        }

        // This annouces that the bot has joined in chat.
        private void Client_OnConnected(object? sender, OnConnectedArgs e)
        {
            Console.WriteLine("[Bot]: is connected.");
        }

        // This is a command that can be accessed in chat by anyone
        private void Client_OnChatCommandReceived(object? sender, OnChatCommandReceivedArgs e)
        {
            switch (e.Command.CommandText.ToLower())
            {
                case "Game":
                    client?.SendMessage(ImportantDoc.ChannelName, "The streamer is playing (Game Unspecified).");
                    break;
            }
            /* Checks if the user's display name is also the channel name.
             * This allows the input to only work for the input
             * user (the streamer) with the command sign. "!command" like so.
             */
            if (e.Command.ChatMessage.DisplayName == ImportantDoc.ChannelName)
            {
                switch (e.Command.CommandText.ToLower())
                {
                    case "Hi":
                        client?.SendMessage(ImportantDoc.ChannelName, "Ello, Boss.");
                        break;
                }
            }
        }

        // If something goes wrong, log the error
        private void Client_OnError(object? sender, TwitchLib.Communication.Events.OnErrorEventArgs e)
        {
            throw new NotImplementedException();
        }

        // Allows the bot to tell you who sent the message and what the message was
        private void Client_OnMessageReceived(object? sender, OnMessageReceivedArgs e)
        {
            Console.WriteLine($"[{e.ChatMessage.DisplayName}]: {e.ChatMessage.Message}");
        }

        /*
         * This function defines and allows the data from the chat log to be read by the bot,
         * including null values from the sender, and writes it out in the console.
         */
        private void Client_OnLog(object? sender, OnLogArgs e)
        {
            Console.WriteLine(e.Data);
        }

        // This will allow the bot to be disconnected from the chat
        internal void Disconnect()
        {
            Console.WriteLine("[Bot]: is leaving...");
            client?.Disconnect();
        }
    }
}