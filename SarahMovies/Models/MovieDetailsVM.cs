using System.ComponentModel.DataAnnotations.Schema;

namespace SarahMovies.Models
{
    public class MovieDetailsVM
    {
        public Movie Movie { get; set; }
        public List<MovieTweet>? Tweets { get; set; }
        public double AverageTweetSentiment()
        {
            if (Tweets == null) return 0;
            int validTweets = 0;
            double totalTweetScore = 0;
            foreach (MovieTweet tweet in Tweets)
            {
                if (tweet.Sentiment != 0)
                {
                    validTweets++;
                    totalTweetScore += tweet.Sentiment;

                }
            }
            return totalTweetScore / validTweets;
        }
    }
}
