using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace _Scripts.Monster
{
    public class MonsterBehaviour : MonoBehaviour
    {
        private NavMeshAgent _agent;

        private Transform _player;

        [SerializeField] private LayerMask _loud, _smelly, _ground;

        private Vector3Int XZPlane = new Vector3Int(1, 0, 1);

        public GameObject destination;
        
        //Idling
        private Vector3 _walkPoint;
        private bool _isWalkPointSet;
        private float _walkPointRange;

        //Searching
        [SerializeField] private float _smellRadius;
        [SerializeField] private float _hearingRadius;
        private Vector3 _targetPosition;
        private bool _targetSet;
        [SerializeField] private float hearingOffset;
        [SerializeField] private float smellingOffset;

        //State Variables
        private bool _objectInHearingRange;
        private bool _objectInSmellRange;

        public void Awake()
        {
            _agent = GetComponentInChildren<NavMeshAgent>();
            _player = FindObjectOfType<FirstPersonController>().transform;
        }

        private void Update()
        {
            /*
            Debug.Log(_targetSet);
            if (!_targetSet)
            {
                SearchTarget();
                _agent.SetDestination(_targetPosition);
                Debug.Log(_targetPosition);
            }

            var temp = _agent.transform.position;
            temp.y = 0;
            if ((temp - _targetPosition).magnitude > 8) return;
            _targetSet = false;
            Debug.Log("ReachedDestination - Looking for Target");
            

            _agent.SetDestination(_player.transform.position);
            */
            
            _agent.SetDestination(destination.transform.position);
        }

        private void SearchTarget()
        {
            var position = transform.position;
            //Hearing
            var hearHit = Physics.OverlapSphere(position, _hearingRadius, _loud);

            Debug.Log(hearHit.Length);
            Collider previousObj = null;
            Collider currentLoudestObj = null;
            for (var index = 0; index < hearHit.Length; index++)
            {
                var obj = hearHit[index];
                if (index == 0)
                {
                    currentLoudestObj = obj;
                    previousObj = obj;
                }
                else
                {
                    currentLoudestObj = (currentLoudestObj.GetComponentInParent<AudioSource>().volume 
                                         > previousObj.GetComponentInParent<AudioSource>().volume) 
                        ? currentLoudestObj 
                        : previousObj;
                }
                previousObj = obj;
            }

            if (currentLoudestObj != null)
            {
                _targetPosition = currentLoudestObj.transform.position 
                                  + Random.insideUnitSphere * currentLoudestObj.transform.gameObject.GetComponentInParent<AudioSource>().volume * hearingOffset;
                _targetPosition.y = 0;
                _targetSet = true;
                Debug.Log("FoundTarget: " + currentLoudestObj.name);
            }
            
            if (!_targetSet) 
            {
                var smellHit = Physics.OverlapSphere(position, _smellRadius,_smelly);
            
                previousObj = null;
                Collider currentSmelliestObj = null;
                for (var index = 0; index < smellHit.Length; index++)
                {
                    var obj = smellHit[index];
                    if (index == 0)
                    {
                        currentSmelliestObj = obj;
                        previousObj = obj;
                    }
                    else
                    {
                        currentLoudestObj = (currentLoudestObj.GetComponentInParent<Smell>().currentSmellStrength 
                                             > previousObj.GetComponentInParent<Smell>().currentSmellStrength) 
                            ? currentLoudestObj 
                            : previousObj;
                    }
                    previousObj = obj;
                }

                if (currentSmelliestObj != null)
                {
                    _targetPosition = currentSmelliestObj.transform.position 
                                      + Random.insideUnitSphere * (int)currentLoudestObj.transform.gameObject.GetComponentInParent<Smell>().currentSmellStrength * smellingOffset;
                    _targetPosition.y = 0;
                    _targetSet = true;
                    Debug.Log("FoundTarget");
                }
            }
        }
    }
}
