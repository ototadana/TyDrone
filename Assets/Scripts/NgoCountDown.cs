using System;
using UnityEngine;

public class NgoCountDown
{
    private float startTime;
    private int count;

    public event EventHandler<int> OnProgress;
    public event EventHandler<int> OnComplete;

    public NgoCountDown(int count, EventHandler<int> onProgress, EventHandler<int> onComplete)
    {
        this.OnProgress += onProgress;
        this.OnComplete += onComplete;
        this.count = count + 1;
    }

    public void Update()
    {
        if (count == 0)
        {
            return;
        }

        if (Time.time > startTime + 1f)
        {
            count--;
            startTime = Time.time;
            OnProgress(this, count);
        }

        if (count == 0)
        {
            OnComplete(this, 0);
        }
    }

    public void Reset()
    {
        count = int.MaxValue;
        startTime = 0;
    }
}
