using _Scripts.Pickup;
using UnityEngine;

public class PickupInventory : MonoBehaviour, IInteract
{
    public void OnInteract(GameObject owner)
    {
        
    }

    public void OnEndInteract(Transform launchDir)
    {
        throw new System.NotImplementedException();
    }
}
