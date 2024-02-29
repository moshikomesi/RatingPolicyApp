using System;

namespace TestRating
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Insurance Rating System Starting...");

            

            // Create an instance of the RatingEngine
            var engine = new RatingEngine();

            // Rate the policy using the RatingEngine
            engine.Rate();

            // Check if a rating was produced
            if (engine.Rating > 0)
            {
                Console.WriteLine($"Rating: {engine.Rating}");
            }
            else
            {
                Console.WriteLine("No rating produced.");
            }
        }
    }
}
