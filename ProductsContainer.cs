using System;
using System.Collections.Generic;
using System.Text;

namespace Paskaita12.uzduotis
{
    class ProductsContainer
    {
        const int Cmax = 1000;
        public int n { get; set; }
        public Product[] ProductsArray { get; set; }
        public ProductsContainer()
        {       // sukuria konteinerinį masyvą	
            n = 0;
            ProductsArray = new Product[Cmax];
        }
        public void AddProduct(Product res)
        {       // prideda prie masyvo 1 naują narį	
            ProductsArray[n++] = res;
        }
        public void SortByFatness()
        {       // rūšiuoja pagal "fatness" reikšmę didėjančia tvarka	
            for (int i = 0; i < n - 1; i++)
            {
                int minInd = i;
                for (int j = i + 1; j < n; j++)
                {
                    Milk CurrentProduct = (Milk)ProductsArray[j];
                    Milk MinProduct = (Milk)ProductsArray[minInd];

                    if (CurrentProduct < MinProduct)
                        minInd = j;
                }
                Product temp = ProductsArray[minInd];
                ProductsArray[minInd] = ProductsArray[i];
                ProductsArray[i] = temp;
            }
        }
        public void SortByVitaminA()
        {       // rūšiuoja pagal "vitaminA" reikšmę mažėjančia tvarka	
            for (int i = 0; i < n - 1; i++)
            {
                int maxInd = i;
                for (int j = i + 1; j < n; j++)
                {
                    LeafyVegetable CurrentProduct = (LeafyVegetable)ProductsArray[j];
                    LeafyVegetable MaxProduct = (LeafyVegetable)ProductsArray[maxInd];

                    if (CurrentProduct.VitaminAMore(MaxProduct))
                        maxInd = j;
                }
                Product temp = ProductsArray[maxInd];
                ProductsArray[maxInd] = ProductsArray[i];
                ProductsArray[i] = temp;
            }
        }
    }
}
