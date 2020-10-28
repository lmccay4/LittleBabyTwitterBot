using System;
using System.Threading;
using System.Collections;
using System.Globalization;
using Tweetinvi;


namespace LittleBabyDylan
{
    public class Twitter
    {
        static DateTime pullTime;

        public static void CheckForTweets()
        {
            Auth.SetUserCredentials("YOUR TWITTER API TOKENS HERE");
            var user = User.GetAuthenticatedUser();

            long dylanID = //User's specific Twitter account ID number;
            var dylan = User.GetUserFromId(dylanID);

            var timelineParameter = Timeline.CreateUserTimelineParameter();
            timelineParameter.ExcludeReplies = true;
            timelineParameter.IncludeRTS = false;

            var lastTweets = dylan.GetUserTimeline(timelineParameter);
            
            foreach (var tweet in lastTweets) 
            {
                TimeSpan mins = TimeSpan.FromMinutes(4);
                if (tweet.CreatedAt >= pullTime.Subtract(mins)) 
                {
                    if (tweet.Text.Contains("r") || tweet.Text.Contains("R") || tweet.Text.Contains("l") || tweet.Text.Contains("L"))
                    { 
                        var culture = new CultureInfo("en-US");
                        string editedTweet = tweet.Text.Replace("r", "w", true, culture).Replace("l", "w", true, culture);
                        
                        if (!(string.IsNullOrEmpty(editedTweet)))
                        {
                            Tweet.FavoriteTweet(tweet.Id);
                            Tweet.PublishTweetInReplyTo("@" + tweet.CreatedBy.ScreenName + " " + editedTweet, tweet.Id);
                            Console.WriteLine("Tweeted: " + editedTweet);
                        }
                    }
                }
                else if (tweet.CreatedAt < pullTime) 
                {
                    break;
                }
            }
            Console.WriteLine(DateTime.Now + " - Checking again...");
        }

        public static void RegisterPullTime() 
        {
            pullTime = DateTime.Now;

            //This sleeps for 20 minutes to give him time to tweet
            Thread.Sleep(1200000);
        }

    }
}