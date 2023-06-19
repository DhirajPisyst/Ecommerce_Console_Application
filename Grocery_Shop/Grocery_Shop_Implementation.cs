using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Linq;

namespace Grocery_Shop
{


    internal class ShopKeeper
    {
        string ShopPath = @"F:/shopitem.txt";
        string ShopList = $@"F:/ShopName/ShopList/shoplist.txt";
        public List<string> ShopNameList = new List<string>();


        public void check(string PhoneNo)
        {
            string ShopPath = $@"F:/ShopName/shop{PhoneNo}.txt";

            //string ShopList = $@"F:/ShopName/ShopList/shoplist.txt";
            List<string> contact_no = new List<string>();



            using (FileStream file = new FileStream(ShopPath, FileMode.OpenOrCreate, FileAccess.Read))
            {

            }
            using (FileStream file = new FileStream(ShopList, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader sr = new StreamReader(file))
                {

                    string line = "";
                    while ((line = sr.ReadLine()) != null)
                    {
                        contact_no = line.Split(',').ToList();
                    }



                }
            }
            string item = contact_no.Find(number => number == PhoneNo);

            if (item == null)
            {
                using (FileStream file = new FileStream(ShopList, FileMode.Append, FileAccess.Write))
                {
                    using (StreamWriter sw = new StreamWriter(file))
                    {
                        sw.Write(PhoneNo + ",");
                    }
                }

            }


        }


        public void shopList()
        {

            using (FileStream file = new FileStream(ShopList, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader sr = new StreamReader(file))
                {
                    string line = "";

                    while ((line = sr.ReadLine()) != null)
                    {

                        ShopNameList = line.Split(',').ToList();


                    }
                }
            }
            for (int i = 0; i < ShopNameList.Count - 1; i++)
            {
                Console.WriteLine($"{i + 1}. ShopNo = {ShopNameList[i]}");
            }


        }
        public void add_shop_item(string ShopPath)
        {


            using (FileStream file = new FileStream(ShopPath, FileMode.Append, FileAccess.Write))
            {


                using (StreamWriter writer = new StreamWriter(file))
                {
                    Console.WriteLine("Enter The Item Name");
                    var userItemName = Console.ReadLine();

                    Console.WriteLine("Enter The Quantity");
                    var quantity = int.Parse(Console.ReadLine());

                    Console.WriteLine("Enter the Per Price");
                    var price = int.Parse(Console.ReadLine());

                    writer.WriteLine(userItemName + "," + quantity + "," + price);
                }
            }


        }
        public void display_shop_item_by_no(int ShopId)
        {
            var ShopById = ShopNameList[ShopId - 1];

            string ShopIdPath = $@"F:/ShopName/shop{ShopById}.txt";
            if (File.Exists(ShopIdPath))
            {
                List<string> shopItemList1 = new List<string>();


                using (FileStream file = new FileStream(ShopIdPath, FileMode.Open, FileAccess.Read))
                {
                    using (StreamReader sr = new StreamReader(file))
                    {
                        string line = "";
                        Console.WriteLine("userItemName");
                        while ((line = sr.ReadLine()) != null)
                        {

                            shopItemList1.Add(line.Substring(0, line.IndexOf(',')));

                        }
                        foreach (string shopItem in shopItemList1)
                        {
                            Console.WriteLine(shopItem);
                        }



                    }
                }
            }
            else
            {
                Console.WriteLine("File does not exist: " + ShopIdPath);

            }


        }

        public void view_shop_item(string ShopPath)
        {


            using (FileStream file = new FileStream(ShopPath, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader sr = new StreamReader(file))
                {
                    string line = "";

                    while ((line = sr.ReadLine()) != null)
                    {

                        Console.WriteLine(line);

                    }
                }
            }

        }
        public void update_shop_item()
        {
            Dictionary<string, int> itemList = new Dictionary<string, int>();

            object[] itemDetails;
            using (FileStream file = new FileStream(ShopPath, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader read = new StreamReader(file))
                {
                    string line = "";

                    while ((line = read.ReadLine()) != null)
                    {

                        itemDetails = line.Split(',');
                        foreach (var element in itemDetails)
                        {
                            Console.Write(element + " ");
                        }
                        Console.WriteLine();


                    }






                }
            }

        }



        // User Method ---------------

