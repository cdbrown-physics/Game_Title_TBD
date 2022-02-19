using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Inventory : ScriptableObject
{
    // Reference to the current item
    public Item currentItem;
    public List<Item> items = new List<Item>(); // inventory, effectivly.
    public int numKeys;

    public void AddItem(Item item)
    {
        // Check if the item is a key
        if (item.isKey)
        {
            numKeys++;
        }
        else
        {
            items.Add(item);
        }
    }

}
