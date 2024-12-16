public class Stopwatch{
    public delegate void StopwatchEventHandler(string message);
    private int timeElapsed;
    private bool isRunning;

    public event StopwatchEventHandler? OnStarted; 
    public event StopwatchEventHandler? OnStopped;
    public event StopwatchEventHandler? OnReset;

    public int TimeElapsed => timeElapsed;

    public Stopwatch()
    {
        timeElapsed = 0;
        isRunning = false;
    }

    public void Start()
    {
        if (!isRunning)
        {
            isRunning = true;
            OnStarted?.Invoke("Stopwatch Started!");
            StartTicking();
        }
        else{
            Console.WriteLine("Stopwatch is already running.");
        }
    }

    public void Stop()
    {
        if (isRunning)
        {
            isRunning = false;
            OnStopped?.Invoke("Stopwatch Stopped!");
        }
        else {
            Console.WriteLine("Stopwatch is not running.");
        }
    }

    public void Reset()
    {
        timeElapsed = 0;
        OnReset?.Invoke("Stopwatch Reset!");
    }
    private void StartTicking(){
        new Thread(() =>{
            while (isRunning){
                Thread.Sleep(1000); 
                timeElapsed++;

                int currentCursorLeft = Console.CursorLeft;
                int currentCursorTop = Console.CursorTop;

                int centerX = Console.WindowWidth / 2; 
                int offsetX = centerX + 10; 

                Console.SetCursorPosition(offsetX, Console.WindowHeight - 2);
                Console.WriteLine($"Time Elapsed: {timeElapsed} seconds".PadRight(Console.WindowWidth - offsetX));

                Console.SetCursorPosition(currentCursorLeft, currentCursorTop);
            }

            int clearCursorLeft = Console.CursorLeft;
            int clearCursorTop = Console.CursorTop;

            Console.SetCursorPosition(0, Console.WindowHeight - 2);
            Console.WriteLine("".PadRight(Console.WindowWidth));
            Console.SetCursorPosition(clearCursorLeft, clearCursorTop);
        }).Start();
    }
}
