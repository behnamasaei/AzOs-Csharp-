using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Shop
{
    

    class Program
    {
        List<ProductModel> productList = new List<ProductModel>();

        static void Main()
        {
            Program program = new Program();
            program.ReadDatabase();
            Console.Clear();
            Console.WriteLine($"Shop system:\n" +
                $"1. List product\n" +
                $"2. Add product\n" +
                $"3. Edit product\n" +
                $"4. Delete product\n" +
                $"5. Search\n" +
                $"6. Buy and Factor\n" +
                $"7. Exit");
            int select = Convert.ToInt32(Console.ReadLine());


            switch (select)
            {
                case 1:
                    program.ListProduct();
                    break;

                case 2:
                    program.AddProduct();
                    break;

                case 3:
                    program.EditProduct();
                    break;

                case 4:
                    program.DeleteProduct();
                    break;

                case 5:
                    program.Search();
                    break;

                case 6:
                    program.Buy();
                    break;

                case 7:
                    break;

                default:
                    Main();
                    break;
            }

        }

        private void Buy()
        {
            Console.Clear();
            List<ProductModel> factor = new List<ProductModel>();
            int select;
            do
            {
                Console.Clear();

                Console.WriteLine("1.buy\n2.menu");
                select = Convert.ToInt32(Console.ReadLine());
                Console.Clear();
                switch (select)
                {
                    case 1:
                        Console.Write("\nEnter ID product: ");
                        int id = Convert.ToInt32(Console.ReadLine());
                        Console.Write("\nEnter count product buy: ");
                        int count = Convert.ToInt32(Console.ReadLine());
                        var index = productList.FindIndex(i => i.ID == id);
                        if (index != -1)
                        {
                            factor.Add(new ProductModel
                            {
                                ID = productList[index].ID,
                                ProductName = productList[index].ProductName,
                                count = count,
                                price = productList[index].price,

                            });
                            var product = new ProductModel
                            {
                                ID = productList[index].ID,
                                ProductName = productList[index].ProductName,
                                count = productList[index].count - count,
                                price = productList[index].price,
                            };

                            productList.RemoveAt(index);
                           
                            productList.Add(product);
                            SaveDatabase();
                        }
                        
                        break;

                    case 2:
                        Console.WriteLine("Factor:");
                        Console.WriteLine($"List of product:\n" +
                $"Id\t\t Product name\t\t Count\t\t price");
                        foreach (var product in factor)
                        {
                            Console.WriteLine($"{product.ID}\t\t {product.ProductName}\t\t {product.count}\t\t {product.price}");
                        }
                        Console.ReadKey();

                        break;
                    default:
                        break;
                }
            } while (select == 1);
            Main();
        }

        private void Search()
        {
            Console.Clear();
            Console.Write("Edit:\nEnter name product: ");
            string name = Console.ReadLine();

            var index = productList.FindIndex(i => i.ProductName == name);
            if (index != -1)
            {
                var product = productList[index];
                Console.WriteLine("Result:");
                Console.WriteLine($"ID:{product.ID}\n" +
                    $"Product name:{product.ProductName}\n" +
                    $"Count:{product.count}\n" +
                    $"price:{product.price}\n");
            }
            else
            {
                Console.WriteLine("Not found!");
            }
            Console.ReadKey();
            Main();
        }

        private void DeleteProduct()
        {
            Console.Clear();
            Console.Write("Edit:\nEnter id product: ");
            int id = Convert.ToInt32(Console.ReadLine());
            var index = productList.FindIndex(i => i.ID == id);
            productList.RemoveAt(index);
            SaveDatabase();
            Main();
        }

        private void EditProduct()
        {
            Console.Clear();
            Console.Write("Edit:\nEnter id product: ");
            int id = Convert.ToInt32(Console.ReadLine());
            var index = productList.FindIndex(i => i.ID == id);
            var product = productList[index];
            Console.WriteLine($"ID:{product.ID}\n" +
                $"Product name:{product.ProductName}\n" +
                $"Count:{product.count}\n" +
                $"price:{product.price}\n");
            if (index != -1)
            {
                Console.Write("\nID: ");
                id = Convert.ToInt32(Console.ReadLine());

                Console.Write("\nProduct name: ");
                string name = Console.ReadLine();

                Console.Write("\nCount: ");
                int count = Convert.ToInt32(Console.ReadLine());

                Console.Write("\nPrice: ");
                double price = Convert.ToDouble(Console.ReadLine());

                productList[index] =new ProductModel
                {
                    ID = id,
                    ProductName = name,
                    count = count,
                    price = price
                };
                SaveDatabase();
                Main();
            }
            else
            {
                Console.WriteLine("Not found!");
            }
        }

        public void ReadDatabase()
        {
            string filePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Database.csv");
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var products = csv.GetRecords<ProductModel>();
                foreach (var item in products)
                {
                    productList.Add(new ProductModel
                    {
                        ID = item.ID,
                        ProductName = item.ProductName,
                        count = item.count,
                        price = item.price
                    });
                }

            }
        }

        public void SaveDatabase()
        {
            string filePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Database.csv");

            using (var writer = new StreamWriter(filePath))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(productList);
            }
        }

        public void ListProduct()
        {
            
            Console.Clear();
            Console.WriteLine($"List of product:\n" +
                $"Id\t\t Product name\t\t Count\t\t price");
            foreach (var product in productList)
            {
                Console.WriteLine($"{product.ID}\t\t {product.ProductName}\t\t {product.count}\t\t {product.price}");
            }
            Console.WriteLine("click to back menu...");
            Console.ReadKey();
            Program.Main();
        }

        public void AddProduct()
        {
            Console.Clear();
            Console.WriteLine("Add product:");
            Console.Write("\nID: ");
            int id = Convert.ToInt32(Console.ReadLine());

            Console.Write("\nProduct name: ");
            string name = Console.ReadLine();

            Console.Write("\nCount: ");
            int count = Convert.ToInt32(Console.ReadLine());

            Console.Write("\nPrice: ");
            double price = Convert.ToDouble(Console.ReadLine());

            productList.Add(new ProductModel
            {
                ID = id,
                ProductName = name,
                count = count,
                price = price
            });
            SaveDatabase();
            Main();
        }
    }
}
