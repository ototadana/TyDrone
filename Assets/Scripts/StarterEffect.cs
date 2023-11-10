using Microsoft.MixedReality.Toolkit.Utilities;
using UnityEngine;

public class StarterEffect : MonoBehaviour
{
    private HoverLight hoverLight;

    private NgoCountDown countDown;
    private Vector3 messagePosition;
    private Vector3 positionPattern;
    private new Renderer renderer;

    [SerializeField]
    private Message message;

    [SerializeField]
    private GameObject propeller;

    [SerializeField]
    private Animator propellerAnimator;

    [SerializeField]
    private CockpitSwitchBoard switchBoard;

    private void Start()
    {
        hoverLight = GetComponent<HoverLight>();
        renderer = GetComponent<Renderer>();

        Debug.Assert(hoverLight != null);
        Debug.Assert(renderer != null);

        Debug.Assert(message != null);
        Debug.Assert(propellerAnimator != null);
        Debug.Assert(propeller != null);
        Debug.Assert(switchBoard != null);


        messagePosition = message.transform.position;
        positionPattern = new Vector3(Random.Range(0f, 100f), Random.Range(0f, 100f), Random.Range(0f, 100f));
    }

    private void OnProgress(object sender, int count)
    {
        message.Show(count.ToString());
    }

    private void OnComplete(object sender, int count)
    {
        propellerAnimator.SetTrigger("Takeoff");
        renderer.enabled = false;
        switchBoard.Takeoff();
        Invoke("RestoreMessagePosition", 2f);
    }

    private void RestoreMessagePosition()
    {
        message.transform.position = messagePosition;
    }

    private void Update()
    {
        if (countDown != null)
        {
            countDown.Update();
        }
        else
        {
            transform.localPosition = GetRandomOffset(1f, 0.2f) * 0.05f;
        }
    }


    public void StartEngine()
    {
        hoverLight.enabled = true;
        propeller.SetActive(true);
        message.transform.position =
            new Vector3(transform.position.x, transform.position.y, transform.position.z);
        if (countDown == null)
        {
            countDown = new NgoCountDown(3, OnProgress, OnComplete);
        }
    }

    public void StopEngine()
    {
        hoverLight.enabled = false;
        propeller.SetActive(false);
        countDown = null;
        message.transform.position = messagePosition;
    }

    private Vector3 GetRandomOffset(float ratio, float frequencyRate)
    {
        float freq = Time.time * frequencyRate;

        return new Vector3(
          (Mathf.PerlinNoise(freq, positionPattern.x) - 0.5f) * ratio,
          (Mathf.PerlinNoise(freq, positionPattern.y) - 0.5f) * ratio + 0.5f,
          (Mathf.PerlinNoise(freq, positionPattern.z) - 0.5f) * ratio
        );
    }

}
