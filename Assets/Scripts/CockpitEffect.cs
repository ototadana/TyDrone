using UnityEngine;

public class CockpitEffect : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private GameObject rotationController;

    [SerializeField]
    private GameObject starter;

    protected virtual void Start()
    {
        Debug.Assert(animator != null);
        Debug.Assert(rotationController != null);
        Debug.Assert(starter != null);
    }

    public void Takeoff()
    {
        Invoke("RevealCockpit", 3f);
    }

    private void RevealCockpit()
    {
        starter.SetActive(false);
        rotationController.SetActive(true);
        animator.enabled = true;
        animator.Play("Takeoff");
    }

    private void Update()
    {
        if (!animator.enabled)
        {
            return;
        }

        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            animator.enabled = false;
        }
    }
}
