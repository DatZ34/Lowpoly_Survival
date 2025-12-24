using UnityEngine;

public class HumanoiAnimationController : MonoBehaviour
{
    [Header("Animation Setting")]
    public Animator animator;

    [Header("Parameters")]
    protected const string PARAM_SPEED = "Speed";
    protected const string PARAM_VELOCITY_X = "VelocityX";
    protected const string PARAM_VELOCITY_Z = "VelocityZ";
    protected const string PARAM_IS_GROUNDED = "IsGrounded";
    protected const string PARAM_IS_MOVING = "IsMoving";
    protected const string PARAM_JUMP = "Jump";

    protected virtual void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }

        if (animator == null)
        {
            Debug.LogError("Animator component not found on " + gameObject.name);
        }
        else
        {
            if (animator.avatar == null)
            {
                 Debug.LogError("Animator avatar is not assigned on " + gameObject.name);
            }
            else if (!animator.avatar.isHuman)
            {
                Debug.LogWarning($" Avatar on {gameObject.name} is not Humanoid!");
            }
            else
            {
                Debug.Log($" Avatar on {gameObject.name} is Humanoid.");
            }    
        }    
    }

    protected void SetSpeed(float speed)
    {
        if (animator != null)
        {
            animator.SetFloat(PARAM_SPEED, speed);
        }
    }

    protected void SetVelocity(float x, float z)
    {
        if (animator != null)
        {
            animator.SetFloat(PARAM_VELOCITY_X, x);
            animator.SetFloat(PARAM_VELOCITY_Z, z);
        }
    }

    protected void SetGrounded(bool grounded)
    {
        if (animator != null)
            animator.SetBool(PARAM_IS_GROUNDED, grounded);
    }

    protected void Setmoving(bool moving)
    {
        if (animator != null)
        {
            animator.SetBool(PARAM_IS_MOVING, moving);
        }
    }
    protected void TriggerJump()
    {
        if (animator != null)
        {
            animator.SetTrigger(PARAM_JUMP);
        }
    }

    protected bool IsAnimationPlaying(string stateName)
    {
        if (animator == null) return false;

        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        return stateInfo.IsName(stateName);
    }

    protected float GetAnimationNormalizedTime()
    {
        if (animator == null) return 0f;

        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        return stateInfo.normalizedTime;
    }
    // Update is called once per frame
    protected virtual void Update()
    {
        
    }
}
