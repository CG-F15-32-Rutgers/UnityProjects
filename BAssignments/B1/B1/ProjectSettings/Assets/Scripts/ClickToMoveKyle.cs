using UnityEngine;
using System.Collections;

namespace CompleteProject
{
    [RequireComponent(typeof(Animator))]

    public class ClickToMoveKyle : MonoBehaviour
    {
        private NavMeshAgent navMeshAgent;
        private bool running;
        public bool selected;

        public float animSpeed = 1.5f;
        private Animator anim;
        private AnimatorStateInfo currentBaseState;
        private AnimatorStateInfo layer2CurrentState;

        static int idleState = Animator.StringToHash("Base Layer.Idle");
        static int fwdState = Animator.StringToHash("Base Layer.Fwd");
        static int bkwdState = Animator.StringToHash("Base Layer.Bkwd");
        static int jumpIdle = Animator.StringToHash("Base Layer.JumpIdle");
        static int jumpBack = Animator.StringToHash("Base Layer.JumpBack");
        static int jumpForward = Animator.StringToHash("Base Layer.JumpForward");
        static int runState = Animator.StringToHash("Base Layer.Run");
        static int runJump = Animator.StringToHash("Base Layer.RunJump");
        // Use this for initialization
        void Awake()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            navMeshAgent.autoTraverseOffMeshLink = false;
            running = false;
            selected = false;
            anim = GetComponent<Animator>();
            //animSpeed = navMeshAgent.speed;
        }

        // Update is called once per frame
        void Update()
        {
            if(navMeshAgent.remainingDistance > 0)
            {
                anim.SetFloat("Speed", navMeshAgent.speed);
                //navMeshAgent.updateRotation = true;
            }
            else
            {
                anim.SetFloat("Speed", 0);
                running = false;
                //anim.SetFloat("Direction", navMeshAgent.speed);
            }
            anim.speed = animSpeed;
            currentBaseState = anim.GetCurrentAnimatorStateInfo(0);

            if (currentBaseState.fullPathHash == idleState || currentBaseState.fullPathHash == fwdState || currentBaseState.fullPathHash == bkwdState || currentBaseState.fullPathHash == runState)
            {
                if (navMeshAgent.isOnOffMeshLink)
                {
                        anim.SetBool("Jump", true);
                }
                if (running)
                {
                    anim.SetBool("Run", true);
                }
            }
            else if (currentBaseState.fullPathHash == jumpIdle || currentBaseState.fullPathHash == jumpBack || currentBaseState.fullPathHash == jumpForward || currentBaseState.fullPathHash == runJump)
            {
                if (!anim.IsInTransition(0))
                {
                    anim.SetBool("Jump", false);
                    navMeshAgent.CompleteOffMeshLink();
                    navMeshAgent.Resume();
                }
            }
            if (currentBaseState.fullPathHash == runState || currentBaseState.fullPathHash == runJump)
            {
                if (!running)
                {
                    anim.SetBool("Run", false);
                }
            }
        }

        public void moveAgent(RaycastHit hit, bool run)
        {
            navMeshAgent.destination = hit.point;
            navMeshAgent.Resume();
        }
    }
}
