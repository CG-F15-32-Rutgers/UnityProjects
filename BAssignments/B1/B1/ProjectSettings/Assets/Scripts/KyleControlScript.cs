using UnityEngine;
using System.Collections;
[RequireComponent(typeof(Animator))]
public class KyleControlScript : MonoBehaviour {

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

    void Start () {
        anim = GetComponent<Animator>();
    }

   
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        anim.SetFloat("Speed", v);
        anim.SetFloat("Direction", h);
        anim.speed = animSpeed;
        currentBaseState = anim.GetCurrentAnimatorStateInfo(0);

        //jump

        if (currentBaseState.fullPathHash == idleState || currentBaseState.fullPathHash == fwdState || currentBaseState.fullPathHash == bkwdState || currentBaseState.fullPathHash == runState)
        {
            if (Input.GetButtonDown("Jump"))
            {
                anim.SetBool("Jump", true);
            }
            if (Input.GetButtonDown("Fire3"))
            {
                anim.SetBool("Run", true);
            }
        }
        else if (currentBaseState.fullPathHash == jumpIdle || currentBaseState.fullPathHash == jumpBack || currentBaseState.fullPathHash == jumpForward || currentBaseState.fullPathHash == runJump)
        {
            if (!anim.IsInTransition(0))
            {
                anim.SetBool("Jump", false);
            }
        }
        if (currentBaseState.fullPathHash == runState || currentBaseState.fullPathHash == runJump)
        {
            if (Input.GetButtonUp("Fire3"))
            {
                anim.SetBool("Run", false);
            }
        }

    }
}
