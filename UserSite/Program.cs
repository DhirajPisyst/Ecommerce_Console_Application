using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserSite
{
    public class UserPanel
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

        string CredentialPath = @"F:/UserList/Credential/UserCredential.txt";
        Dictionary<string, List<string>> CredentialData = new Dictionary<string, List<string>>();



        public void creatingbill(string UserPath, int ShopNo, string phone)
        {
            var ShopById = ShopNameList[ShopNo - 1];
            int subPrice;
            string timeStamp = DateTime.Now.ToString("yyyyMMddHHmmss");
            string ShopIdPath = $@"F:/ShopName/shop{ShopById}.txt";
            string billPath = $@"F:/UserList/user{phone}/bill_{timeStamp}_{phone}.txt";

            int total = 0;
            Dictionary<string, List<int>> map2 = new Dictionary<string, List<int>>();
            Dictionary<string, string> CartData = new Dictionary<string, string>();

            using (FileStream cartFile = new FileStream(UserPath, FileMode.Open, FileAccess.ReadWrite))
            {
                using (StreamReader read = new StreamReader(cartFile))
                {
                    string line = "";
                    while ((line = read.ReadLine()) != null)
                    {
                        if (line.Length > 0)
                        {
                            List<string> list = new List<string>();
                            string addedItemName = line.Substring(0, line.IndexOf(','));
                            string addedQuantity = line.Substring(line.LastIndexOf(",") + 1);


                            if (!CredentialData.ContainsKey(addedItemName))
                            {

                                list.Add(addedItemName);
                                list.Add(addedQuantity);
                                CartData.Add(list[0], list[1]);
                            }


                        }
                        else
                        {
                            Console.WriteLine("Blank Space in File");
                        }


                    }
                }

                using (FileStream shopFile = new FileStream(ShopIdPath, FileMode.Open, FileAccess.Read))
                {
                    using (StreamReader sr = new StreamReader(shopFile))
                    {

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


                            map2.Add(shopItemName, shopItemQuantityPrice);



                        }
                    }


                }
                foreach (string element in CartData.Keys)
                {
                    int perPrice = map2[element][1];
                    int subQuantity = Convert.ToInt32(CartData[element]);

                    subPrice = perPrice * subQuantity;
                    total = total + subPrice;

                    string line = $"ItemName = {element} , Quantity = {subQuantity} , SubPrice = {subPrice}";
                    Console.WriteLine(line);

                }

                //Console.WriteLine("Press 1 To Order");
                //var orderInput = Console.ReadLine();

                //if (orderInput == "1")
                //{
                //    using (FileStream billFile = new FileStream(billPath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                //    {
                //        using (StreamWriter sw1 = new StreamWriter(billFile))
                //        {
                //            foreach (string element in CartData.Keys)
                //            {
                //                int perPrice = map2[element][1];
                //                int subQuantity = Convert.ToInt32(CartData[element]);

                //                subPrice = perPrice * subQuantity;
                //                total = total + subPrice;

                //                string line = $"ItemName = {element} , Quantity = {subQuantity} , SubPrice = {subPrice}";

                //                sw1.Write(line);
                //                sw1.Write("Total Bill = " + total);


                //            }
                //        }
                //    }
                //}

                using (FileStream billFile = new FileStream(billPath, FileMode.Create, FileAccess.ReadWrite))
                {
                    using (StreamWriter sw1 = new StreamWriter(billFile))
                    {
                        foreach (string element in CartData.Keys)
                        {
                            int perPrice = map2[element][1];
                            int subQuantity = Convert.ToInt32(CartData[element]);

                            subPrice = perPrice * subQuantity;
                            total = total + subPrice;

                            string line = $"ItemName = {element} , Quantity = {subQuantity} , SubPrice = {subPrice}";

                            sw1.Write(line);
                            sw1.Write("Total Bill = " + total);


                        }
                    }
                }







                Console.WriteLine("Total Bill = " + total);
                //Console.WriteLine("Billing Time =" + timeStamp);

            }




        }

        //Converting File Data Into Dictionary ---------------------------------------------
        public void ConvertingCredentialFile()
        {
            //Converting File Data Into Dictionary ---------------------------------------------
            using (FileStream crdentialfile = new FileStream(CredentialPath, FileMode.Open, FileAccess.ReadWrite))
            {
                using (StreamReader read = new StreamReader(crdentialfile))
                {
                    string line = "";
                    while ((line = read.ReadLine()) != null)
                    {
                        if (line.Length > 0)
                        {
                            List<string> list = new List<string>();
                            string contactno = line.Substring(0, line.IndexOf(','));
                            int len = line.LastIndexOf(",") - line.IndexOf(',') - 1;
                            string name = line.Substring((line.IndexOf(',') + 1), len);
                            string password = line.Substring(line.LastIndexOf(",") + 1);


                            if (!CredentialData.ContainsKey(contactno))
                            {

                                list.Add(name);
                                list.Add(password);
                                CredentialData.Add(contactno, list);
                            }


                        }
                        else
                        {
                            Console.WriteLine("Blank Space in File");
                        }


                    }
                }
            }



        }
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
                                sw2.WriteLine(useLine);

                                Console.WriteLine("Successfully Added Into The Cart !!");
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


        // Login And SignUp ------------------------------------------------
        public Boolean login(string UserPhoneNo, string UserPassword)
        {
            ConvertingCredentialFile();

            foreach (string element in CredentialData.Keys)
            {
                if (UserPassword == CredentialData[element][1] && UserPhoneNo == element)
                {
                    Console.WriteLine("Login Successfully !!");
                    return true;
                }
            }

            Console.WriteLine("Invalid Password or Mobile Number !!");
            return false;

        }

        public void signUp(string UserName, String UserPhoneNo, string UserPassword)
        {


            string NewUserFolderPath = $@"F:/UserList/user{UserPhoneNo}";



            //Converting File Data Into Dictionary ---------------------------------------------
            ConvertingCredentialFile();

            foreach (string element in CredentialData.Keys)
            {
                if (element == UserPhoneNo)
                {
                    Console.WriteLine("Already Mobile Number Exist");
                    return;

                }

            }
            Directory.CreateDirectory(NewUserFolderPath);

            //Adding Details in Credential File
            using (FileStream CredFile = new FileStream(CredentialPath, FileMode.Open, FileAccess.Write))
            {
                using (StreamWriter sw = new StreamWriter(CredFile))
                {
                    string addData = $"{UserPhoneNo},{UserName},{UserPassword}";
                    sw.WriteLine(addData);
                    Console.WriteLine("Successfully Sign Up !!");
                }
            }

            //Creating Add Cart File 
            string UserAddCartPath = $@"{NewUserFolderPath}/Cart_{UserPhoneNo}.txt";
            using (FileStream cartfile = new FileStream(UserAddCartPath, FileMode.CreateNew)) { }

        }

    }
    internal class Program
    {
        static void Main(string[] args)
        {
            UserPanel UserObj = new UserPanel();

            string inputChoice;
            string role;

            int ShopNo;


            do
            {
                Console.WriteLine("---------User---------");
                Console.WriteLine("Press 1 For Login");
                Console.WriteLine("Press 2 For Sign Up");
                Console.WriteLine("Press 3 For Exit");
                role = Console.ReadLine();
                switch (role)
                {
                    case "1":
                        Console.Write("Enter User Mobile Number = ");
                        var phone = Console.ReadLine();

                        Console.Write("Enter Your Password = ");
                        var password = Console.ReadLine();

                        Boolean loggedIn = UserObj.login(phone, password);

                        if (loggedIn)
                        {
                            do
                            {

                                var UserPath = $@"F:/UserList/user{phone}/cart_{phone}.txt";
                                Console.WriteLine("----Shop List----");
                                UserObj.shopList();

                                Console.WriteLine("Select Shop ");
                                ShopNo = int.Parse(Console.ReadLine());


                                Console.WriteLine("----User----");
                                Console.WriteLine("Enter the Choice");
                                Console.WriteLine("Press 1 For Add To Cart");
                                Console.WriteLine("Press 2 To View");
                                Console.WriteLine("Press 3 To Update");
                                Console.WriteLine("Press 4 To Remove");
                                Console.WriteLine("Press 5 To Exit");
                                Console.WriteLine("Press 6 To Display Shop Item");
                                Console.WriteLine("Press 7 To Buy Item For Cart");
                                inputChoice = Console.ReadLine();
                                switch (inputChoice)
                                {
                                    case "1":
                                        Console.WriteLine("Add");
                                        UserObj.add_user_item(UserPath, ShopNo);
                                        break;

                                    case "2":
                                        Console.WriteLine("View");
                                        UserObj.view_user_item(UserPath);
                                        break;

                                    case "3":
                                        Console.WriteLine("Update");
                                        break;

                                    case "4":
                                        Console.WriteLine("Remove");
                                        break;

                                    case "5":
                                        Console.WriteLine("Thanks For Using This Appplication");
                                        break;

                                    case "6":
                                        //Console.WriteLine("Display Shop Item");
                                        UserObj.display_shop_item_by_no(ShopNo);
                                        break;

                                    case "7":
                                        UserObj.creatingbill(UserPath, ShopNo, phone);
                                        break;

                                    default:
                                        Console.WriteLine("Invalid Input");
                                        break;
                                }

                            } while (inputChoice != "5");
                        }



                        break;

                    case "2":
                        string UserName;
                        string UserPhoneNo;
                        string UserPassword;
                        Console.Write("Enter Your Name = ");
                        UserName = Console.ReadLine();

                        Console.Write("Enter Your Phone Numer = ");
                        UserPhoneNo = Console.ReadLine();

                        if (UserPhoneNo.Length == 10)
                        {
                            Console.Write("Enter Your Password = ");
                            UserPassword = Console.ReadLine();
                            UserObj.signUp(UserName, UserPhoneNo, UserPassword);

                        }
                        else
                        {
                            Console.WriteLine("Invalid Mobile Number Length ");

                        }




                        break;
                }

            } while (role != "3");



        }
    }
}
