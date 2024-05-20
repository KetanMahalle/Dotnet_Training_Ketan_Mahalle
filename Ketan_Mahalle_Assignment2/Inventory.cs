using System;
using System.Collections.Generic;
using System.Linq;

public class Inventory
{
    public List<Item> items;

    public Inventory()
    {
        items = new List<Item>();
    }

    public void AddItem(Item item)
    {
        items.Add(item);
        Console.WriteLine("Item added successfully.");
    }

    public void DisplayAllItems()
    {
        if (items.Count == 0)
        {
            Console.WriteLine("No items in inventory.");
            return;
        }

        //Converting List in array for assignment.
        //Though we can simply iterate over list as well.
        /*   foreach (var item in items)
             {
                 Console.WriteLine(item);
             }                                   
        */

        Item[] itemsArray = items.ToArray();
        foreach (var item in itemsArray)
        {
            Console.WriteLine(item);
        }
    }

    public Item FindItemById(int id)
    {
        foreach (var item in items)
        {
            if (item.ID == id)
            {
                return item;
            }
        }
        return null;
    }


    public void UpdateItem(int id, string newName, double newPrice, int newQuantity)
    {
        var item = FindItemById(id);
        if (item != null)
        {
            item.Name = newName;
            item.Price = newPrice;
            item.Quantity = newQuantity;
            Console.WriteLine("Item updated successfully.");
        }
        else
        {
            Console.WriteLine("Item not found.");
        }
    }

    public void DeleteItem(int id)
    {
        var item = FindItemById(id);
        if (item != null)
        {
            items.Remove(item);
            Console.WriteLine("Item deleted successfully.");
        }
        else
        {
            Console.WriteLine("Item not exist. Plz ensure correct id to Delete.");
        }
    }
}
