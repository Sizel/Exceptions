using System;

namespace Exceptions
{
    class Program
    {
        static void Main(string[] args)
        {
            House house = new House(1000, "Calea Iesilor str.");
            Person person1 = new Person(null, "Max");
            Person person2 = new Person(null, "Vlad");
            Console.WriteLine($"Person1 house before selling: { person1.House?.Address }");
            Console.WriteLine($"Person2 house before selling: { person2.House?.Address }");
            try
            {
                SellHouse(person1, person2);
            }
            catch (NoHouseException e)
            {
                Console.WriteLine(e.Source);
                Console.WriteLine(e.StackTrace);
                Console.WriteLine(e.Message);
            }
            catch (IndexOutOfRangeException)
            {

            }
            Console.WriteLine($"Person1 house after selling: { person1.House?.Address }");
            Console.WriteLine($"Person2 house after selling: { person2.House?.Address }");
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

            //if (person1.House == null)
            //{
            //    Console.WriteLine("Person1 doesn't have a house to sell");
            //}
            //else
            //{
            //    person2.House = person1.House;
            //    person1.House = null;
            //}
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
        public int Price { get; set; }
        public string Address { get; set; }
        public House(int price, string address)
        {
            Price = price;
            Address = address;
        }

    }
    class NoHouseException : Exception
    {
        public NoHouseException(string name)
        {
            Console.WriteLine($"{ name } doesn't have a house to sell");
        }
    }


}
