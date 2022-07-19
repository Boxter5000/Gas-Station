using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Inventory
{
    public event EventHandler OnItemListChange;
    private List<Item> itemList;
    
    public Inventory()
    {
        itemList = new List<Item>();
    }

    public void AddItem(Item item)
    {
        
        if (item.isStackable())
        {
            bool alreadyInInventory = false;
            foreach (var inventoryItem in itemList)
            {
                if (inventoryItem.itemType == item.itemType)
                {
                    inventoryItem.amount += item.amount;
                    alreadyInInventory = true;
                }
            }
            if (!alreadyInInventory)
            {
                itemList.Add(item);
            }
        }
        else
        {
            itemList.Add(item);
        }
        OnItemListChange?.Invoke(this, EventArgs.Empty);
    }

    public List<Item> GetItemList()
    {
        return itemList;
    }
}
