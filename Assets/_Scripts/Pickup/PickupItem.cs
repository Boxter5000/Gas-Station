using UnityEngine;

namespace _Scripts.Pickup
{
    [RequireComponent(typeof(SphereCollider), typeof(Rigidbody))]
    public class PickupItem : MonoBehaviour, IInteract
    {
        [SerializeField] private float launchForce = 20f;
        
        private SphereCollider _collider;
        private Rigidbody _rb;
        private void Awake()
        {
            _collider = GetComponent<SphereCollider>();
            _collider.isTrigger = true;
            _rb = GetComponent<Rigidbody>();
        }

        public void OnInteract(GameObject owner)
        {
            var t = transform;
            t.position = owner.transform.position;
            t.parent = owner.transform;

            _rb.isKinematic = true;
        }

        public void OnEndInteract(Transform lauchDir)
        {
            _rb.isKinematic = false;
            transform.parent = null;
            _rb.AddForce(lauchDir.forward * launchForce, ForceMode.Impulse);
        }
    }
}
