using TMPro;
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
        Debug.Log(itemSlotContainer);
    }

    public void SetInventory(Inventory inventory)
    {
        _inventory = inventory;
        _inventory.OnItemListChange += Inventory_OnItemListChange;
        RefreshInventoryItems();
    }

    private void Inventory_OnItemListChange(object sender, System.EventArgs e)
    {
        RefreshInventoryItems();
    }
    
    public void RefreshInventoryItems()
    {
        foreach (Transform child in itemSlotContainer)
        {
            if(child == itemSlotTemplate) continue;
            
            Destroy(child.gameObject);
        }
        
        foreach (var item in _inventory.GetItemList())
        {
            RectTransform itemSlotRectTransform =
                Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>(); 
            itemSlotRectTransform.gameObject.SetActive(true);
            Image image = itemSlotRectTransform.Find("Icon").GetComponent<Image>();
            image.sprite = item.GetSprite();
            TextMeshProUGUI uiText = itemSlotRectTransform.Find("Text").GetComponent<TextMeshProUGUI>();
            if (item.amount > 1)
            {
                uiText.SetText(item.amount.ToString());
            }
            else
            {
                uiText.SetText(" ");
            }
        }
    }
}
