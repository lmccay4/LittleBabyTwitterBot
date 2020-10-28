using System;
using System.Diagnostics;

namespace LittleBabyDylan
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch s = new Stopwatch();
            s.Start();

            while (s.Elapsed < TimeSpan.FromDays(60))
            {
                Twitter.RegisterPullTime();
                Twitter.CheckForTweets();
            }

            s.Stop();
        }
    }
}
