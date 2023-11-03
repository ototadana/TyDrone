using UnityEngine;

[RequireComponent(typeof(CockpitStickController))]
public class AutoRotation : MonoBehaviour
{
    private NgoEngine engine;
    private CockpitStickController stickController;
    private bool enabledAutoRotation;
    private float lastValue;

    [SerializeField]
    private Turntable turntable;

    public bool EnabledAutoRoataion
    {
        get
        {
            return enabledAutoRotation;
        }
        set
        {
            enabledAutoRotation = value;
            stickController.UpdateRotation(0f);
            engine.SetEnabled("DepthSensor", value);
            turntable.Size = value ? Size.Big : Size.Normal;
        }
    }

    private void Start()
    {
        engine = NgoEngine.GetInstance();
        stickController = GetComponent<CockpitStickController>();

        Debug.Assert(stickController != null);
        Debug.Assert(turntable != null);
    }

    private void Update()
    {
        if (!enabledAutoRotation || stickController.IsInRotation)
        {
            return;
        }

        float rotation = ToRoatation(engine.GetState("nearest"));
        stickController.UpdateRotation(rotation);
    }

    private float ToRoatation(float val)
    {
        /*
        float rotation = (intensity - 4f) * 0.15f + 0.45f;
            return val > 0 ? rotation : -rotation;
        */

        float intensity = Mathf.Abs(val);
        if (intensity < 2f)
        {
            return 0f;
        }

        float rotation = 0f;
        if (val > 0f && lastValue > 0f)
        {
            rotation = 0.2f;
        }

        if (val < 0f && lastValue < 0f)
        {
            rotation = -0.2f;
        }
        lastValue = val;
        return rotation;
    }
}
