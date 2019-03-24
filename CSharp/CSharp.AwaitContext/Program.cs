using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CSharp.AwaitContext
{
    public class Program
    {
        private static Label _label;
        [STAThread]
        static void Main(string[] args)
        {
            var app = new Application();
            var win = new Window();
            var panel = new StackPanel();
            var button = new Button();
            _label = new Label();
            _label.FontSize = 32;
            _label.Height = 200;
            button.Height = 100;
            button.FontSize = 32;
            button.Content = new TextBlock
            {
                Text = "Start asynchronous operations"
            };
            button.Click += Click;
            panel.Children.Add(_label);
            panel.Children.Add(button);
            win.Content = panel;
            app.Run(win);

            Console.ReadLine();
        }

        private static async void Click(object sender, RoutedEventArgs e)
        {
            _label.Content = new TextBlock { Text = "Calculating..." };
            TimeSpan resultWithContext = await Test();
            TimeSpan resultWithNoContext = await TestNoContext();

            //这会导致回调函数，在线程池中执行，不在同步上下文中，产生异常。
            //TimeSpan resultWithNoContext = await TestNoContext().ConfigureAwait(false);
            
            var sb = new StringBuilder();
            sb.AppendLine($"With the context: {resultWithContext}");
            sb.AppendLine($"Without the context: {resultWithNoContext}");
            sb.AppendLine($"Radio: {resultWithContext.TotalMilliseconds / resultWithNoContext.TotalMilliseconds:0.00}");
            _label.Content = new TextBlock { Text = sb.ToString() };
        }

        private async static Task<TimeSpan> Test()
        {
            const int iterationsNumber = 100000;
            var sw = Stopwatch.StartNew();
            for (int i = 0; i < iterationsNumber; i++)
            {
                var t = Task.Run(() => { });
                await t;
            }
            sw.Stop();
            return sw.Elapsed;
        }
        private static async Task<TimeSpan> TestNoContext()
        {
            const int iterationsNumber = 100000;
            var sw = Stopwatch.StartNew();
            for (int i = 0; i < iterationsNumber; i++)
            {
                var t = Task.Run(() => { });
                await t.ConfigureAwait(continueOnCapturedContext: false);
            }
            sw.Stop();
            return sw.Elapsed;
        }
    }
}
