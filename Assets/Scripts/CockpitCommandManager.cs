using System;
using UnityEngine;


public class CockpitCommandManager : MonoBehaviour
{
    private NgoEngine engine;

    private int landCount;
    private bool takeoff;
    private string stickCommand;
    private string extraCommand;

    private void Start()
    {
        engine = NgoEngine.GetInstance();
    }

    private void LateUpdate()
    {
        if (landCount > 0)
        {
            engine.EntryCommand("land");
            landCount--;
            return;
        }

        if (takeoff)
        {
            engine.EntryCommand("takeoff");
            takeoff = false;
            return;
        }

        if (!string.IsNullOrEmpty(extraCommand))
        {
            engine.EntryCommand(extraCommand);
            extraCommand = "";
            return;
        }

        if (!string.IsNullOrEmpty(stickCommand))
        {
            engine.EntryCommand(stickCommand);
            return;
        }
    }

    public void Land()
    {
        landCount = 5;
        takeoff = false;
        stickCommand = "";
        extraCommand = "";
    }

    public void Takeoff()
    {
        takeoff = true;
    }

    public void SetStickCommand(string stickCommand)
    {
        this.stickCommand = stickCommand;
    }

    public void SetExtraCommand(string extraCommand)
    {
        if(!IsMoving())
        {
            this.extraCommand = extraCommand;
        }
    }
    private bool IsMoving()
    {
        string[] vals = stickCommand.Split();

        if(vals.Length < 2)
        {
            return false;
        }

        for(int i = 1; i < vals.Length; i++)
        {
            if (float.TryParse(vals[i], out float num) && num > 0f)
            {
                return true;
            }
        }
        return false;
    }
}
