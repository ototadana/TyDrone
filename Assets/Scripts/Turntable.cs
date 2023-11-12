using UnityEngine;

public enum Size { Small, Normal, Big }

public class Turntable : MonoBehaviour
{
    private NgoEngine engine;
    private int target = int.MaxValue;
    private int offset = 0;
    private bool autoTurn = true;
    private Size size = Size.Normal;

    public bool AutoTurn
    {
        get => autoTurn;
        set
        {
            autoTurn = value;
            if (!autoTurn)
            {
                transform.localRotation = Quaternion.identity;
            }
        }
    }

    public bool Calibrate { get; set; }

    public Size Size
    {
        get => size;
        set
        {
            size = value;
            if (size == Size.Small)
            {
                transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
            }
            else if (size == Size.Big)
            {
                transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            }
            else
            {
                transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
            }
        }
    }

    private void Start()
    {
        engine = NgoEngine.GetInstance();
    }

    private void Update()
    {
        if (!AutoTurn)
        {
            return;
        }

        var currentY = Normalize(Mathf.RoundToInt(transform.rotation.eulerAngles.y));

        if (target != int.MaxValue)
        {
            if (target == currentY)
            {
                target = int.MaxValue;
                return;
            }
            var speed = Mathf.Abs(target - currentY) > 30f ? 10f : 1f;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0f, target, 0f), speed);
            return;
        }

        var yaw = Mathf.RoundToInt(engine.GetState("yaw"));
        if (Calibrate)
        {
            transform.rotation = Quaternion.identity;
            offset = yaw;
        }
        else
        {
            if (AutoTurn)
            {
                yaw -= offset;
            }

            if (yaw != currentY)
            {
                target = Normalize(yaw);
            }
        }
    }

    public void UpdateOffset()
    {
        UpdateOffset(transform.rotation.eulerAngles.y);
    }

    public void UpdateOffset(float y)
    {
        offset = Mathf.RoundToInt(y);
    }

    private int Normalize(int a)
    {
        a = a % 360;
        return (a < 0) ? 360 + a : a;
    }

}
