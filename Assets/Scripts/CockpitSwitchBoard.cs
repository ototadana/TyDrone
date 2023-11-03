using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine;

[RequireComponent(typeof(CockpitStickController))]
public class CockpitSwitchBoard : MonoBehaviour
{
    private NgoEngine engine;
    private CockpitStickController stickController;

    [SerializeField]
    private Turntable turntable;

    [SerializeField]
    private VideoContainerDisplayPositions displayPositions;

    [SerializeField]
    private Interactable monitorButton;

    [SerializeField]
    private Interactable recordingButton;


    private void Start()
    {
        engine = NgoEngine.GetInstance();
        stickController = GetComponent<CockpitStickController>();

        Debug.Assert(stickController != null);
        Debug.Assert(turntable != null);
        Debug.Assert(displayPositions != null);
        Debug.Assert(monitorButton != null);
        Debug.Assert(recordingButton != null);
    }


    public bool FPV
    {
        get
        {
            return !turntable.AutoTurn;
        }

        set
        {
            if (value)
            {
                displayPositions.Center();
                monitorButton.gameObject.SetActive(false);
                turntable.AutoTurn = false;
                stickController.RotationSpeed = RotationSpeed.Slow;
            }
            else
            {
                displayPositions.Bottom();
                turntable.AutoTurn = true;
                monitorButton.gameObject.SetActive(true);
                monitorButton.IsToggled = false;
                stickController.RotationSpeed = RotationSpeed.Fast;
            }
        }
    }

    public bool Fast
    {
        get
        {
            return stickController.Fast;
        }

        set
        {
            stickController.Fast = value;
            turntable.Size = value ? Size.Small : Size.Normal;
        }
    }

    public bool Recording
    {
        get
        {
            return recordingButton.IsToggled;
        }

        set
        {
            recordingButton.IsToggled = value;
            engine.SetRecording(value);
        }
    }
}
