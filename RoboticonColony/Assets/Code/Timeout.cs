using System;
using System.Diagnostics;

public class Timeout
{
    private int timeAllowed;
    private Stopwatch stopwatch;

    /// <summary>
    /// creates a timout object. use to keep track of how long phases are going
    /// </summary>
    /// <param name="time">time in seconds of the timeout</param>
    public Timeout(int time)
    {
        if (time < 1)
        {
            throw new ArgumentOutOfRangeException("Must be at least one second");
        }
        stopwatch = new Stopwatch();
        timeAllowed = time;
    }

    /// <summary>
    /// start the timer
    /// </summary>
    public void Start()
    {
        stopwatch.Start();
    }

    /// <summary>
    /// get the seconds remaining before a timeout
    /// </summary>
    public int SecondsRemaining
    {
        get
        {
            return Math.Max(timeAllowed - stopwatch.Elapsed.Seconds, 0);
        }
    }

    /// <summary>
    /// check if the timeout has reached its time allowed
    /// </summary>
    public bool Finished
    {
        get
        {
            return SecondsRemaining == 0;
        }
    }
}
