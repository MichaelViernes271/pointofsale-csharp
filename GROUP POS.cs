using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;




/*****************************************

Group Project by:
	Jaison, Losito
	Mamauag, Kurt Cedrick C.
	Mallari, Roldan Y.
	Viernes, Michael E.
	
*****************************************/





namespace PointOfSaleSystem
{
    class Menu
    {
        public static string[] foodmenu = new string[]{
            "Steak", "Bacon", "Afritada", "Egg Pie", "Apple Pie",
            "Burger", "Fruit Salad", "Macaroni", "Adobo", "Distilled water"
        };
        public static int[] price = new int[]{
            30, 20, 30, 40, 50,
            10, 15, 25, 30, 100
        };
        public static void DisplayMenu()
        {
            int option = 0;
            Console.WriteLine("\t\tMenu List");
            Console.WriteLine(" Options \tPrice");
            for (int x = 0; x < 10; x++)
                Console.WriteLine(++option + ". " + foodmenu[x] + " \tPHP" + price[x]);
			Console.WriteLine("\nTO WITHDRAW ORDER, PLEASE ENTER ZERO (0).\n ");
        }
    }
    class ComputePOS
    {
        public static void Compute(List<string> customerOrder, List<uint> customerExpenditure, List<decimal> foodCostList, int counter, bool isPaying)
        {
            decimal foodCostListSum = 0m;
            Console.WriteLine(
                "\tRECEIPT\n" +
                "FOOD CHOICE \t AMOUNT \t NO. OF ORDER"
                );
            for (int x = 0; x < counter; x++)
            {
                Console.WriteLine(customerOrder[x] + " \t\t " + foodCostList[x] + " \t\t " + customerExpenditure[x]);
            }
            foreach (decimal foodcost in foodCostList)
                foodCostListSum += foodcost;
            Console.WriteLine(
                "-----------------------------" +
                "\n \t\tSUM: {0}\n", foodCostListSum
			);
			if(!isPaying){
				Console.WriteLine("PAYMENT COST: " + foodCostListSum);
				decimal payment = Convert.ToDecimal(Console.ReadLine());
				payment -= foodCostListSum;
				Console.WriteLine("\nCharges: " + payment + "\nTHANK YOU AND HAVE A NICE DAY!");
			}
        }
    }
	class POS
    {
        static void Main(string[] args)
        {
            bool quit = false;
            while (!quit)
            {
                List<string> customerOrder = new List<string>();
                List<uint> customerExpenditure = new List<uint>();
                List<decimal> foodCostList = new List<decimal>();
                int counter = 0;

                Console.WriteLine("WELCOME TO FAST FOOD CHAIN!");
                Menu.DisplayMenu();
                bool more = true;
                while (more)
                {
                    try
                    {
                        Console.Write("Choose options: ");
                        uint food = Convert.ToUInt32(Console.ReadLine());
						if(food == 0){
							try{
								Console.WriteLine(
									'\t' + customerOrder[counter - 1] + " REMOVED SUCCESSFULLY.\n" 
								);
								customerOrder.RemoveAt(counter - 1);
								customerExpenditure.RemoveAt(counter - 1);
								foodCostList.RemoveAt(counter - 1);
								counter--;
							}catch(Exception e){
								Console.WriteLine(e.Message);
							}
						}else{
							customerOrder.Add(Menu.foodmenu[--food]);
							Console.Write("Amount: ");
							uint amount = Convert.ToUInt32(Console.ReadLine());
							customerExpenditure.Add(amount);
							decimal foodCost = Menu.price[food] * amount;
							foodCostList.Add(foodCost);
							counter++;
						}
						//display customer details
						ComputePOS.Compute(customerOrder, customerExpenditure, foodCostList, counter, more);
                        //order more options
                        Console.Write("Add more options (y/n)? ");
                        char moreOption = Convert.ToChar(Console.ReadLine());
                        if ('y' == moreOption);
                        else if ('n' == moreOption){
							more = false;
                            break;
						}
                        else
                            throw new Exception("Wrong input. \'y\' and \'n\' only.");
                    } catch (FormatException fe)
                    {
                        Console.WriteLine("Wrong input. Please try again.");
                    }
                }

                //display customer details
                ComputePOS.Compute(customerOrder, customerExpenditure, foodCostList, counter, more);
                //quit or continue pos
                try
                {
                    Console.Write("Quit (y/n)? ");
                    char quitOption = Convert.ToChar(Console.ReadLine());
                    if ('n' == quitOption) ;
                    else if ('y' == quitOption)
                        break;
                    else
                        throw new Exception("Wrong input. \'y\' and \'n\' only.");
                } catch (Exception e)
                {
                    Console.WriteLine("Wrong input.");
                }
            }
        }
    }
}
