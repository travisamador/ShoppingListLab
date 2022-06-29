Console.WriteLine("Welcome to the convenience store!");
//make dictionary w/ items & prices
Dictionary<string, double> stock = new Dictionary<string, double>();
stock.Add("Coca-Cola", 1.99);
stock.Add("Kitkat", .50);
stock.Add("Doritos", 1.25);
stock.Add("Lemonade", .99);
stock.Add("Red Bull", 4.99);
stock.Add("Sour Patch Kids", 2.25);
stock.Add("Skittles", .75);
stock.Add("Mountain Dew",  1.75);
//make list for customer items
List<string> cart = new List<string>();
double cartprice = 0;
bool orderLoop = true;
while (orderLoop)
{
    Console.WriteLine();
    Console.WriteLine(string.Format("{0,-4}{1,-17}{2,-1}", "#", "Item", "Price\n=============================="));
    int menuCount = 1;
    foreach (KeyValuePair<string, double> kvp in stock)
    {
        Console.WriteLine(string.Format("{0,-4}{1,-17}{2,-1}", $"{menuCount}", $"{kvp.Key}", $"${kvp.Value:N2}"));
        menuCount++;
    }
    //ask user for item name and add it to cart
    Console.WriteLine();
    Console.WriteLine("What item would you like to buy?\nYou may order by item name or number.");
    string item = Console.ReadLine();
    if (int.TryParse(item, out int menuNum) && (menuNum > 0 && menuNum < stock.Count + 1))
    {
        item = stock.ElementAt(menuNum - 1).Key;
    }
    if (stock.TryGetValue(item, out double price))
    {
        Console.WriteLine($"Adding {item} to cart at ${price:N2}.");
        cart.Add(item);
        //vvv Works, but build specifications call for loop
        //cartprice += price;
        //ask user if they want another item
        while (true)
        {
            Console.WriteLine("Would you like to buy anything else? (y/n)");
            string answer = Console.ReadLine().ToLower().Trim();
            if (answer == "y")
            {
                break;
            }
            else if (answer == "n")
            {
                orderLoop = false;
                //sort cart by price
                sortCart(ref cart, stock);
                //display columns of customer's items and prices 
                Console.WriteLine("Thanks for your purchase!\nHere is what you bought:");
                cart.ForEach(x => Console.WriteLine(string.Format("{0,-17}{1,-1}", $"{x}", $"${stock[x]:N2}")));
                Console.WriteLine("----------------------------------");
                //display highest price
                //vvv alternate method used before making sortCart()
                //Console.WriteLine(string.Format("{0,-17}{1,-1}", "Highest Price:", $"{hiCost(cart, stock, out double hi)} at ${hi:N2}"));
                Console.WriteLine(string.Format("{0,-17}{1,-1}", "Highest Price:", $"{cart[cart.Count - 1]} at ${stock[cart[cart.Count - 1]]}"));
                //display lowest price
                //vvv alternate method used before making sortCart()
                //Console.WriteLine(string.Format("{0,-17}{1,-1}", "Lowest Price:", $"{lowCost(cart, stock, out double low)} at ${low:N2}"));
                Console.WriteLine(string.Format("{0,-17}{1,-1}", "Lowest Price:", $"{cart[0]} at ${stock[cart[0]]:N2}"));
                //display price of customer cart
                //vvv Alternate option, build specifications call for loop
                //Console.WriteLine("---------------------------\n" + string.Format("{0,-17}{1,-1}", "Total Price:", $"${cartprice:N2}"));
                Console.WriteLine(string.Format("{0,-17}{1,-1}", "Total Price:", $"${getTotal(cart, stock):N2}"));
                Console.WriteLine("Thank you, have a nice day!");
                break;
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("ERROR: Please enter y or n");
                continue;
            }
        }
    }
    //prompt if item entered is't available
    else
    {
        Console.WriteLine("Sorry, we don't carry that item here. Please try again.");
        continue;
    }
}

//------------methods

//getTotal() method
static double getTotal(List<string> list, Dictionary<string, double> dictionary)
{
    double total = 0;
    list.ForEach(s => total += dictionary[s]);
    return total;   
}

//hiCost() method
static string hiCost(List<string> list, Dictionary<string, double> dictionary, out double cost)
{
    cost = 0;
    string item = "";
    foreach (string s in list)
    {
        if (dictionary[s] > cost)
        {
            item = s;
            cost = dictionary[s];
        }
    }
    return item;
}

//lowCost() method
static string lowCost(List<string> list, Dictionary<string, double> dictionary, out double cost)
{
    cost = 999;
    string item = "";
    foreach (string s in list)
    {
        if (dictionary[s] < cost)
        {
            item = s;
            cost = dictionary[s];
        }
    }
    return item;
}

//sortCart() method
static void sortCart(ref List<string> list, Dictionary<string, double> dictionary)
{
    for(int i = 0; i < list.Count - 1; i++)
    {
        for (int j = i + 1; j < list.Count; j++)
        {
            if (dictionary[list[i]] > dictionary[list[j]])
            {
                (list[i], list[j]) = (list[j], list[i]);
            }
        }
    }
    
}