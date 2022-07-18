using Assets._Scripts.Inventory;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemHolder : MonoBehaviour
{
    [SerializeField] public InventoryItemTypes holdItem;
    [SerializeField] public Sprite itemIcon;
    [HideInInspector] public int itemCount = 0;

    [Header("Refs")]
    [SerializeField] public Image _image;
    [SerializeField] public TMP_Text _text;

    public ItemHolder(InventoryItemTypes type, Sprite sprite, int itemCount)
    {
        this.holdItem = type;
        this.itemIcon = sprite;
        this.itemCount = itemCount;
    }
}
