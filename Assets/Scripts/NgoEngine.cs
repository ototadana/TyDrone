using System;
using UnityEngine;

public abstract class NgoEngine
{
    private static NgoEngine engine;

    public static NgoEngine GetInstance()
    {
        if (engine == null)
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                engine = new TyDroneAndroidPlugin();
            }
            else
            {
                engine = new TyDroneStub();
            }
            engine.Run();
        }
        return engine;
    }


    protected abstract void Run();

    public abstract void EntryCommand(string command);

    public abstract string GetSentCommand();

    public abstract string GetNotice();

    public abstract byte[] PickImage();

    public abstract void SetRecording(bool value);

    public abstract bool IsRecording();

    public float GetState(string name)
    {
        return GetState(name, 0f);
    }

    public abstract float GetState(string name, float defaultValue);

    public abstract void SetEnabled(string name, bool enabled);
}

class TyDroneAndroidPlugin : NgoEngine
{
    private AndroidJavaObject engine;

    public TyDroneAndroidPlugin()
    {
        this.engine = new AndroidJavaObject("com.xpfriend.tydrone.AndroidMain");
    }

    public override void EntryCommand(string command)
    {
        engine.Call("entryCommand", command);
    }

    public override string GetNotice()
    {
        return engine.Call<string>("getNotice");
    }

    public override string GetSentCommand()
    {
        return engine.Call<string>("getSentCommand");
    }

    public override float GetState(string name, float defaultValue)
    {
        string value = engine.Call<string>("getState", name);
        return value != null ? float.Parse(value) : defaultValue;
    }

    public override bool IsRecording()
    {
        return engine.Call<bool>("isRecording");
    }

    public override byte[] PickImage()
    {
        AndroidJavaObject obj = this.engine.Call<AndroidJavaObject>("pickImage");
        if (obj == null || obj.GetRawObject() == null)
        {
            return new byte[] { };
        }

        sbyte[] image = AndroidJNIHelper.ConvertFromJNIArray<sbyte[]>(obj.GetRawObject());
        if (image == null)
        {
            return new byte[] { };
        }
        return (byte[])(Array)image;
    }

    public override void SetEnabled(string name, bool enabled)
    {
        this.engine.Call("setEnabled", name, enabled);
    }

    public override void SetRecording(bool value)
    {
        this.engine.Call("setRecording", value);
    }

    protected override void Run()
    {
        this.engine.Call("run");
    }
}

class TyDroneStub : NgoEngine
{
    private float dummyTimeout = float.MaxValue;
    private string sentCommand;
    private string notice;
    private float yaw;
    private string nextNotice;
    private bool recording;

    public override void EntryCommand(string command)
    {
        if (command.StartsWith("takeoff") || command.StartsWith("land"))
        {
            nextNotice = $"OK ({command})";
            dummyTimeout = Time.time + 3f;
        }
        sentCommand = command;
    }

    public override string GetNotice()
    {
        if (Time.time > dummyTimeout)
        {
            dummyTimeout = float.MaxValue;
            notice = nextNotice;
        }
        return notice;
    }

    public override string GetSentCommand()
    {
        return sentCommand;
    }

    private float GetYaw()
    {
        if (sentCommand == null || !sentCommand.StartsWith("stick "))
        {
            return yaw;
        }

        var s = sentCommand.Split(' ');
        if (float.Parse(s[3]) > 0.01f)
        {
            yaw = yaw + Mathf.RoundToInt(Time.deltaTime * 35f);
        }
        else if (float.Parse(s[3]) < -0.01f)
        {
            yaw = yaw - Mathf.RoundToInt(Time.deltaTime * 35f);
        }

        return yaw;
    }

    public override byte[] PickImage()
    {
        return new byte[] { };
    }

    protected override void Run()
    {
    }

    public override void SetRecording(bool value)
    {
        recording = value;
    }

    public override bool IsRecording()
    {
        return recording;
    }

    public override float GetState(string name, float defaultValue)
    {
        if (name == "yaw")
        {
            return GetYaw();
        }
        else if (name == "bat")
        {
            return 38f;
        }
        return defaultValue;
    }

    public override void SetEnabled(string name, bool enabled)
    {
    }
}