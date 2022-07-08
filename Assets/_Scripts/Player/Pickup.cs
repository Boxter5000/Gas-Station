using System;
using System.Net.Http.Headers;
using _Scripts.Pickup;
using UnityEngine;

namespace _Scripts.Player
{
    public class Pickup : MonoBehaviour
    {
        [SerializeField] private KeyCode pickupKey = KeyCode.E;
        [SerializeField] private Transform origin;
        [SerializeField] private float pickupDistance;
        private PickupItem _currentItem;
        
        

        private void Update()
        {
            if (!Input.GetKeyDown(pickupKey)) return;
            if (_currentItem == null)
            {
                RaycastHit hit;
                Ray ray = new Ray(origin.position, origin.forward);
                Physics.Raycast(ray ,out hit);

                if(hit.distance > pickupDistance) return;
                _currentItem = hit.transform.gameObject.GetComponent<PickupItem>();
                if (_currentItem != null)
                {
                    _currentItem.OnInteract(gameObject);
                }
            }
            else
            {
                _currentItem.OnEndInteract(origin);
                _currentItem = null;
            }
        }
    }
}
