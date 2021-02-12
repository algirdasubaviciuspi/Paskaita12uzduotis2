using System;
using System.Collections.Generic;
using System.Text;

namespace Paskaita12.uzduotis
{
    public class Product
    {           // bazinė klasė	
        public string name { get; set; }
        public double price { get; set; }
        public double weight { get; set; }
        public Product() { }
        public Product(string name, double price, double weight)
        {
            this.name = name;
            this.price = price;
            this.weight = weight;
        }
        public override string ToString()
        {
            return String.Format("{0,12} {1,10:f2} {2,10:f1}", name, price, weight);
        }
    }
    public class Milk : Product
    {           // išvestinė klasė pieno produktams	
        public double fatness { get; set; }
        public DateTime date { get; set; }
        public Milk() { }
        public Milk(string name, double price, double weight, double fatness, DateTime date) :
            base(name, price, weight)
        {
            this.fatness = fatness;
            this.date = date;
        }
        public override string ToString()
        {
            return String.Format("{0,6} {1} {2,10} {3,15:d} {4,15:d} {5,10} {6,10}",
                "p", base.ToString(), fatness, date, "-", "-", "-");
        }
        public bool Condition()
        {           // tikrina ar galiojimo laikas iki 3 dienų	
            DateTime CurrentDate = DateTime.Now;
            if ((date > CurrentDate) && (date < CurrentDate.AddDays(3)))
                return true;
            else return false;
        }
        public static bool operator <(Milk m1, Milk m2)
        {           // lygina "fatness" reikšmes rikiavimui	
            if (m1.fatness < m2.fatness)
                return true;
            return false;
        }
        public static bool operator >(Milk m1, Milk m2)
        {           // lygina "fatness" reikšmes rikiavimui	
            if (m1.fatness > m2.fatness)
                return true;
            return false;
        }
    }
    public class Vegetable : Product
    {               // išvestinė tarpinė klasė daržovėms	
        public DateTime date { get; set; }
        public Vegetable() { }
        public Vegetable(string name, double price, double weight, DateTime date) :
            base(name, price, weight)
        {
            this.date = date;
        }
    }
    public class LeafyVegetable : Vegetable
    {               // išvestinė klasė lapinėms daržovėms	
        public int vitaminA { get; set; }
        public LeafyVegetable() { }
        public LeafyVegetable(string name, double price, double weight, DateTime date, int vitaminA) :
            base(name, price, weight, date)
        {
            this.vitaminA = vitaminA;
        }
        public override string ToString()
        {
            return String.Format("{0,6} {1} {2,10} {3,15:d} {4,15:d} {5,10} {6,10}",
                "l", base.ToString(), "-", "-", date, vitaminA, "-");
        }
        public bool VitaminAMore(LeafyVegetable other)
        {           // lygina "vitaminA" reikšmes rikiavimui	
            if (vitaminA > other.vitaminA)
                return true;
            else return false;
        }

    }
    public class FruitVegetable : Product
    {               // išvestinė klasė vaisinėms daržovėms	
        public DateTime date { get; set; }
        public int sweetness { get; set; }
        public FruitVegetable(string name, double price, double weight, DateTime date, int sweetness) :
            base(name, price, weight)
        {
            this.date = date;
            this.sweetness = sweetness;
        }
        public override string ToString()
        {
            return String.Format("{0,6} {1} {2,10} {3,15:d} {4,15:d} {5,10} {6,10}",
                "v", base.ToString(), "-", "-", date, "-", sweetness);
        }
        public bool Condition()
        {       // tikrina ar "sweetness" reikšmė > 3	
            if (sweetness > 3)
                return true;
            else return false;
        }
    }
}

