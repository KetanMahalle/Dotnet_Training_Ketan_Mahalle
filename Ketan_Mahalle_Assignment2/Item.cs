using System;

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

