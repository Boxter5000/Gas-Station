using _Scripts.Pickup;
using Assets._Scripts.Inventory;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private int InventorySize;
    [SerializeField] private ItemHolder emptySlot;
    private ItemHolder[] _inventoryArray;

    private void Awake()
    {
        _inventoryArray = new ItemHolder[InventorySize];
        _inventoryArray = GetComponentsInChildren<ItemHolder>();
    }
    public bool AddItemToInventory(ItemHolder type)
    {
        for (int i = 0; i < _inventoryArray.Length; i++)
        {
            if (_inventoryArray[i].holdItem == type.holdItem)
            {
                _inventoryArray[i].itemCount++;
                UpdateSlot(i);
                return true;
            }
            if (_inventoryArray[i] == null)
            {
                _inventoryArray[i] = type;
                UpdateSlot(i);
                return true;
            }
        }

        return false;
    }
    public bool HasItemInInventory(InventoryItemTypes type)
    {
        foreach (var i in _inventoryArray)
        {
            if (i.holdItem == type) return true;
        }

        return false;
    }

    public bool RemoveItemFromInventory(ItemHolder type, int removeAmount = 1)
    {
        for (int i = 0; i < _inventoryArray.Length; i++)
        {
            if (_inventoryArray[i].holdItem == type.holdItem)
            {
                if (_inventoryArray[i].itemCount > removeAmount++)
                {
                    _inventoryArray[1].itemCount -= removeAmount;
                }
                else
                {
                    _inventoryArray[1] = emptySlot;
                }
                return true;
            }
        }

        return false;
    }

    private void UpdateSlot(int slotIndex)
    {
        var thisItem = _inventoryArray[slotIndex];

        thisItem._text.text = thisItem.itemCount.ToString();
        thisItem._image.sprite = thisItem.itemIcon;
    }
}
