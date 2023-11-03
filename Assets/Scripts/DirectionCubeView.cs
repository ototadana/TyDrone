using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionCubeView : MonoBehaviour
{
    [SerializeField]
    private GameObject[] arrows;

    private bool enableArrows = true;

    public bool EnableArrows
    {
        get
        {
            return enableArrows;
        }

        set
        {
            enableArrows = value;
            UpdateArrows(enableArrows);
        }
    }

    private void Start()
    {
        UpdateArrows(enableArrows);
    }

    private void UpdateArrows(bool enable)
    {
        if (arrows == null)
        {
            return;
        }

        foreach (var arrow in arrows)
        {
            arrow.SetActive(enable);
        }
    }


}
