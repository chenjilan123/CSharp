using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CSharp.WpfTaskScheduler
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonSync_Click(object sender, RoutedEventArgs e)
        {
            ContentTextBlock.Text = string.Empty;
            try
            {
                //在UI线程队列中等待UI线程队列中的线程完成。
                //这将造成死锁。
                //string result = TaskMethod(TaskScheduler.FromCurrentSynchronizationContext()).Result;

                string result = TaskMethod().Result;
                ContentTextBlock.Text = result;
            }
            catch (Exception ex)
            {
                ContentTextBlock.Text = ex.InnerException.Message;
            }
        }

        private void ButtonAsync_Click(object sender, RoutedEventArgs e)
        {
            ContentTextBlock.Text = string.Empty;
            Mouse.OverrideCursor = Cursors.Wait;
            Task<string> task = TaskMethod();
            task.ContinueWith(t =>
            {
                ContentTextBlock.Text = t.Exception.InnerException.Message;
                Mouse.OverrideCursor = null;
            }, CancellationToken.None, TaskContinuationOptions.OnlyOnFaulted, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void ButtonAsyncOK_Click(object sender, RoutedEventArgs e)
        {
            ContentTextBlock.Text = string.Empty;
            Mouse.OverrideCursor = Cursors.Wait;
            Task<string> task = TaskMethod(TaskScheduler.FromCurrentSynchronizationContext());
            task.ContinueWith(t => Mouse.OverrideCursor = null
            , CancellationToken.None
            , TaskContinuationOptions.None
            , TaskScheduler.FromCurrentSynchronizationContext());

        }

        Task<string> TaskMethod()
        {
            return TaskMethod(TaskScheduler.Default);
        }

        Task<string> TaskMethod(TaskScheduler scheduler)
        {
            Task delay = Task.Delay(TimeSpan.FromSeconds(2));
            return delay.ContinueWith(t =>
            {
                string str = $"Thread id: {Thread.CurrentThread.ManagedThreadId}, Is Thread Pool: {Thread.CurrentThread.IsThreadPoolThread}";
                ContentTextBlock.Text = str;
                return str;
            }, scheduler);
        }
    }
}
