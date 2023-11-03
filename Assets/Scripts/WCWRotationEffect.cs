using Microsoft.MixedReality.Toolkit.Utilities;
using UnityEngine;

[RequireComponent(typeof(HoverLight))]
public class WCWRotationEffect : MonoBehaviour
{
    private HoverLight hoverLight;

    [SerializeField]
    private GameObject propeller;

    private void Start()
    {
        hoverLight = GetComponent<HoverLight>();

        Debug.Assert(hoverLight != null);
        Debug.Assert(propeller != null);
    }

    public void StartRotation()
    {
        hoverLight.enabled = true;
        propeller.SetActive(true);
    }

    public void StopRotation()
    {
        hoverLight.enabled = false;
        propeller.SetActive(false);
    }

}
