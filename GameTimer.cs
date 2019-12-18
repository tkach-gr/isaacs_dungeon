using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ECSIsaac
{
    static class GameTimer
    {
        static Stopwatch stopwatch;
        static double frameStart;

        public static double FrameTime => Elapsed - frameStart;
        public static double Elapsed => TimeSpan.FromTicks(stopwatch.ElapsedTicks).TotalMilliseconds;

        static GameTimer()
        {
            stopwatch = new Stopwatch();
        }

        public static void Start()
        {
            stopwatch.Start();
        }

        public static void Stop()
        {
            stopwatch.Stop();
        }

        public static void Reset()
        {
            stopwatch.Reset();
        }

        public static void NextFrame()
        {
            frameStart = Elapsed;
        }
    }
}
