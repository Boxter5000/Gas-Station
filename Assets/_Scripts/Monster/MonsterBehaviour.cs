using System;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace _Scripts.Monster
{
    public class MonsterBehaviour : MonoBehaviour
    {
        private NavMeshAgent _agent;

        private Transform _player;

        [SerializeField] private Terrain terrain;

        [SerializeField] private LayerMask loud, smelly;

        private Vector3Int _xzPlane = new Vector3Int(1, 0, 1);

        //Idling
        private Vector3 _walkPoint;
        private bool _isWalkPointSet;
        private float _walkPointRange;

        //Searching
        [SerializeField] private float smellRadius;
        [SerializeField] private float hearingRadius;
        
        [SerializeField] private float hearingOffset;
        [SerializeField] private float smellingOffset;
        
        private Vector3 _targetPosition;
        private bool _targetSet;

        //State Variables
        private bool _objectInHearingRange;
        private bool _objectInSmellRange;

        private float Timer;
        
        public void Awake()
        {
            _agent = GetComponentInChildren<NavMeshAgent>();
            _player = FindObjectOfType<FirstPersonController>().transform;
        }

        private void Start()
        {
            _targetPosition = transform.position;
        }

        private void Update()
        {
            Debug.Log(_targetSet.ToString());
            if (!_targetSet)
            {
                SearchTarget();
            }
            else
            {
                _agent.SetDestination(_targetPosition);
                Debug.Log(_targetPosition.ToString());
                Timer += Time.deltaTime;
                if (Timer > 10)
                {
                    _targetSet = false;
                }
            }

            var temp = _agent.transform.position;
            temp.y = terrain.SampleHeight(_targetPosition);
            if ((temp - _targetPosition).magnitude > 2) return;
            _targetSet = false;
        }

        private void SetSoundTargetPosition(GameObject obj, float offset)
        {
            _targetPosition = obj.transform.position 
                              + Random.insideUnitSphere * (GetAudioVolume(obj.transform.gameObject.GetComponentInParent<AudioSource>(), 1024) * offset - offset);
            _targetPosition.y = terrain.SampleHeight(_targetPosition);
            _targetSet = true;
            Debug.Log("FoundTarget: " + obj.name);
            _objectInHearingRange = true;
        }
        
        private void SetSmellTargetPosition(GameObject obj, float offset)
        {
            _targetPosition = obj.transform.position 
                              + Random.insideUnitSphere * ((int)obj.gameObject.GetComponentInParent<Smell>().currentSmellStrength * offset);
            _targetPosition.y = terrain.SampleHeight(_targetPosition);
            _targetSet = true;
            Debug.Log("FoundTarget: " + obj.name);
            _objectInSmellRange = true;
        }

        private float GetAudioVolume(AudioSource source, int sampleLength)
        {
            if (source.clip == null) return 0; 
            var clipSampleData = new float[sampleLength];
            source.clip.GetData(clipSampleData, source.timeSamples);
            return clipSampleData.Sum(sample => Mathf.Abs(sample)) / sampleLength;
        }

        private void SearchTarget()
        {
            var position = transform.position;
            //Hearing
            var hearHit = Physics.OverlapSphere(position, hearingRadius, loud);

            Debug.Log(hearHit.Length.ToString());
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
                    currentLoudestObj = (GetAudioVolume(currentLoudestObj.GetComponentInParent<AudioSource>(), 1024) 
                                         > GetAudioVolume(previousObj.GetComponentInParent<AudioSource>(), 1024)) 
                        ? currentLoudestObj 
                        : previousObj;
                }
                previousObj = obj;
            }

            if (_targetSet) return;
            {
                var smellHit = Physics.OverlapSphere(position, smellRadius,smelly);
            
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
                        currentSmelliestObj = (currentSmelliestObj.GetComponentInParent<Smell>().currentSmellStrength 
                                               > previousObj.GetComponentInParent<Smell>().currentSmellStrength) 
                            ? currentSmelliestObj 
                            : previousObj;
                    }
                    previousObj = obj;
                }

                if (currentLoudestObj != null && currentSmelliestObj != null)
                {
                    var source = currentLoudestObj.GetComponentInParent<AudioSource>();
                    var smell = currentSmelliestObj.GetComponentInParent<Smell>().currentSmellStrength;

                    var audioVolume = GetAudioVolume(source, 1024);

                    if (audioVolume != 0 && smell != Smell.SmellStrength.Covered)
                    {
                        if (audioVolume <= 0)
                        {
                            SetSmellTargetPosition(currentSmelliestObj.gameObject, smellingOffset);
                            return;
                        }

                        if (smell == Smell.SmellStrength.Covered)
                        {
                            SetSoundTargetPosition(currentLoudestObj.gameObject, hearingOffset);
                            return;
                        }

                        if (audioVolume >= 1)
                        {
                            SetSoundTargetPosition(currentLoudestObj.gameObject, hearingOffset);
                        }
                        else if (audioVolume < 1 && smell == Smell.SmellStrength.Exposed)
                        {
                            SetSmellTargetPosition(currentSmelliestObj.gameObject, smellingOffset);
                        }
                        else if (audioVolume < 0.6 && smell >= Smell.SmellStrength.Lingering)
                        {
                            SetSmellTargetPosition(currentSmelliestObj.gameObject, smellingOffset);
                        }
                    }
                    else
                    {
                        _targetSet = true;
                        Patroling();
                    }
                }
                else
                {
                    _targetSet = true;
                    Patroling();
                }
            }
        }

        private void Patroling()
        {
            var Offset = 8;
            var _patrolPosition = _targetPosition + Random.onUnitSphere * Offset;
            _patrolPosition.y = terrain.SampleHeight(_patrolPosition);
            _targetPosition = _patrolPosition;
            
            Debug.Log("Patrolling");
        }
    }
}
