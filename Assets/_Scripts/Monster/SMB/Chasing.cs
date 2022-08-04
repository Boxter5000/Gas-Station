using UnityEngine;
using UnityEngine.Animations;

namespace _Scripts.Monster.SMB
{
    public class Chasing : SceneLinkedSMB<MonsterBehaviour>
    {
        public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller)
        {
            base.OnSLStateNoTransitionUpdate(animator, stateInfo, layerIndex, controller);
            m_MonoBehaviour.Patrolling();
        }
    }
}
