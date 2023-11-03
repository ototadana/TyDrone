using UnityEngine;

public class VideoContainerVideoPlayer : MonoBehaviour
{
    private readonly float textureMultiplierStep = 1f / 60f;
    private float textureMultiplier = 0.5f;
    private NgoEngine engine;

    [SerializeField]
    private Renderer center;


    private void Start()
    {
        engine = NgoEngine.GetInstance();
        Debug.Assert(center != null);
    }

    private void Update()
    {
        UpdateTexture();
    }

    private void UpdateTexture()
    {
        byte[] bytes = engine.PickImage();
        if (bytes.Length == 0)
        {
            return;
        }

        bytes = FadeIn(bytes);
        if (textureMultiplier >= 1.0 && engine.GetState("lit") != 0f)
        {
            bytes = Dark(bytes, 0.6f);
        }
        ChangeTexture(center, bytes);
    }

    private void ChangeTexture(Renderer renderer, byte[] bytes)
    {
        Texture2D texture = new Texture2D(960, 720, TextureFormat.RGB24, false);
        texture.LoadRawTextureData(bytes);
        texture.Apply();

        Destroy(renderer.material.mainTexture);
        renderer.material.mainTexture = texture;
    }

    private byte[] FadeIn(byte[] bytes)
    {
        if (textureMultiplier >= 1.0)
        {
            return bytes;
        }

        var nb = Dark(bytes, textureMultiplier);
        textureMultiplier += textureMultiplierStep;
        return nb;
    }

    private byte[] Dark(byte[] bytes, float textureMultiplier)
    {
        byte[] nb = new byte[bytes.Length];
        for (int i = 0; i < bytes.Length; i++)
        {
            nb[i] = (byte)(((float)bytes[i]) * textureMultiplier);
        }
        return nb;
    }
}