        public void add_user_item(string UserPath, int ShopNo)
        {
            var ShopById = ShopNameList[ShopNo - 1];

            string ShopIdPath = $@"F:/ShopName/shop{ShopById}.txt";
            List<string> shopItemList1 = new List<string>();
            Dictionary<string, List<int>> map1 = new Dictionary<string, List<int>>();
            Console.WriteLine("Enter the Item Name");
            string userItemName = Console.ReadLine();









            using (FileStream shopfile = new FileStream(ShopIdPath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                using (StreamReader sr = new StreamReader(shopfile))
                {

                    //Dictionary<string, List<int>> map1 = new Dictionary<string, List<int>>();
                    string line = "";
                    while ((line = sr.ReadLine()) != null)
                    {
                        List<int> shopItemQuantityPrice = new List<int>();
                        int len = line.LastIndexOf(',') - (line.IndexOf(',') + 1);
                        int shopItemQuantity = Convert.ToInt32(line.Substring((line.IndexOf(',')) + 1, len));
                        int shopItemPrice = Convert.ToInt32(line.Substring((line.LastIndexOf(',')) + 1));
                        string shopItemName = line.Substring(0, line.IndexOf(","));
                        //string itemquantityprice = line.Substring((line.IndexOf(',') + 1));

                        shopItemQuantityPrice.Add(shopItemQuantity);
                        shopItemQuantityPrice.Add(shopItemPrice);


                        map1.Add(shopItemName, shopItemQuantityPrice);



                    }
                }

                bool flag = false;
                foreach (string element in map1.Keys)
                {
                    //Console.WriteLine($"userItemName= {element} ,Item_quantity_price= {map[element]}");
                    if (userItemName == element)
                    {
                        flag = true;

                    }

                }
                if (flag)
                {
                    Console.WriteLine("Enter the Item Quantity");
                    int userQuantity = int.Parse(Console.ReadLine());
                    int itemQuantityInShop = map1[userItemName][0];
                    if (itemQuantityInShop >= userQuantity)
                    {
                        map1[userItemName][0] = map1[userItemName][0] - userQuantity;


                        using (StreamWriter sw1 = new StreamWriter(ShopIdPath))
                        {
                            foreach (string element in map1.Keys)
                            {
                                string newLine = $"{element},{map1[element][0]},{map1[element][1]}";
                                sw1.WriteLine(newLine);
                            }
                        }

                        using (FileStream userFile = new FileStream(UserPath, FileMode.Append, FileAccess.Write))
                        {
                            using (StreamWriter sw2 = new StreamWriter(userFile))
                            {
                                string useLine = $"{userItemName},{userQuantity}";
                                sw2.Write(useLine);
                            }
                        }


                    }
                    else
                    {
                        Console.WriteLine($"Sorry !! Availabe Quantity= {itemQuantityInShop} ");
                    }

                }
                else
                {
                    Console.WriteLine("Sorry!!! Item Not Availabe In This Shop");
                }

            }
            //writer.WriteLine($"{itemName},{quantity}");





        }


        public void view_user_item(string UserPath)
        {



            using (FileStream file = new FileStream(UserPath, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader read = new StreamReader(file))
                {
                    string line = "";
                    while ((line = read.ReadLine()) != null)
                    {
                        Console.WriteLine(line);
                    }
                }
            }

        }


    }






