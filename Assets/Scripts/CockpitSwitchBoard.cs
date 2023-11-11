using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine;

[RequireComponent(typeof(CockpitStickController), typeof(CockpitCommandManager), typeof(CockpitEffect))]
public class CockpitSwitchBoard : MonoBehaviour
{
    private NgoEngine engine;
    private CockpitStickController stickController;
    private CockpitCommandManager commandManager;
    private CockpitEffect effect;

    [SerializeField]
    private Turntable turntable;

    [SerializeField]
    private VideoContainerDisplayPositions displayPositions;

    [SerializeField]
    private Interactable monitorButton;

    [SerializeField]
    private Interactable recordingButton;

    [SerializeField]
    private Interactable calibrateButton;


    private void Start()
    {
        engine = NgoEngine.GetInstance();
        stickController = GetComponent<CockpitStickController>();
        commandManager = GetComponent<CockpitCommandManager>();
        effect = GetComponent<CockpitEffect>();

        Debug.Assert(stickController != null);
        Debug.Assert(commandManager != null);
        Debug.Assert(effect != null);
        Debug.Assert(turntable != null);
        Debug.Assert(displayPositions != null);
        Debug.Assert(monitorButton != null);
        Debug.Assert(recordingButton != null);
        Debug.Assert(calibrateButton != null);
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

    public void Takeoff()
    {
        commandManager.Takeoff();
        effect.Takeoff();
        calibrateButton.IsToggled = false;
        turntable.Calibrate = false;
    }
}
