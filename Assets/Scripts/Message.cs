using TMPro;
using UnityEngine;


[RequireComponent(typeof(TMP_Text), typeof(Animator))]
public class Message : MonoBehaviour
{
    private TMP_Text messageText;
    private Animator animator;

    private void Start()
    {
        messageText = GetComponent<TMP_Text>();
        animator = GetComponent<Animator>();

        Debug.Assert(messageText != null);
        Debug.Assert(animator != null);
    }

    public void Show(string message)
    {
        Show(message, false);
    }

    public void Show(string message, bool error)
    {
        messageText.text = message;

        if (error)
        {
            messageText.color = Color.red;
        }
        else
        {
            messageText.color = Color.white;
        }

        animator.SetTrigger("Visible");
    }
    private void OnAnimatorMove()
    {
        animator.ResetTrigger("Visible");
    }

}
