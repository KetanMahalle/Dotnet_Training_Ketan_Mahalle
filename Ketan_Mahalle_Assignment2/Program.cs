﻿using System;
using System.Collections.Generic;
using System.Linq;
public class Item
{
    public int ID;
    public string Name;
    public double Price;
    public int Quantity;

    public Item(int id, string name, double price, int quantity)
    {
        ID = id;
        Name = name;
        Price = price;
        Quantity = quantity;
    }

    public override string ToString()
    {
        string result = "ID:"+ID+" ||  Name:"+Name+ " ||  Price:"+Price+"  || Quantity: "+Quantity;
        return result;
    }
}

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



class Program
{
    static void Main(string[] args)
    {
        Inventory obj = new Inventory();
        bool startLoop = true;

        while (startLoop)
        {
            Console.WriteLine("\nInventory Management System");
            Console.WriteLine("1. Add Item");
            Console.WriteLine("2. Display All Items");
            Console.WriteLine("3. Find Item by ID");
            Console.WriteLine("4. Update Item");
            Console.WriteLine("5. Delete Item");
            Console.WriteLine("6. Exit");
            Console.Write("Select an option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddNewItem(obj);
                    break;
                case "2":
                    obj.DisplayAllItems();
                    break;
                case "3":
                    FindItem(obj);
                    break;
                case "4":
                    UpdateExistingItem(obj);
                    break;
                case "5":
                    DeleteExistingItem(obj);
                    break;
                case "6":
                    startLoop = false;
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }

    static void AddNewItem(Inventory obj)
    {
        Console.Write("Enter item ID: ");
        int id = Convert.ToInt32(Console.ReadLine());

        Console.Write("Enter item name: ");
        string name = Console.ReadLine();

        Console.Write("Enter item price: ");
        double price = Convert.ToDouble(Console.ReadLine());

        Console.Write("Enter item quantity: ");
        int quantity = Convert.ToInt32(Console.ReadLine());

        Item item = new Item(id, name, price, quantity);
        obj.AddItem(item);
    }

    static void FindItem(Inventory obj)
    {
        if (obj.items.Count == 0)
        {
            Console.WriteLine("No items in inventory.");
            return;
        }
        else
        {
            Console.Write("Enter item ID to find: ");
            int id = Convert.ToInt32(Console.ReadLine());

            Item item = obj.FindItemById(id);
            if (item != null)
            {
                Console.WriteLine(item);
            }
            else
            {
                Console.WriteLine("Item not found.");
            }
        }
    }

    static void UpdateExistingItem(Inventory obj)
    {
        if (obj.items.Count == 0)
        {
            Console.WriteLine("No items in inventory.");
            return;
        }
        else
        {
            Console.Write("Enter item ID to update: ");
            int id = Convert.ToInt32(Console.ReadLine());
            Item exist = obj.FindItemById(id);
            if (exist != null)
            {

                Console.Write("Enter new item name: ");
                string newName = Console.ReadLine();

                Console.Write("Enter new item price: ");
                double newPrice = Convert.ToDouble(Console.ReadLine());

                Console.Write("Enter new item quantity: ");
                int newQuantity = Convert.ToInt32(Console.ReadLine());

                obj.UpdateItem(id, newName, newPrice, newQuantity);

            }
            else
            {
                Console.WriteLine("Id not exist. Plz ensure correct id to update.");
            }
        }
    }

    static void DeleteExistingItem(Inventory obj)
    {
        if (obj.items.Count == 0)
        {
            Console.WriteLine("No items in inventory.");
            return;
        }
        else
        {
            Console.Write("Enter item ID to delete: ");
            int id = Convert.ToInt32(Console.ReadLine());

            obj.DeleteItem(id);
        }
    }
}

