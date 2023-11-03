using UnityEngine;

public enum RotationSpeed { Fast, Slow }

[RequireComponent(typeof(CockpitCommandManager))]
public class CockpitStickController : MonoBehaviour
{
    private float rx, ry, lx, ly;
    private int fast = 0;

    [SerializeField]
    private Transform tello;

    [SerializeField]
    private Renderer telloBody;

    private CockpitCommandManager commandManager;

    private bool isOperating;
    private bool isInRotation;

    public bool IsOperating { get => isOperating; set => isOperating = value; }

    public bool IsInRotation { get => isInRotation; }

    public bool Fast
    {
        get
        {
            return fast == 1;
        }
        set
        {
            fast = value ? 1 : 0;
        }
    }

    public RotationSpeed RotationSpeed { get; set; } = RotationSpeed.Fast;

    private void Start()
    {
        commandManager = transform.GetComponent<CockpitCommandManager>();

        Debug.Assert(tello != null);
        Debug.Assert(telloBody != null);
        Debug.Assert(commandManager != null);
    }

    private void Update()
    {
        UpdateCommand();
        var command = $"stick {rx:F2} {ry:F2} {lx:F2} {ly:F2} {fast}";
        commandManager.SetStickCommand(command);

        UpdateColor();
    }

    private void UpdateColor()
    {
        if (telloBody == null)
        {
            return;
        }

        var maxValue = MaxValue();
        if (maxValue > 0.28f)
        {
            var doubleMaxValue = maxValue * 2f;
            var gb = Mathf.Max(1f - doubleMaxValue, 0f);

            var r = 1f;
            if (doubleMaxValue > 1f)
            {
                r += (1f - doubleMaxValue) * 2;
            }
            telloBody.material.color = new Color(r, gb, gb);
        }
        else
        {
            telloBody.material.color = Color.white;
        }
    }

    private float MaxValue()
    {
        return Mathf.Max(Mathf.Abs(rx), Mathf.Max(Mathf.Abs(ry), Mathf.Abs(ly)));
    }


    private void UpdateCommand()
    {
        if (isOperating)
        {
            rx = ToStickValue(tello.localPosition.x);
            ry = ToStickValue(tello.localPosition.z);
            ly = ToStickValue(tello.localPosition.y);
        }
        else
        {
            rx = 0f; // RightX left < 0 < right
            ry = 0f; // RightY backward < 0 < forward
            ly = 0f; // LeftY down < 0 < up
        }
    }

    private float ToStickValue(float v)
    {
        if (v < -1.0f)
        {
            return -1.0f;
        }

        if (v > 1.0f)
        {
            return 1.0f;
        }

        return v;
    }

    public void UpdateRotation(float rotation)
    {
        lx = rotation;
    }

    public void StartRotation(bool ccw)
    {
        isInRotation = true;
        var speed = RotationSpeed == RotationSpeed.Fast ? 0.5f : 0.2f;
        UpdateRotation(ccw ? -speed : speed);
    }

    public void StopRotation()
    {
        isInRotation = false;
        UpdateRotation(0);
    }
}
