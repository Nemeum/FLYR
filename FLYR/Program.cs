using System;
using System.Collections.Generic;
using System.Diagnostics;

/*Create the ProductInStore class with the properties: Product, codeName, and Price.
    Make the getters and setter to access to the value of each variable*/
public class ProductInStore
{
    public string Product { get; set; }
    public string codeName { get; set; }
    public decimal Price { get; set; }
}

/*Create the Checkout Class with its public Checkout method*/
public class Checkout
{
    /*Create two dictionaries
     cart: It will store the product codes (GR1,SR1,CF1) and their quantities
     pricing_rules: It will store the product codes (GR1,SR1,CF1) and their corresponding
    Product objects (ProductInStore) */
    private Dictionary<string, int> cart = new Dictionary<string, int>();
    private Dictionary<string, ProductInStore> pricing_rules;

    /*It initializes the pricing_rules with the passed argument*/
    public Checkout(Dictionary<string, ProductInStore> pricing_rules)
    {
        this.pricing_rules = pricing_rules;
    }

    /*This is the Scan method. It adds an item to the cart.*/
    public void Scan(string item)
    {
        //If the item is already in the cart, it increments the quantity.
        if (cart.ContainsKey(item))
        {
            cart[item]++;
        }
        //Otherwise, it adds the item to the cart with a quantity of 1
        else
        {
            cart[item] = 1;
        }
    }

    /*The Total() method It calculates the total price of the items in the cart 
      according to the pricing rules.
      Setup the decimal in the declaration of method Total(), because it will return a decimal number.*/
    public decimal Total()
    {
        //Initialize the variable total to zero
        decimal total = 0;
        /*It iterates over each item in the cart and adds the price of the item to the total based on the pricing rules*/
        foreach (var item in cart)
        {
            //item.Key: It returns "GR1”, “SR1", or “CF1”,
            switch (item.Key)
            {
                //If the item is “GR1”, it applies the “buy one get one” offer.
                case "GR1":
                    int qty = item.Value;
                    total += pricing_rules[item.Key].Price * (qty / 2 + qty % 2);
                    break;
                /*If the item is “SR1” and the quantity is 3 or more, it applies the discounted price of £4.50.
                  Otherwise, it applies the normal price of £5.00*/
                case "SR1":
                    total += item.Value >= 3 ? 4.5m * item.Value : pricing_rules[item.Key].Price * item.Value;
                    break;
                /*If the item is “CF1” and the quantity is 3 or more, it applies the discounted price of two thirds of the normal price.
                  Otherwise, it applies the normal price of £11.23 */
                case "CF1":
                    total += item.Value >= 3 ? pricing_rules[item.Key].Price * 2 / 3 * item.Value : pricing_rules[item.Key].Price * item.Value;
                    break;
                /* If the item in the cart is not “GR1”, “SR1”, or “CF1”, it applies the normal price.*/
                default:
                    total += pricing_rules[item.Key].Price * item.Value;
                    break;
            }
        }
        
        return total;
    }
}


class Program
{
    static void Main(string[] args)
    {
        //It creates a dictionary of ProductInStore objects.
        var pricing_rules = new Dictionary<string, ProductInStore>
        {
            { "GR1", new ProductInStore { Product = "GR1", codeName = "Green Tea", Price = 3.11m } },
            { "SR1", new ProductInStore { Product = "SR1", codeName = "Strawberries", Price = 5m } },
            { "CF1", new ProductInStore { Product = "CF1", codeName = "Coffee", Price = 11.23m } }
        };

        /* Declare a variable co and creating a new instance of the Checkout class
          with pricing_rules as an argument to the constructor */
        var co = new Checkout(pricing_rules);
        co.Scan("GR1");
        co.Scan("SR1");
        co.Scan("CF1");
        co.Scan("GR1");
        co.Scan("CF1");
        co.Scan("SR1");
        co.Scan("SR1");
        co.Scan("CF1");

        //Calling the Total method that calculates the total price of all the items that have been added to the checkout.
        var price = co.Total(); 
        
        //Showing in the console the total price
        Console.WriteLine(String.Format("{0:Total price: £0.00}", price));
       
        
    }
}
