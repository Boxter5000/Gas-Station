using System;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class SmellObject : MonoBehaviour
{
    [HideInInspector] public float maxDistance;
    private SphereCollider _collider;

    [Range(0, 1)] public float covertPercent = 0.35f;
    [SerializeField] private float colliderRadius = 14f;

    [SerializeField] private bool _drawDebug;

    private void Awake()
    {
        _collider = GetComponent<SphereCollider>();
        _collider.isTrigger = true;
        _collider.radius = colliderRadius;
    }

    private void OnDrawGizmos()
    {
        if (_drawDebug)
        {
            Gizmos.color = new Color(0, 1, 0, .1f);
            Gizmos.DrawSphere(transform.position, colliderRadius);
            Gizmos.color = new Color(0, 0, 1, .2f);
            Gizmos.DrawSphere(transform.position, colliderRadius * covertPercent);
        }
        
    }
}
