using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.IO;

namespace TestRating
{
    /// <summary>
    /// The RatingEngine reads the policy application details from a file and produces a numeric 
    /// rating value based on the details.
    /// </summary>
    public class RatingEngine
    {
        public decimal Rating { get; set; }
        public void Rate()
        {
            // log start rating
            Console.WriteLine("Starting rate.");

            Console.WriteLine("Loading policy.");

            string policyJson = File.ReadAllText("policy.json");

            var settings = new JsonSerializerSettings
            {
                Converters = { new StringEnumConverter() },
                TypeNameHandling = TypeNameHandling.Auto // Include type information in JSON
            };
            Policy policy = null;

            // Deserialize the JSON into a JObject to extract the "type" property
            JObject jsonObject = JObject.Parse(policyJson);

            // Based on the "type" property, deserialize the JSON into the corresponding subclass
            string policyTypeString = jsonObject["type"].ToObject<string>();

            // Parse the policy type string into PolicyType enum
            PolicyType policyType;

            if (Enum.TryParse(policyTypeString, out policyType))
            {
                switch (policyType)
                {
                    case PolicyType.Travel:
                        policy = JsonConvert.DeserializeObject<Travel>(policyJson, settings);
                        break;
                    case PolicyType.Life:
                        policy = JsonConvert.DeserializeObject<LifeInsurance>(policyJson, settings);
                        break;
                    case PolicyType.Health:
                        policy = JsonConvert.DeserializeObject<Health>(policyJson, settings);
                        break;
                    default:
                        Console.WriteLine("Unknown policy type");
                        break;
                }
            }
            else
            {
                Console.WriteLine($"Invalid policy type: {policyTypeString}");
            }



            // Call the Rate method of the specific policy type
            Rating = policy.Rate();

            Console.WriteLine("Rating completed.");

        }

    }
}
