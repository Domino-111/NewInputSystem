using UnityEngine;
using UnityEngine.Events;

// System is the namespace, '.Serializable' is the attribute tag. Makes the class/struct viewable in the inspector
[System.Serializable]
public class Alarm
{
    [SerializeField] private float timeRemaining, timeMax;
    [SerializeField] private bool looping;

    [SerializeField] private bool paused, stopped;

    public UnityEvent onComplete;

    public Alarm(float time, bool looping)
    {
        timeMax = time;
        timeRemaining = time;
        // 'this.looping' gets set to the incoming looping
        this.looping = looping;
    }

    public bool IsPlaying => !paused && !stopped;
    public bool IsPaused => paused;
    public bool IsStopped => stopped;

    /// <summary>
    /// Move the alarm towards zero by the given time and trigger the completed event at 0.
    /// </summary>
    /// <param name="time"></param>
    public void Tick(float time)
    {
        if (paused || stopped)
        {
            return;
        }

        timeRemaining -= time;

        if (timeRemaining <= 0)
        {
            if (looping)
            {
                timeRemaining = timeMax + timeRemaining;
            }

            else
            {
                timeRemaining = 0;
                stopped = true;
            }

            onComplete.Invoke();
        }
    }

    public Alarm Play()
    {
        paused = false;
        stopped = false;

        if (timeRemaining <= 0)
        {
            stopped = true;
        }

        // 'return this' allows us to chain methods against one Alarm
        return this;
    }

    public Alarm Pause()
    {
        paused = true;
        return this;
    }

    public Alarm Stop()
    {
        stopped = true;
        timeRemaining = 0;
        return this;
    }

    /// <summary>
    /// Resets an alarm to full time - does not play a stopped/paused alarm. Use '.Play()' for that
    /// </summary>
    /// <returns></returns>
    public Alarm Reset()
    {
        timeRemaining = timeMax;
        return this;
    }

    public Alarm Reset(float newMax)
    {
        timeMax = newMax;
        return Reset();
    }
}
