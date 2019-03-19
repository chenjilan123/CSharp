using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CSharp
{
    public class Crawling
    {
        public void Run()
        {
            CreateLinks();
            var t = RunProgram();
            t.Wait();


            //Console.ReadLine();
        }

        static async Task RunProgram()
        {
            var bag = new ConcurrentBag<CrawlingTask>();
            string[] urls =
            {
                "http://microsoft.com/",
                "http://google.com/",
                "http://facebook.com/",
                "http://twitter.com/",
            };
            var crawlers = new Task[4];
            for (int i = 0; i < 4; i++)
            {
                string crawlerName = $"Crawler {i + 1}";
                bag.Add(new CrawlingTask { UrlToCrawl = urls[i], ProducerName = crawlerName });
                crawlers[i] = Task.Run(() => Crawl(bag, crawlerName));
            }
            await Task.WhenAll(crawlers);
        }

        static async Task Crawl(ConcurrentBag<CrawlingTask> bag, string crawlerName)
        {
            CrawlingTask task;
            while(bag.TryTake(out task))
            {
                IEnumerable<string> urls = await GetLinksFromContent(task);
                if (urls != null)
                {
                    foreach (var url in urls)
                    {
                        var t = new CrawlingTask
                        {
                            UrlToCrawl = url,
                            ProducerName = crawlerName,
                        };
                        bag.Add(t);
                    }
                }
                Console.WriteLine($"Indexing url {task.UrlToCrawl} posted by {task.ProducerName} is completed by {crawlerName}");
            }
        }

        static Dictionary<string, string[]> _contentEnulation = new Dictionary<string, string[]>();

        static async Task<IEnumerable<string>> GetLinksFromContent(CrawlingTask task)
        {
            //模拟网络I/O
            await GetRandomDelay();
            if (_contentEnulation.ContainsKey(task.UrlToCrawl))
            {
                return _contentEnulation[task.UrlToCrawl];
            }
            return null;
        }

        static void CreateLinks()
        {
            //microsoft
            _contentEnulation["http://microsoft.com/"] = new[]
            {
                "http://microsoft.com/a.html",
                "http://microsoft.com/b.html",
            };
            _contentEnulation["http://microsoft.com/a.html"] = new[]
            {
                "http://microsoft.com/c.html",
                "http://microsoft.com/d.html",
            };
            _contentEnulation["http://microsoft.com/b.html"] = new[]
            {
                "http://microsoft.com/e.html",
            };
            //google
            _contentEnulation["http://google.com/"] = new[]
            {
                "http://google.com/a.html",
                "http://google.com/b.html",
            };
            _contentEnulation["http://google.com/a.html"] = new[]
            {
                "http://google.com/c.html",
                "http://google.com/d.html",
            };
            _contentEnulation["http://google.com/b.html"] = new[]
            {
                "http://google.com/e.html",
                "http://google.com/f.html",
            };
            _contentEnulation["http://google.com/c.html"] = new[]
            {
                "http://google.com/h.html",
                "http://google.com/i.html",
            };
            //facebook
            _contentEnulation["http://facebook.com/"] = new[]
            {
                "http://facebook.com/a.html",
                "http://facebook.com/b.html",
            };
            _contentEnulation["http://facebook.com/a.html"] = new[]
            {
                "http://facebook.com/c.html",
                "http://facebook.com/d.html",
            };
            _contentEnulation["http://facebook.com/b.html"] = new[]
            {
                "http://facebook.com/e.html",
            };
            //twitter
            _contentEnulation["http://twitter.com/"] = new[]
            {
                "http://twitter.com/a.html",
                "http://twitter.com/b.html",
            };
            _contentEnulation["http://twitter.com/a.html"] = new[]
            {
                "http://twitter.com/c.html",
                "http://twitter.com/d.html",
            };
            _contentEnulation["http://twitter.com/b.html"] = new[]
            {
                "http://twitter.com/e.html",
            };
            _contentEnulation["http://twitter.com/c.html"] = new[]
            {
                "http://twitter.com/f.html",
                "http://twitter.com/g.html",
            };
            _contentEnulation["http://twitter.com/d.html"] = new[]
            {
                "http://twitter.com/h.html",
            };
            _contentEnulation["http://twitter.com/e.html"] = new[]
            {
                "http://twitter.com/i.html",
            };
        }

        static Task GetRandomDelay()
        {
            var delay = new Random(DateTime.Now.Millisecond).Next(150, 200);
            return Task.Delay(delay);
        }
    }

    class CrawlingTask
    {
        public string UrlToCrawl { get; set; }
        public string ProducerName { get; set; }
    }
}
