using UnityEngine;

public class VideoContainerDisplayPositions : MonoBehaviour
{
    private readonly float movingTime = 0.2f;

    private float endTime;
    private float rotationSpeed;
    private float movingSpeed;
    private float targetPosition = float.MaxValue;
    private float targetRotation = float.MaxValue;

    [SerializeField]
    private Transform mainPanel;

    private void Start()
    {
        Debug.Assert(mainPanel != null);
    }

    public void Center()
    {
        UpdateScale(5f);
        Move(0f, 0f);
    }

    public void Low()
    {
        UpdateScale(2f);
        Move(-0.2f, 20f);
    }

    public void Bottom()
    {
        UpdateScale(2f);
        Move(-0.4f, 30f);
    }

    private void UpdateScale(float size)
    {
        transform.localScale = new Vector3(size, size, size);
    }

    private void Move(float yPosition, float xRotation)
    {
        movingSpeed = (transform.TransformPoint(new Vector3(0, yPosition, 0)).y - mainPanel.position.y) / movingTime;
        rotationSpeed = (xRotation - mainPanel.localRotation.eulerAngles.x) / movingTime;
        endTime = Time.time + movingTime;

        targetPosition = yPosition;
        targetRotation = xRotation;
    }

    private void FixedUpdate()
    {
        if (endTime > Time.time)
        {
            UpdatePostionAndRotation();
            return;
        }

        if (targetPosition != float.MaxValue)
        {
            mainPanel.localPosition = new Vector3(mainPanel.localPosition.x, targetPosition, mainPanel.localPosition.z);
            targetPosition = float.MaxValue;
        }
        if (targetRotation != float.MaxValue)
        {
            var localRotation = mainPanel.localRotation.eulerAngles;
            mainPanel.localRotation = Quaternion.Euler(targetRotation, localRotation.y, localRotation.z);
            targetRotation = float.MaxValue;
        }
    }

    private void UpdatePostionAndRotation()
    {
        mainPanel.Translate(0f, movingSpeed * Time.deltaTime, 0f, Space.World);
        mainPanel.Rotate(rotationSpeed * Time.deltaTime, 0f, 0f);
    }
}
