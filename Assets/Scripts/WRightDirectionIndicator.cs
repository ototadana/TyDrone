using Microsoft.MixedReality.Toolkit.Utilities;
using UnityEngine;

[RequireComponent(typeof(HoverLight))]
public class WRightDirectionIndicator : MonoBehaviour
{
    [SerializeField]
    private Collider target;

    [SerializeField]
    private GameObject propeller;

    private HoverLight hoverLight;


    private void Start()
    {
        hoverLight = GetComponent<HoverLight>();

        Debug.Assert(target != null);
        Debug.Assert(propeller != null);
        Debug.Assert(hoverLight != null);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == target)
        {
            StartMoter();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other == target)
        {
            StopMoter();
        }
    }

    public void StartMoter()
    {
        hoverLight.enabled = true;
        propeller.SetActive(true);
    }

    public void StopMoter()
    {
        hoverLight.enabled = false;
        propeller.SetActive(false);
    }
}
