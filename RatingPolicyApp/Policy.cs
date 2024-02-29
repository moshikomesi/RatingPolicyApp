using System;

namespace TestRating
{
    public abstract class Policy
    {
        public PolicyType Type { get; set; }
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }

        public abstract decimal Rate();
    }

    public class LifeInsurance : Policy
    {
        public bool IsSmoker { get; set; }
        public decimal Amount { get; set; }

        public override decimal Rate()
        {
            Console.WriteLine("Rating LIFE policy...");
            Console.WriteLine("Validating policy.");
            decimal Rating = 0;
            // Validate DateOfBirth
            if (DateOfBirth == DateTime.MinValue)
            {
                Console.WriteLine("Life policy must include Date of Birth.");
                return Rating; // Return a default value or handle the error accordingly
            }
            // Validate age eligibility
            if (DateOfBirth < DateTime.Today.AddYears(-100))
            {
                Console.WriteLine("Max eligible age for coverage is 100 years.");
                return Rating; // Return a default value or handle the error accordingly
            }
            // Validate Amount
            if (Amount == 0)
            {
                Console.WriteLine("Life policy must include an Amount.");
                return Rating; // Return a default value or handle the error accordingly
            }

            // Calculate age
            int age = DateTime.Today.Year - DateOfBirth.Year;
            if (DateOfBirth.Month == DateTime.Today.Month &&
                DateTime.Today.Day < DateOfBirth.Day ||
                DateTime.Today.Month < DateOfBirth.Month)
            {
                age--;
            }
            // Calculate base rate
             Rating = Amount * age / 200;
            // Apply smoker rate if applicable
            if (IsSmoker)
            {
                return Rating * 2;
            }

            return Rating; // Return the calculated rate
        }

    }

    public class Travel : Policy
    {
        public string Country { get; set; }
        public int Days { get; set; }

        public override decimal Rate()
        {

            Console.WriteLine("Rating TRAVEL policy...");
            Console.WriteLine("Validating policy.");
            decimal Rating = 0; 
            if (Days <= 0)
            {
                Console.WriteLine("Travel policy must specify Days.");
                return Rating;
            }
            if (Days > 180)
            {
                Console.WriteLine("Travel policy cannot be more then 180 Days.");
                return Rating;
            }
            if (String.IsNullOrEmpty(Country))
            {
                Console.WriteLine("Travel policy must specify country.");
                
                    return Rating;
            }
              Rating = Days * 2.5m;
            if (Country == "Italy")
            {
                Rating *= 3;
            }

            return Rating; 
        }
    }

    public class Health : Policy
    {
        public string Gender { get; set; }
        public decimal Deductible { get; set; }

        public override decimal Rate()
        {
            decimal Rating = 0;

            Console.WriteLine("Rating HEALTH policy...");
            Console.WriteLine("Validating policy.");
            if (String.IsNullOrEmpty(Gender))
            {
                Console.WriteLine("Health policy must specify Gender");
                return Rating;
            }
           
            if (Gender == "Male")
            {
               

                if (Deductible < 500)
                {
                    Rating = 1000m;
                }
                Rating = 900m;
            }
            else
            {
                if (Deductible < 800)
                {
                    Rating = 1100m;
                }
                Rating = 1000m;
            }

            return Rating;
        }
    }





}
