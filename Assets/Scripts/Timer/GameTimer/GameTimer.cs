using System;
using UnityEngine;

public class GameTimer
{
    public float Duration { get; private set; }
    public float Remaining { get; private set; }
    public bool IsRunning { get; private set; }

    public event Action<float> OnTick;      // broadcast remaining time
    public event Action OnCompleted;        // when timer ends

    private float tickInterval = 1f; // default 1 sec
    private float tickAccumulator = 0f;

    public GameTimer(float duration, float tickInterval = 1f)
    {
        Duration = duration;
        Remaining = duration;
        this.tickInterval = tickInterval;
    }

    public void Start()
    {
        IsRunning = true;
    }

    public void Pause()
    {
        IsRunning = false;
    }

    public void Reset()
    {
        Remaining = Duration;
        tickAccumulator = 0f;
    }

    public void Update(float deltaTime)
    {
        if (!IsRunning) return;

        Remaining -= deltaTime;
        tickAccumulator += deltaTime;

        // Tick event
        if (tickAccumulator >= tickInterval)
        {
            OnTick?.Invoke(Remaining);
            tickAccumulator = 0f;
        }

        // Completion event
        if (Remaining <= 0f)
        {
            IsRunning = false;
            Remaining = 0f;
            OnCompleted?.Invoke();
        }
    }
}
