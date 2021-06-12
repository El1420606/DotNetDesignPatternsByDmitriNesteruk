using System;
using static System.Console;

namespace FacetedBuilder
{
    public class Person
    {
        //address
        public string StreetAddress, Postcode, City;

        // employment
        public string CompanyName, Position;
        public int AnnualIncome;

        public override string ToString()
        {
            return $"{nameof(StreetAddress)}:{StreetAddress},{nameof(Postcode)}:{Postcode},{nameof(City)}:{City},{nameof(CompanyName)}:{CompanyName},{nameof(Position)}:{Position},{nameof(AnnualIncome)}:{AnnualIncome}";
        }
    }

    public class PersonBuilder //facade
    {
        protected Person person = new Person();

        public PersonJobBuilder Works => new PersonJobBuilder(person);
        public PersonAddressBuilder Lives => new PersonAddressBuilder(person);

        public static implicit operator Person(PersonBuilder pb)
        {
            return pb.person;
        }
    }

    #region PersonJobBuilder
    public class PersonJobBuilder : PersonBuilder
    {
        public PersonJobBuilder(Person person)
        {
            this.person = person;
        }

        public PersonJobBuilder At(string companyName)
        {
            person.CompanyName = companyName;
            return this;
        }

        public PersonJobBuilder WithPostcode(string postcode)
        {
            person.Postcode = postcode;
            return this;
        }

        public PersonJobBuilder AsA(string position)
        {
            person.Position = position;
            return this;
        }

        public PersonJobBuilder Earning(int amount)
        {
            person.AnnualIncome = amount;
            return this;
        }

        public PersonJobBuilder In(string city)
        {
            person.City = city;
            return this;
        }
    }
    #endregion

    #region PersonAddressBuilder
    public class PersonAddressBuilder : PersonBuilder
    {
        public PersonAddressBuilder(Person person)
        {
            this.person = person;
        }

        public PersonAddressBuilder At(string companyName)
        {
            person.CompanyName = companyName;
            return this;
        }

        public PersonAddressBuilder AsA(string position)
        {
            person.Position = position;
            return this;
        }

        public PersonAddressBuilder WithPostcode(string postcode)
        {
            person.Postcode = postcode;
            return this;
        }

        public PersonAddressBuilder Earning(int amount)
        {
            person.AnnualIncome = amount;
            return this;
        }

        public PersonAddressBuilder In(string city)
        {
            person.City = city;
            return this;
        }
    }
    #endregion


    class FacetedConsole
    {
        static void Main(string[] args)
        {
            var pb = new PersonBuilder();
            Person person = pb
                .Lives.At("Lodon")
                .In("哈哈哈")
                .WithPostcode("21313")
                .Works.At("家里蹲大学")
                .AsA("开发")
                .Earning(5000);
            WriteLine(person);
        }
    }
}
