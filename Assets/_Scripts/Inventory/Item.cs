using _Scripts.Inventory;
using UnityEngine;

public class Item
{
    public enum ItemType
    {
        Ziptie,
        Lighter,
        Key,
        Dynamite,
        Battery
    }

    public ItemType itemType;
    public int amount;

    public Sprite GetSprite()
    {
        switch (itemType)
        {
            default:
            case ItemType.Ziptie:  return ItemAssets.Instance.Ziptie;
            case ItemType.Lighter:  return ItemAssets.Instance.LighterSprite;
            case ItemType.Key:      return ItemAssets.Instance.KeysSprite;
            case ItemType.Dynamite: return ItemAssets.Instance.DynamitSprite;
            case ItemType.Battery:  return ItemAssets.Instance.Battery;
        }
    }

    public bool isStackable()
    {
        switch (itemType)
        {
            case ItemType.Battery:
            case ItemType.Dynamite:
            case ItemType.Ziptie: 
                return true;
            case ItemType.Key:
            case ItemType.Lighter:
                return false;
        }

        return false;
    }
}