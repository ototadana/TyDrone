using UnityEngine;

public class TelloRotationStabilizer : MonoBehaviour
{
    private void Update()
    {
        if (Mathf.Abs(transform.localRotation.eulerAngles.y) > 30f)
        {
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, Quaternion.identity, 10f);
        }
        else
        {
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, Quaternion.identity, 1f);
        }
    }
}