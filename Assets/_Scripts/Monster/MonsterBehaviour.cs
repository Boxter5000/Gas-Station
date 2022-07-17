using UnityEngine;
using UnityEngine.AI;
namespace _Scripts.Monster
{
    public class MonsterBehaviour : MonoBehaviour
    {
        private NavMeshAgent _agent;

        private Transform _player;

        private LayerMask _loud, _smelly, _ground;

        //Idling
        private Vector3 _walkPoint;
        private bool _isWalkPointSet;
        private float _walkPointRange;

        //Searching
        private float _smellRadius;
        private float _hearingRadius;
        private Vector3 _targetPosition;

        //State Variables
        private bool _objectInHearingRange;
        private bool _objectInSmellRange;

        public void Awake()
        {
            _agent = GetComponentInChildren<NavMeshAgent>();
            _player = FindObjectOfType<FirstPersonController>().transform;
        }

        public void SearchTarget()
        {
            var position = transform.position;
            //Hearing
            var hearHit = Physics.SphereCastAll(position, _hearingRadius, Vector3.zero, Mathf.Infinity, _loud);

            RaycastHit previousObj = new RaycastHit();
            RaycastHit currentLoudestObj = new RaycastHit();
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
                    currentLoudestObj = (currentLoudestObj.transform.gameObject.GetComponentInParent<AudioSource>().volume > previousObj.transform.gameObject.GetComponentInParent<AudioSource>().volume) 
                        ? currentLoudestObj 
                        : previousObj;
                }
                previousObj = obj;
            }
            

            _targetPosition = currentLoudestObj.transform.position + Random.insideUnitSphere * currentLoudestObj.transform.gameObject.GetComponentInParent<AudioSource>().volume * 5;
            
            var smellHit = Physics.SphereCastAll(position, _smellRadius, Vector3.zero, Mathf.Infinity,_smelly);
        }
    }
}
