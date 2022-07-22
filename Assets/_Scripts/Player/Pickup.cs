using _Scripts.Pickup;
using UnityEngine;

namespace _Scripts.Player
{
    public class Pickup : MonoBehaviour
    {
        [SerializeField] private KeyCode pickupKey = KeyCode.E;
        [SerializeField] private Transform origin;
        [SerializeField] private float pickupDistance;

        private IInteract _currentItem;
        private IInteract _lastItem;
        public bool _holdsItem;
        
        private void Update()
        {
            var ray = new Ray(origin.position, origin.forward);
            if (!Physics.Raycast(ray, out var hit, pickupDistance)) return;

            if(_currentItem !=null)
                _lastItem = _currentItem;
            
            _currentItem = hit.transform.gameObject.GetComponent<IInteract>();
            if (_currentItem == null)
            {
                _currentItem = null;
            }

            Interact();
            SetOutline();
        }

        private void Interact()
        {
            if (!Input.GetKeyDown(pickupKey) || _currentItem == null) return;
            if(!_holdsItem)
                _currentItem.OnInteract(gameObject);
            else
            {
                _lastItem.OnEndInteract(origin);
                _holdsItem = false;
                _currentItem = null;
            }
        }
        private void SetOutline()
        {
            if (_currentItem != null)
            {
                _currentItem.DrawOutline(true);
            }
            else if(_lastItem != null)
            {
                _lastItem.DrawOutline(false);
            }
            
        }
    }
}
