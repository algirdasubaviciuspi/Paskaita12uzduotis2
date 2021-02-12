using System;
using System.IO;

namespace Paskaita12.uzduotis
{
    class Program
    {
        const string CFd = "..\\..\\..\\GautiProduktai.txt";
        const string CFr = "..\\..\\..\\IšrūšiuotiProduktai.txt";
        static void Main(string[] args)
        {
            ProductsContainer AllProducts = new ProductsContainer();
            ProductsContainer MilkProducts = new ProductsContainer();
            ProductsContainer LeafyVegetableProducts = new ProductsContainer();
            ProductsContainer FruitVegetableProducts = new ProductsContainer();

            ReadProducts(CFd, AllProducts);
            if (File.Exists(CFr))
                File.Delete(CFr);
            PrintProducts("Gauti produktai", CFr, AllProducts);

            ToNewArray(MilkProducts, LeafyVegetableProducts, FruitVegetableProducts, AllProducts);

            MilkProducts.SortByFatness();
            LeafyVegetableProducts.SortByVitaminA();
            double Average = FruitPriceAverage(FruitVegetableProducts);

            PrintProducts("Rikiuoti pieno produktai", CFr, MilkProducts);
            PrintProducts("Rikiuotos lapinės daržovės", CFr, LeafyVegetableProducts);
            PrintProducts("Atrinktos vaisinės daržovės", CFr, FruitVegetableProducts);
            PrintAverage(CFr, Average, FruitVegetableProducts.n);
        }
        static void ReadProducts(string file, ProductsContainer Products)
        {               // nuskaito duomenis į vieną mišrų masyvą	
            using (StreamReader reader = new StreamReader(file))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    char type = char.Parse(parts[0]);
                    string name = parts[1].Trim();
                    double price = double.Parse(parts[2]);
                    double weight = double.Parse(parts[3]);

                    if (type == 'p')        // pieno produktai	
                    {
                        double fatness = double.Parse(parts[4]);
                        DateTime date = DateTime.Parse(parts[5]);

                        Product milk = new Milk(name, price, weight, fatness, date);
                        Products.AddProduct(milk);
                    }
                    else if (type == 'l')   // lapinės daržovės	
                    {
                        DateTime date = DateTime.Parse(parts[4]);
                        int vitaminA = int.Parse(parts[5]);

                        Product leafyVegetable = new LeafyVegetable(name, price, weight, date, vitaminA);
                        Products.AddProduct(leafyVegetable);
                    }
                    else if (type == 'v')   // vaisinės daržovės	
                    {
                        DateTime date = DateTime.Parse(parts[4]);
                        int sweetness = int.Parse(parts[5]);

                        Product fruitVegetable = new FruitVegetable(name, price, weight, date, sweetness);
                        Products.AddProduct(fruitVegetable);
                    }
                }
            }
        }
        static void PrintProducts(string tableTitle, string file, ProductsContainer Products)
        {               // spausdina į failą universalią lentelę	
            using (var fr = File.AppendText(file))
            {
                if (Products.n > 0)
                {
                    string header = new string('-', 110) + '\n' +
                        String.Format("{0,6} {1,12} {2,10} {3,10} {4,10} {5,15} {6,15} {7,10} {8,10}",
                        "Tipas", "Pavadinimas", "Kaina", "Svoris", "Riebumas", "Galioja iki", "Nuskinta", "Vitaminas", "Saldumo") + '\n' +
                        String.Format("{0,6} {1,12} {2,10} {3,10} {4,10} {5,15} {6,15} {7,10} {8,10}",
                        " ", " ", "(eur/kg)", "(kg)", "(%)", " ", " ", "A (gr)", "koef.") + '\n' +
                        new string('-', 110);
                    fr.WriteLine(tableTitle);
                    fr.WriteLine(header);

                    for (int i = 0; i < Products.n; i++)
                    {
                        fr.WriteLine(Products.ProductsArray[i].ToString());
                    }
                    fr.WriteLine(new string('-', 110));
                    fr.WriteLine('\n');
                }
                else
                {
                    fr.WriteLine(tableTitle);
                    fr.WriteLine("Tuščia");
                }
            }
        }
        static void ToNewArray(ProductsContainer NewMilk, ProductsContainer NewLeafyVegetable,
            ProductsContainer NewFruitVegetable, ProductsContainer Products)
        {               // iš vieno mišraus produktų masyvo sukuria 3 masyvus pagal produkto tipą 	
            for (int i = 0; i < Products.n; i++)
            {
                Product currentProduct = Products.ProductsArray[i];
                if (currentProduct is Milk)
                {
                    Milk currentMilk = (Milk)currentProduct;
                    if (currentMilk.Condition())
                        NewMilk.AddProduct(currentProduct as Milk);
                }
                else if (currentProduct is LeafyVegetable)
                {
                    if (currentProduct.weight < 1)
                        NewLeafyVegetable.AddProduct(currentProduct as LeafyVegetable);
                }
                else if (currentProduct is FruitVegetable)
                {
                    FruitVegetable currentFruit = (FruitVegetable)currentProduct;
                    if (currentFruit.Condition())
                        NewFruitVegetable.AddProduct(currentProduct as FruitVegetable);
                }
            }
        }
        static double FruitPriceAverage(ProductsContainer Products)
        {               // skaičiuoja tenkinančių sąlygas vaisinių daržovių kainų vidurkį	
            double amount = 0;
            double sum = 0;
            for (int i = 0; i < Products.n; i++)
            {
                amount++;
                Product currentProduct = Products.ProductsArray[i];
                sum += currentProduct.price;
            }
            if (amount > 0)
                return sum / amount;
            return 0;
        }
        static void PrintAverage(string file, double Average, int n)
        {               // į failą spausdina kainų vidurkio rezultatą	
            using (var fr = File.AppendText(file))
            {
                if (n > 0)
                    fr.WriteLine("Vaisinių daržovių, kurių saldumo koef.> 3 , kainų vidurkis yra {0:f2} eur", Average);
            }
        }
    }
}