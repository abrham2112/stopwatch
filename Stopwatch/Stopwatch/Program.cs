using System;
using System.Threading;

class Stopwatch
{
    private TimeSpan timeElapsed;
    private bool isRunning;
    private Timer timer;
    public delegate void StopwatchEventHandler(string message);
    public event StopwatchEventHandler OnStarted;
    public event StopwatchEventHandler OnStopped;
    public event StopwatchEventHandler OnReset;


    public Stopwatch()
    {
        timeElapsed = TimeSpan.Zero;
        isRunning = false;
    }


    public void Start()
    {
        if (isRunning)
        {
            Console.WriteLine("Stopwatch is already running.");
            return;
        }

        isRunning = true;
        timer = new Timer(Tick, null, 400, 1000);
        OnStarted?.Invoke("Stopwatch Started!");
    }

    public void Stop()
    {
        if (!isRunning)
        {
            Console.WriteLine("Stopwatch is not running.");
            return;
        }

        isRunning = false;
        timer?.Dispose();
        OnStopped?.Invoke("Stopwatch Stopped!");
    }

    public void Reset()
    {
        Stop();
        timeElapsed = TimeSpan.Zero;
        OnReset?.Invoke("Stopwatch Reset!");
    }

    private void Tick(object state)
    {
        timeElapsed = timeElapsed.Add(TimeSpan.FromSeconds(1));
        Console.Clear();
        Console.WriteLine("Press 'S' to Start, 'T' to Stop, 'R' to Reset, or 'Q' to Quit.");
        Console.WriteLine($"Time Elapsed: {timeElapsed:hh\\:mm\\:ss}");
    }
}

class Program
{
    static void Main(string[] args)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.OnStarted += message => Console.WriteLine(message);
        stopwatch.OnStopped += message => Console.WriteLine(message);
        stopwatch.OnReset += message => Console.WriteLine(message);

        bool running = true;

        Console.WriteLine("Stopwatch");
        Console.WriteLine("Press 'S' to Start, 'T' to Stop, 'R' to Reset, or 'Q' to Quit.");

        while (running)
        {
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.S:
                        stopwatch.Start();
                        break;
                    case ConsoleKey.T:
                        stopwatch.Stop();
                        break;
                    case ConsoleKey.R:
                        stopwatch.Reset();
                        break;
                    case ConsoleKey.Q:
                        running = false;
                        stopwatch.Stop();
                        Console.WriteLine("Exiting Stopwatch");
                        break;
                    default:
                        Console.WriteLine("Invalid input. Please press 'S', 'T', 'R', or 'Q'.");
                        break;
                }
            }

            Thread.Sleep(100);
        }
    }
}
