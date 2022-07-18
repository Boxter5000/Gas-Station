using System;
using _Scripts.Inventory;
using _Scripts.Pickup;
using Unity.Mathematics;
using UnityEngine;

public class ItemWorld : MonoBehaviour, IInteract
{
    [SerializeField] private Item.ItemType _type;
    [SerializeField] private int _amount;
    
    private Item _item;
    private MeshFilter mesh;

    private void Awake()
    {
        _item = new Item {itemType = _type, amount = _amount};
        SetItem(_item);
    }

    public void SetItem(Item item)
    {
        _item = item;
    }
    
    public void OnInteract(GameObject owner)
    {
        var _owner = owner.transform.parent.GetComponent<FirstPersonController>();
        _owner.inventory.AddItem(_item);
        _owner._uiInventory.RefreshInventoryItems();
        Destroy(gameObject);
    }

    public void OnEndInteract(Transform launchDir)
    {
    }
}
