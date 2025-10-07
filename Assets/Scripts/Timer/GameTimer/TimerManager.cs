using System.Collections.Generic;
using UnityEngine;

public class TimerManager : Singleton<TimerManager>
{
    private readonly List<GameTimer> timers = new List<GameTimer>();

    public GameTimer StartTimer(float duration, float tickInterval = 1f)
    {
        var timer = new GameTimer(duration, tickInterval);
        timers.Add(timer);
        timer.Start();
        return timer;
    }

    private void Update()
    {
        float delta = Time.deltaTime;

        for (int i = timers.Count - 1; i >= 0; i--)
        {
            timers[i].Update(delta);

            // Remove finished timers automatically
            if (!timers[i].IsRunning && timers[i].Remaining <= 0f)
            {
                timers.RemoveAt(i);
            }
        }
    }
}
