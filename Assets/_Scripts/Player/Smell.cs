using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smell : MonoBehaviour
{
    public enum SmellStrength
    {
        Covered = -1, // Enemy can not Sense you
        Lingering = 2, // enemy knows your general position
        Exposed = 0, // Enemy has full awareness of where you are
    }

    private readonly List<SmellObject> _smellsInRange = new List<SmellObject>();
    private SmellObject _nearestSmellObject;
    private float _shortestRange = float.MaxValue;

    public SmellStrength currentSmellStrength = SmellStrength.Exposed;
    [SerializeField] private bool debugMode;
    private void Update()
    {
        UpdateSmell();
        if (debugMode) {
            Debug.Log(currentSmellStrength.ToString());
        }
        
    }

    private void UpdateSmell()
    {
        if (_smellsInRange.Count != 0)
        {
            var distanceToSmell = 0f;
            foreach (var smell in _smellsInRange)
            {
                distanceToSmell = Vector3.Distance(smell.gameObject.transform.position, transform.position);
                if (distanceToSmell < _shortestRange)
                {
                    _shortestRange = distanceToSmell;
                    _nearestSmellObject = smell;
                }
            }

            if (_nearestSmellObject.maxDistance * _nearestSmellObject.covertPercent < distanceToSmell)
                currentSmellStrength = SmellStrength.Lingering;
            else
                currentSmellStrength = SmellStrength.Covered;
        }
        else
        {
            _shortestRange = float.MaxValue;
            _nearestSmellObject = null;
            currentSmellStrength = SmellStrength.Exposed;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var newSmell = other.gameObject.GetComponent<SmellObject>();
        if (newSmell != null)
        {
            _smellsInRange.Add(newSmell);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (_smellsInRange.Contains(other.gameObject.GetComponent<SmellObject>()))
        {
            _smellsInRange.Remove(other.gameObject.GetComponent<SmellObject>());
        }
        else
        {
            Debug.LogWarning("the SmellObject was already Removed");
        }
    }
}
