using UnityEngine;
using UnityEngine.UI;

public class UI_Inventory : MonoBehaviour
{
    private Inventory _inventory;
    private Transform itemSlotContainer;
    private Transform itemSlotTemplate;

    private void Awake()
    {
        itemSlotContainer = transform.Find("InventoryHolder");
        itemSlotTemplate = itemSlotContainer.Find("ItemSlot");
    }

    public void SetInventory(Inventory inventory)
    {
        _inventory = inventory;
        RefreshInventoryItems();
    }

    public void RefreshInventoryItems()
    {
        foreach (var item in _inventory.GetItemList())
        {
            RectTransform itemSlotRectTransform =
                Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>(); 
            itemSlotRectTransform.gameObject.SetActive(true);
            Image image = itemSlotRectTransform.Find("Icon").GetComponent<Image>();
            image.sprite = item.GetSprite();
        }
    }
}
