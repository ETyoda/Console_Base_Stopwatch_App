using System;
using System.Threading;

namespace StopwatchApp
{
    
    public delegate void StopwatchEventHandler(string message);

    public class Stopwatch
    {
        
        private int _timeElapsed; 
        private bool _isRunning;

        
        public event StopwatchEventHandler OnStarted;
        public event StopwatchEventHandler OnStopped;
        public event StopwatchEventHandler OnReset;

    
        public int TimeElapsed => _timeElapsed;
        public bool IsRunning => _isRunning;

        
        public void Start()
        {
            if (_isRunning)
            {
                Console.WriteLine("Stopwatch is already running!");
                return;
            }

            _isRunning = true;
            OnStarted?.Invoke("Stopwatch Started!");
        }

        public void Stop()
        {
            if (!_isRunning)
            {
                Console.WriteLine("Stopwatch is not running!");
                return;
            }

            _isRunning = false;
            OnStopped?.Invoke("Stopwatch Stopped!");
        }

        public void Reset()
        {
            _timeElapsed = 0;
            _isRunning = false;
            OnReset?.Invoke("Stopwatch Reset!");
        }

        public void Tick()
        {
            if (_isRunning)
            {
                _timeElapsed += 100; 
            }
        }

        public string GetFormattedTime()
        {
            int totalSeconds = _timeElapsed / 1000;
            int milliseconds = _timeElapsed % 1000;

            int hours = totalSeconds / 3600;
            int minutes = (totalSeconds % 3600) / 60;
            int seconds = totalSeconds % 60;

            return $"{hours:D2}:{minutes:D2}:{seconds:D2}:{milliseconds:D3}";
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

            
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("Console Stopwatch Application");
                Console.WriteLine("--------------------------------");
                Console.WriteLine($"Time Elapsed: {stopwatch.GetFormattedTime()}");
                Console.WriteLine("[S] Start  [T] Stop  [R] Reset  [Q] Quit");
                Console.Write("Enter your choice: ");

                if (Console.KeyAvailable)
                {
                    char input = Console.ReadKey(true).KeyChar;

                    switch (char.ToUpper(input))
                    {
                        case 'S':
                            stopwatch.Start();
                            break;
                        case 'T':
                            stopwatch.Stop();
                            break;
                        case 'R':
                            stopwatch.Reset();
                            break;
                        case 'Q':
                            exit = true;
                            stopwatch.Stop();
                            Console.WriteLine("Exiting the application...");
                            break;
                        default:
                            Console.WriteLine("Invalid input. Please try again.");
                            break;
                    }
                }

                
                Thread.Sleep(100);
                stopwatch.Tick();
            }
        }
    }
}
