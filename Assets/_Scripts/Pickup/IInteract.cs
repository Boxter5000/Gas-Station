using UnityEngine;

namespace _Scripts.Pickup
{
    public interface IInteract
    {
        void OnInteract(GameObject owner);
        void OnEndInteract(Transform launchDir);
        void DrawOutline(bool draw);
    }
}