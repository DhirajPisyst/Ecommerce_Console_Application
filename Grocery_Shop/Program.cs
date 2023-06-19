using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryShop
{
    internal class Shop
    {
        static void Main(string[] arg)
        {

            string[] itemList1 = new string[100];
            double[] itemPrice1 = new double[100];
            int[] itemQuantity1 = new int[100];
            int currentIndex = 0;
            string item;
            double price;
            int quantity;
            string inputChoice;
            double totalPricePerItem;



            string[] availabeItemName = new string[100];
            int[] availabeItemQuantity = new int[100];
            int currentStockIndex = 0;





            string role;

            //Console.WriteLine("Choose The Role");
            //Console.WriteLine("Press 1 For User");
            //Console.WriteLine("Press 2 For Admin");
            //Console.WriteLine("Press 3 For Exit");
            //role = Console.ReadLine();

            //Console.WriteLine(role);
            do
            {
                Console.WriteLine("Choose The Role");
                Console.WriteLine("Press 1 For User");
                Console.WriteLine("Press 2 For Admin");
                Console.WriteLine("Press 3 For Exit");
                role = Console.ReadLine();

                Console.WriteLine(role);

                if (role == "1")
                {
                    do
                    {
                        Console.WriteLine("-----------User-----------");
                        Console.WriteLine("Enter the Choice");
                        Console.WriteLine("Press 1 To Add");
                        Console.WriteLine("Press 2 To Update");
                        Console.WriteLine("Press 3 To View");
                        Console.WriteLine("Press 4 To Remove");
                        Console.WriteLine("Press 5 To Exit");
                        inputChoice = Console.ReadLine();

                        if (inputChoice == "5")
                        {
                            Console.WriteLine("Thank You For Using This Application");
                            return;
                        }

                        //Add Item ---------------------------------
                        else if (inputChoice == "1")
                        {
                            Console.WriteLine("-- Add Item --");
                            Console.WriteLine("Enter the Item Name");
                            item = Console.ReadLine();

                            itemList1[currentIndex] = item;

                            Console.WriteLine("Enter the Quantity");
                            quantity = Convert.ToInt16(Console.ReadLine());
                            itemQuantity1[currentIndex] = quantity;


                            Console.WriteLine("Enter the Item Per Price");
                            price = Convert.ToDouble(Console.ReadLine());



                            itemPrice1[currentIndex] = price;
                            currentIndex++;

                            Console.WriteLine("Added Successfully!");
                            Console.WriteLine();

                        }
                        //Update Item --------------------------------
                        else if (inputChoice == "2")
                        {
                            int updateIndex;
                            Console.WriteLine("-- Update Item --");
                            for (int i = 0; i < currentIndex; i++)
                            {

                                Console.Write((i + 1) + ". " + itemList1[i] + "-> ");
                                Console.Write("Quantity = " + itemQuantity1[i] + ", ");
                                Console.Write("Item Per Price = " + itemPrice1[i] + " ");
                                Console.WriteLine();
                            }

                            Console.WriteLine("Enter the item name to update");

                            string updateItemName = Console.ReadLine();




                            for (int i = 0; i < currentIndex; i++)
                            {
                                if (itemList1[i] == updateItemName)
                                {
                                    updateIndex = i;
                                    //itemList1[updateIndex] = updateItemName;

                                    Console.WriteLine("Enter The Updated Item Quantity");
                                    quantity = Convert.ToInt16(Console.ReadLine());
                                    itemQuantity1[updateIndex] = quantity;

                                    Console.WriteLine("Enter the Updated Item Price");
                                    price = Convert.ToDouble(Console.ReadLine());
                                    itemPrice1[updateIndex] = price;

                                    Console.WriteLine("Updated Successfully");
                                    break;
                                }
                                else if (i == currentIndex - 1)
                                {
                                    Console.WriteLine("Item Not Found!!");
                                }

                            }
                            Console.WriteLine();


                        }
                        // View Item List ------------------------------
                        else if (inputChoice == "3")
                        {
                            double totalPrice = 0;

                            Console.WriteLine("-- List Of Item --");
                            for (int i = 0; i < currentIndex; i++)
                            {
                                Console.Write((i + 1) + ". " + itemList1[i] + "-> ");
                                Console.Write("Quantity = " + itemQuantity1[i] + ", ");
                                Console.Write("Item Price = " + itemPrice1[i] + " ");
                                totalPrice = totalPrice + (itemPrice1[i] * Convert.ToDouble(itemQuantity1[i]));

                                Console.WriteLine();
                            }
                            Console.WriteLine();
                            Console.WriteLine("Total Price = " + totalPrice);
                            Console.WriteLine();
                        }

                        //Remove Item ---------------------------------
                        else if (inputChoice == "4")
                        {
                            int removeIndex;
                            Console.WriteLine("-- Remove --");
                            for (int i = 0; i < currentIndex; i++)
                            {
                                Console.Write(itemList1[i] + " = ");
                                Console.Write(itemPrice1[i]);
                                Console.WriteLine();
                            }
                            Console.WriteLine("Enter the item name to Remove");
                            item = Console.ReadLine();

                            for (int i = 0; i < currentIndex; i++)
                            {
                                if (itemList1[i] == item)
                                {
                                    removeIndex = i;
                                    for (; removeIndex < currentIndex; removeIndex++)
                                    {
                                        if (removeIndex == currentIndex - 1)
                                        {
                                            currentIndex--;
                                        }
                                        else
                                        {
                                            itemList1[removeIndex] = itemList1[removeIndex + 1];
                                            itemPrice1[removeIndex] = itemPrice1[removeIndex + 1];
                                            itemQuantity1[removeIndex] = itemQuantity1[removeIndex + 1];
                                            currentIndex--;

                                        }

                                    }
                                    Console.WriteLine("Removed Successfully!");

                                    break;

                                }
                            }
                            Console.WriteLine();
                        }
                        else
                        {
                            Console.WriteLine("Invalid Input !!");
                            Console.WriteLine();
                        }
                    }
                    while (inputChoice != "5");
                }
                else if (role == "2")
                {

                    do
                    {
                        Console.WriteLine("-----------Admin-----------");
                        Console.WriteLine("Enter the Choice");
                        Console.WriteLine("Press 1 To Add");
                        Console.WriteLine("Press 2 To View");
                        Console.WriteLine("Press 3 To Remove");
                        Console.WriteLine("Press 4 To Exit");
                        inputChoice = Console.ReadLine();

                        if (inputChoice == "4")
                        {
                            Console.WriteLine("Thank You For Using This Application");
                            break;
                        }
                        else if (inputChoice == "1")
                        {
                            Console.WriteLine("-- Add Item --");

                            Console.WriteLine("Enter the Item Name");
                            item = Console.ReadLine();

                            if (currentStockIndex == 0)
                            {
                                availabeItemName[currentStockIndex] = item;
                                Console.WriteLine("Enter the Quantity");
                                quantity = Convert.ToInt16(Console.ReadLine());
                                availabeItemQuantity[currentStockIndex] = quantity;

                                Console.WriteLine("Enter Per Price");
                                price = Convert.ToDouble(Console.ReadLine());
                                itemPrice1[currentStockIndex] = price;

                                currentStockIndex++;
                            }
                            else
                            {
                                Boolean flag = false;
                                for (int i = 0; i < currentStockIndex; i++)
                                {
                                    if (item == availabeItemName[i])
                                    {
                                        flag = true;
                                        string choose;
                                        Console.WriteLine("Already Item available... Do You Want To Add In Stock ?");
                                        Console.WriteLine("For yes press 1 and For no press 2");
                                        choose = Console.ReadLine();

                                        if (choose == "1")
                                        {

                                            Console.WriteLine("Enter the Quantity");
                                            quantity = Convert.ToInt16(Console.ReadLine());
                                            availabeItemQuantity[i] = availabeItemQuantity[i] + quantity;

                                            Console.WriteLine("Added Successfully!");
                                            break;

                                        }
                                        else if (choose == "2")
                                        {
                                            break;
                                        }
                                        else
                                        {
                                            Console.WriteLine("Invalid Input !!");
                                            break;
                                        }

                                    }

                                }
                                if (!flag)
                                {
                                    availabeItemName[currentStockIndex] = item;
                                    Console.WriteLine("Enter the Quantity");
                                    quantity = Convert.ToInt16(Console.ReadLine());
                                    availabeItemQuantity[currentStockIndex] = quantity;

                                    Console.WriteLine("Enter Per Price");
                                    price = Convert.ToDouble(Console.ReadLine());
                                    itemPrice1[currentStockIndex] = price;

                                    currentStockIndex++;

                                    Console.WriteLine("Added Successfully!");

                                }

                            }





                            Console.WriteLine();

                        }
                        else if (inputChoice == "2")
                        {
                            for (int i = 0; i < currentStockIndex; i++)
                            {
                                Console.WriteLine($"{i + 1}. {availabeItemName[i]} -> InStock_Qunatity = {availabeItemQuantity[i]}, Per_Price = {itemPrice1[i]}");

                            }
                            Console.WriteLine();
                        }
                        else if (inputChoice == "3")
                        {
                            string itemNameToRemove;

                            for (int i = 0; i < currentStockIndex; i++)
                            {
                                Console.WriteLine($"{i + 1}. {availabeItemName[i]} -> InStock_Qunatity = {availabeItemQuantity[i]}, Per_Price = {itemPrice1[i]}");

                            }


                            Console.WriteLine("Enter the Item name From Above List");
                            itemNameToRemove = Console.ReadLine();

                            for (int i = 0; i < currentStockIndex; i++)
                            {
                                if (availabeItemName[i] == itemNameToRemove)
                                {

                                    for (int removeIndex = i; removeIndex < currentStockIndex; removeIndex++)
                                    {
                                        if (removeIndex == currentStockIndex - 1)
                                        {
                                            currentStockIndex--;
                                        }
                                        else
                                        {
                                            availabeItemName[removeIndex] = availabeItemName[removeIndex + 1];
                                            itemPrice1[removeIndex] = itemPrice1[removeIndex + 1];
                                            availabeItemQuantity[removeIndex] = availabeItemQuantity[removeIndex + 1];
                                            currentStockIndex--;
                                        }

                                    }
                                    Console.WriteLine("Removed Successfully!");

                                    break;

                                }
                            }


                        }

                    } while (inputChoice != "4");


                }

                else if (role == "3")
                {
                    Console.WriteLine("Thanks For Using This Application !!");
                    return;
                }
                else
                {
                    Console.WriteLine("Invalid Input !!");
                }
            } while (role != "3");




        }
    }
}
