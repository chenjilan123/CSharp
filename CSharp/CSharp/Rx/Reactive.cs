using System;
using System.Collections.Generic;
using System.Text;
using System.Reactive.Linq;
using System.Reactive.Concurrency;
using System.Threading;
using System.Diagnostics;
using System.Reactive.Subjects;

namespace CSharp.Rx
{
    internal class Reactive
    {
        #region ObserveCollection
        public Reactive ObserveCollection()
        {
            return this.CreateObserver();

            //Sequence
            var sw = Stopwatch.StartNew();
            foreach (var i in EnumerableEventSequence())
            {
                Console.WriteLine(i);
            }
            sw.Stop();
            Console.WriteLine($"Sequence enum time: {sw.Elapsed.TotalSeconds.ToString("0.00")}s");
            Console.WriteLine();

            //Sequence
            sw.Restart();
            IObservable<int> o = EnumerableEventSequence().ToObservable();
            using (var subscription = o.Subscribe(Console.Write))
            {
                Console.WriteLine("IObservable");
            }
            sw.Stop();
            Console.WriteLine($"Observe enum time: {sw.Elapsed.TotalSeconds.ToString("0.00")}s");

            //Parallel
            sw.Restart();
            o = EnumerableEventSequence().ToObservable().SubscribeOn(TaskPoolScheduler.Default);
            using (var subscription = o.Subscribe(Console.Write))
            {
                Console.WriteLine("IObservable async");
                Console.ReadLine();
            }
            sw.Stop();
            Console.WriteLine($"Observe async enum time: {sw.Elapsed.TotalSeconds.ToString("0.00")}s");

            return this;
        }
        #endregion

        #region CustomObserver
        public Reactive CustomObserver()
        {

            return this;
        }
        #endregion

        #region Subject
        public Reactive Subject()
        {
            //https://docs.microsoft.com/en-us/previous-versions/dotnet/reactive-extensions/hh211737(v=vs.103)

            Console.WriteLine("Subject: ");
            var subject = new Subject<string>();
            subject.OnNext("A");
            using (var subscription = OutputToConsole(subject))
            {
                subject.OnNext("B");
                subject.OnNext("C");
                subject.OnNext("D");
                subject.OnNext("E");
                subject.OnCompleted();
                subject.OnNext("F");
            }
            Console.WriteLine();

            Console.WriteLine("ReplaySubject");
            //Buffer
            var replaySubject = new ReplaySubject<string>(2);
            replaySubject.OnNext("A");
            replaySubject.OnNext("B");
            replaySubject.OnNext("C");
            using (var subjection = OutputToConsole(replaySubject))
            {
                replaySubject.OnNext("D");
                replaySubject.OnCompleted();
                replaySubject.OnNext("E");
            }
            Console.WriteLine();

            //Time
            Console.WriteLine("Time window replay subject");
            var timeSubject = new ReplaySubject<string>(TimeSpan.FromSeconds(1.2D));
            timeSubject.OnNext("A");
            Thread.Sleep(TimeSpan.FromSeconds(0.5D));
            timeSubject.OnNext("B");
            Thread.Sleep(TimeSpan.FromSeconds(0.5D));
            timeSubject.OnNext("C");
            Thread.Sleep(TimeSpan.FromSeconds(0.5D));
            timeSubject.OnNext("D");
            Thread.Sleep(TimeSpan.FromSeconds(0.5D));
            timeSubject.OnNext("E");
            Thread.Sleep(TimeSpan.FromSeconds(0.5D));
            timeSubject.OnNext("F");
            using (var subscription = OutputToConsole(timeSubject))
            {
                Thread.Sleep(TimeSpan.FromSeconds(1.5D));
                timeSubject.OnNext("G");
                timeSubject.OnNext("H");
                timeSubject.OnCompleted();
                timeSubject.OnNext("I");
            }
            Console.WriteLine();

            Console.WriteLine("Async subject");
            var asyncSubject = new AsyncSubject<string>();
            asyncSubject.OnNext("F");
            using (var subscription = OutputToConsole(timeSubject))
            {
                Thread.Sleep(TimeSpan.FromSeconds(1.5D));
                asyncSubject.OnNext("G");
                asyncSubject.OnNext("H");
                asyncSubject.OnCompleted();
                asyncSubject.OnNext("I");
            }
            Console.WriteLine();

            Console.WriteLine("Behavior subject");
            var behaviorSubject = new BehaviorSubject<string>("Default"); //缓存"Default"
            //behaviorSubject.OnNext("K");
            //behaviorSubject.OnNext("M");
            using (var subscription = OutputToConsole(behaviorSubject))
            {
                behaviorSubject.OnNext("A");
                behaviorSubject.OnNext("B");
                behaviorSubject.OnNext("C");
                behaviorSubject.OnCompleted();
            }
            Console.WriteLine();

            return this;
        }

        #endregion

        #region CreateObserver
        public Reactive CreateObserver()
        {
            var o = Observable.Return(0);
            using (var sub = OutputToConsole(o)) ;
            Console.WriteLine("-------------------------");

            o = Observable.Empty<int>();
            using (var sub = OutputToConsole(o)) ;
            Console.WriteLine("-------------------------");

            o = Observable.Repeat(500);
            using (var sub = OutputToConsole(o.Take(5))) ;
            Console.WriteLine("-------------------------");

            o = Observable.Range(0, 5);
            using (var sub = OutputToConsole(o.Take(5))) ;
            Console.WriteLine("-------------------------");



            return this;
        }
        #endregion

        private IDisposable OutputToConsole<T>(IObservable<T> sequence)
        {
            return sequence.Subscribe(
                obj => Console.WriteLine($"{obj}")
                , ex => Console.WriteLine($"Error: {ex.Message}")
                , () => Console.WriteLine("Completed")
                );
        }

        private IEnumerable<int> EnumerableEventSequence()
        {
            for (int i = 0; i < 5; i++)
            {
                Thread.Sleep(TimeSpan.FromSeconds(0.5D));
                yield return i;
            }
        }
    }
}