    internal class Grocery_Shop_Implementation
    {
        static void Main(string[] args)
        {
            ShopKeeper ShopObj = new ShopKeeper();

            string role;
            string inputChoice;
            string ShopPath = "";
            string Phone = "";


            Console.WriteLine("Enter Shopkeeper Mobile Number");
            Phone = Console.ReadLine();

            ShopPath = $@"F:/ShopName/shop{Phone}.txt";

            ShopObj.check(Phone);

            do
            {
                Console.WriteLine("----ShopKeeper----");
                Console.WriteLine("Enter the Choice");
                Console.WriteLine("Press 1 To Add");
                Console.WriteLine("Press 2 To View");
                Console.WriteLine("Press 3 To Update");
                Console.WriteLine("Press 4 To Remove");
                Console.WriteLine("Press 5 To Exit");
                inputChoice = Console.ReadLine();
                switch (inputChoice)
                {
                    case "1":
                        Console.WriteLine("Add_Admin");
                        ShopObj.add_shop_item(ShopPath);
                        break;

                    case "2":
                        Console.WriteLine("View_Admin");
                        ShopObj.view_shop_item(ShopPath);
                        break;

                    case "3":
                        Console.WriteLine("Update_Admin");
                        ShopObj.update_shop_item();
                        break;

                    case "4":
                        Console.WriteLine("Remove_Admin");
                        break;

                    case "5":
                        Console.WriteLine("Thanks For Using This Appplication");
                        break;

                    default:
                        Console.WriteLine("Invalid Input");
                        break;
                }

            } while (inputChoice != "5");

            //do
            //{

            //    Console.WriteLine("Choose The Role");
            //    Console.WriteLine("Press 1 For User");
            //    Console.WriteLine("Press 2 For ShopKeeper");
            //    Console.WriteLine("Press 3 For Exit");
            //    role = Console.ReadLine();

            //    switch (role)
            //    {
            //        case "1":
            //            int ShopNo;
            //            Console.WriteLine("Enter User Mobile Number");
            //            var phone = Console.ReadLine();
            //            var UserPath = $@"F:/UserList/user{phone}.txt";

            //            Console.WriteLine("----Shop List----");
            //            ShopObj.shopList();

            //            Console.WriteLine("Select Shop ");
            //            ShopNo = int.Parse(Console.ReadLine());

            //            do
            //            {
            //                Console.WriteLine("----User----");
            //                Console.WriteLine("Enter the Choice");
            //                Console.WriteLine("Press 1 To Add");
            //                Console.WriteLine("Press 2 To View");
            //                Console.WriteLine("Press 3 To Update");
            //                Console.WriteLine("Press 4 To Remove");
            //                Console.WriteLine("Press 5 To Exit");
            //                Console.WriteLine("Press 6 To Display Shop Item");
            //                inputChoice = Console.ReadLine();
            //                switch (inputChoice)
            //                {
            //                    case "1":
            //                        Console.WriteLine("Add");
            //                        ShopObj.add_user_item(UserPath, ShopNo);
            //                        break;

            //                    case "2":
            //                        Console.WriteLine("View");
            //                        ShopObj.view_user_item(UserPath);
            //                        break;

            //                    case "3":
            //                        Console.WriteLine("Update");
            //                        break;

            //                    case "4":
            //                        Console.WriteLine("Remove");
            //                        break;

            //                    case "5":
            //                        Console.WriteLine("Thanks For Using This Appplication");
            //                        break;

            //                    case "6":
            //                        //Console.WriteLine("Display Shop Item");
            //                        ShopObj.display_shop_item_by_no(ShopNo);
            //                        break;

            //                    default:
            //                        Console.WriteLine("Invalid Input");
            //                        break;
            //                }

            //            } while (inputChoice != "5");

            //            break;




            //        case "2":

            //            string ShopPath = "";
            //            string Phone = "";


            //            Console.WriteLine("Enter Shopkeeper Mobile Number");
            //            Phone = Console.ReadLine();

            //            ShopPath = $@"F:/ShopName/shop{Phone}.txt";

            //            ShopObj.check(Phone);

            //            do
            //            {
            //                Console.WriteLine("----ShopKeeper----");
            //                Console.WriteLine("Enter the Choice");
            //                Console.WriteLine("Press 1 To Add");
            //                Console.WriteLine("Press 2 To View");
            //                Console.WriteLine("Press 3 To Update");
            //                Console.WriteLine("Press 4 To Remove");
            //                Console.WriteLine("Press 5 To Exit");
            //                inputChoice = Console.ReadLine();
            //                switch (inputChoice)
            //                {
            //                    case "1":
            //                        Console.WriteLine("Add_Admin");
            //                        ShopObj.add_shop_item(ShopPath);
            //                        break;

            //                    case "2":
            //                        Console.WriteLine("View_Admin");
            //                        ShopObj.view_shop_item(ShopPath);
            //                        break;

            //                    case "3":
            //                        Console.WriteLine("Update_Admin");
            //                        ShopObj.update_shop_item();
            //                        break;

            //                    case "4":
            //                        Console.WriteLine("Remove_Admin");
            //                        break;

            //                    case "5":
            //                        Console.WriteLine("Thanks For Using This Appplication");
            //                        break;

            //                    default:
            //                        Console.WriteLine("Invalid Input");
            //                        break;
            //                }

            //            } while (inputChoice != "5");

            //            break;

            //        case "3":
            //            Console.WriteLine("Thanks for Using This Application");
            //            break;

            //        default:
            //            Console.WriteLine("Invalid Input");
            //            break;

            //    }




            //} while (role != "3");

        }
    }
}