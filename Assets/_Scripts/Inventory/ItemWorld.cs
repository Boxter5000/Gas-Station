using _Scripts.Pickup;
using UnityEngine;
[RequireComponent(typeof(Outline))]
public class ItemWorld : MonoBehaviour, IInteract
{
    [SerializeField] private Item.ItemType _type;
    [SerializeField] private int _amount;
    
    private Item _item;
    private MeshFilter mesh;
    private Outline _outline;
    
    private void Awake()
    {
        _item = new Item {itemType = _type, amount = _amount};
        SetItem(_item);
        _outline = GetComponent<Outline>();
        _outline.enabled = false;
    }

    public void SetItem(Item item)
    {
        _item = item;
    }
    
    public void OnInteract(GameObject owner)
    {
        var _owner = owner.transform.parent.GetComponent<FirstPersonController>();
        _owner.inventory.AddItem(_item);
        Destroy(gameObject);
    }

    public void OnEndInteract(Transform launchDir)
    {
    }

    public void DrawOutline(bool draw)
    {
        if(_outline != null)
            _outline.enabled = draw;
    }
}
