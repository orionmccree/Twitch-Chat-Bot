using System;


// starting the class of OnionBot
namespace OnionBot
{
    //making our main class function
    class Program
    {
        /*
         * Here we are calling to our main string using our Bot class, which we defined in Bot.cs, naming it Onion, then declaring a new string for it's parameters
         * Next we use our declared bot Onion to start a Connection argument with our JSON information. We then have it read the lines from Twitch chat.
         * Lastly, it will disconnect from the information from our JSON argument (the twitch connection) when we close it.
         */

        static void Main(string[] args)
        {
            Bot Onion = new Bot();

            Onion.Connect(true);

            Console.ReadLine();

            Onion.Disconnect();
        }
    }
}