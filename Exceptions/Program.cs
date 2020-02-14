using System;
using System.IO;

namespace Exceptions
{
    class Program
    {
        static void Main(string[] args)
        {
            House house = new House(1000, "Calea Iesilor str.");
            Person person1 = new Person(null, "Vlad");
            Person person2 = new Person(null, "Max");
            Console.WriteLine($"Person1 house before selling: { person1.House?.Address }");
            Console.WriteLine($"Person2 house before selling: { person2.House?.Address }");
            #region Catch exception with if statement before it happens in the SellHouse method
            //if (person1.House.Address == null)
            //{
            //    Console.WriteLine("Person1 doesn't have a house for selling");
            //}
            //else
            //{
            //    SellHouse(person1, person2);
            //}
            #endregion
            StreamWriter streamWriter = new StreamWriter("a.txt"); // try inside try?
            try
            {
                house.Price = 40000;
                SellHouse(person1, person2);
                Console.WriteLine($"Person1 house after selling: { person1.House?.Address }");
                Console.WriteLine($"Person2 house after selling: { person2.House?.Address }");
                House[] houses =
                {
                    new House(30000, "kkdkdkd"),
                    new House(100000, "dewkoe"),
                    new House(500000, "eirie")
                };
                int overallPrice = CalculateOverallPrice(houses);
                streamWriter.Write(overallPrice);
            }
            catch (NoHouseException e) when (person1.Name.Equals("Max")) // why use when???
            {
                Console.WriteLine(e.Source);
                Console.WriteLine(e.StackTrace);
                Console.WriteLine(e.Message);
            }
            catch (NoHouseException e)
            {
                person1.House = house;
                SellHouse(person1, person2);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                streamWriter.Close();
            }

        }
        static int CalculateOverallPrice(House[] houses)
        {
            int res = 0;
            foreach (var house in houses)
            {
                res += house.Price;
            }

            return res;
        }
        static void SellHouse(Person person1, Person person2)
        {
            if (person1.House == null)
            {
                throw new NoHouseException(person1.Name);
            }
            else
            {
                person2.House = person1.House;
                person1.House = null;
            }
        }
    }

    class Person
    {
        public House House { get; set; }
        public string Name { get; set; }
        public Person(House house, string name)
        {
            House = house;
            Name = name;
        }
    }

    class House
    {
        private int price;
        public int Price
        {
            get
            {
                return price;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("value");
                }
                else
                {
                    price = value;
                }
            }
        }
        public string Address { get; set; }
        public House(int price, string address)
        {
            Price = price;
            Address = address;
        }

    }
    class NoHouseException : Exception
    {
        public string Message { get; set; }
        public NoHouseException(string name)
        {
            Message = $"Exception: { name } has no house for selling";
        }
    }
}