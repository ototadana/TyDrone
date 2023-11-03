using UnityEngine;

public class TelloHandGestureModelController : MonoBehaviour
{
    private bool toHome;

    [SerializeField]
    private CockpitStickController stickController;

    private void Start()
    {
        Debug.Assert(stickController != null);
    }

    private void Update()
    {
        if (OVRInput.GetActiveController() == OVRInput.Controller.Touch)
        {
            return;
        }

        if (toHome)
        {
            transform.localPosition = Vector3.zero;
        }
        stickController.IsOperating = !toHome;
    }

    public void SetToHome(bool value)
    {
        toHome = value;
    }

}
