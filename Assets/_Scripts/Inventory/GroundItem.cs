using _Scripts.Pickup;
using Assets._Scripts.Inventory;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class GroundItem : MonoBehaviour, IInteract
{
    private InventoryManager _inventoryManager;
    [SerializeField] private InventoryItemTypes _type;
    [SerializeField] private Sprite _icon;
    [SerializeField] private int _amount;
    private void Awake()
    {
        GetComponent<SphereCollider>().isTrigger = true;
        _inventoryManager = FindObjectOfType<InventoryManager>();
    }
    
    
    public void OnInteract(GameObject owner)
    {
        _inventoryManager.AddItemToInventory(new ItemHolder(_type, _icon, _amount));
        Destroy(gameObject);
    }
    
    public void OnEndInteract(Transform launchDir)
    {
        throw new System.NotImplementedException();
    }
}
