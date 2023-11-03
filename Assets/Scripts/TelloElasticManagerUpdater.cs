using Microsoft.MixedReality.Toolkit.Experimental.Physics;
using UnityEngine;

[RequireComponent(typeof(ElasticsManager))]
public class TelloElasticManagerUpdater : MonoBehaviour
{
    private readonly float interval = 0.2f;

    private ElasticsManager elastics;


    protected void Start()
    {
        elastics = GetComponent<ElasticsManager>();
        Debug.Assert(elastics != null);

        UpdateBounds();
    }

    public void UpdateBounds()
    {
        var extent = elastics.TranslationElasticExtent;
        var bounds = elastics.TranslationElasticExtent.StretchBounds;
        bounds.center = transform.parent.position;
        bounds.extents = new Vector3(interval * 2, interval * 2, interval * 2);

        extent.StretchBounds = bounds;
        extent.SnapPoints = new Vector3[] { bounds.center };
        extent.SnapRadius = interval / 2;

        elastics.TranslationElasticExtent = extent;
    }

    private void Update()
    {
        UpdateBounds();
    }
}
