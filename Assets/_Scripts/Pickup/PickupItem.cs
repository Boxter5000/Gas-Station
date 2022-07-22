using UnityEngine;

namespace _Scripts.Pickup
{
    [RequireComponent(typeof(SphereCollider), typeof(Rigidbody), typeof(Outline))]
    public class PickupItem : MonoBehaviour, IInteract
    {
        [SerializeField] private float launchForce = 20f;
        
        private SphereCollider _collider;
        private Rigidbody _rb;
        private Outline _outline;
        
        
        private void Awake()
        {
            _collider = GetComponent<SphereCollider>();
            _collider.isTrigger = true;
            _rb = GetComponent<Rigidbody>();
            _outline = GetComponent<Outline>();
            _outline.enabled = false;
        }

        public void OnInteract(GameObject owner)
        {
            var t = transform;
            t.position = owner.transform.position;
            t.parent = owner.transform;
            owner.GetComponent<Player.Pickup>()._holdsItem = true;
            
            _rb.isKinematic = true;
        }

        public void OnEndInteract(Transform lauchDir)
        {
            _rb.isKinematic = false;
            transform.parent = null;
            _rb.AddForce(lauchDir.forward * launchForce, ForceMode.Impulse);
        }

        public void DrawOutline(bool draw)
        {
            _outline.enabled = draw;
        }
    }
}
