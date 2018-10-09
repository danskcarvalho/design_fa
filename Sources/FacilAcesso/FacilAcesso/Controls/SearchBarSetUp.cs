using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FacilAcesso
{
    public static class SearchBarSetUp
    {
        class EntryTextChanged
        {
            public Action Action { get; set; }

            public async void OnTextChanged(object sender, TextChangedEventArgs e)
            {
                var myEntry = sender as Entry;
                cancellationTokens.TryGetValue(myEntry, out CancellationTokenSource tokenSource);

                if (tokenSource != null)
                {
                    tokenSource.Cancel();
                    cancellationTokens.Remove(myEntry);
                }
                try
                {
                    tokenSource = new CancellationTokenSource();
                    cancellationTokens.Add(myEntry, tokenSource);

                    await Task.Delay(500, tokenSource.Token);

                    tokenSource = null;
                    cancellationTokens.Remove(myEntry);

                    Action();
                }
                catch (TaskCanceledException) { }
            }
        }
        static Dictionary<Entry, CancellationTokenSource> cancellationTokens = new Dictionary<Entry, CancellationTokenSource>();
        static Dictionary<Entry, EntryTextChanged> alreadySetUp = new Dictionary<Entry, EntryTextChanged>();

        public static void SetUp(this Entry entry, Action action)
        {
            if (alreadySetUp.ContainsKey(entry))
                entry.TextChanged -= alreadySetUp[entry].OnTextChanged;

            var evt = new EntryTextChanged() { Action = action };
            entry.TextChanged += evt.OnTextChanged;
            alreadySetUp[entry] = evt;
        }
    }

    public static class SearchBarSetUp2
    {
        class EntryTextChanged
        {
            public Action Action { get; set; }

            public async void OnTextChanged(object sender, TextChangedEventArgs e)
            {
                var myEntry = sender as SearchBar;
                cancellationTokens.TryGetValue(myEntry, out CancellationTokenSource tokenSource);

                if (tokenSource != null)
                {
                    tokenSource.Cancel();
                    cancellationTokens.Remove(myEntry);
                }
                try
                {
                    tokenSource = new CancellationTokenSource();
                    cancellationTokens.Add(myEntry, tokenSource);

                    await Task.Delay(500, tokenSource.Token);

                    tokenSource = null;
                    cancellationTokens.Remove(myEntry);

                    Action();
                }
                catch (TaskCanceledException) { }
            }
        }
        static Dictionary<SearchBar, CancellationTokenSource> cancellationTokens = new Dictionary<SearchBar, CancellationTokenSource>();
        static Dictionary<SearchBar, EntryTextChanged> alreadySetUp = new Dictionary<SearchBar, EntryTextChanged>();

        public static void SetUp(this SearchBar entry, Action action)
        {
            if (alreadySetUp.ContainsKey(entry))
                entry.TextChanged -= alreadySetUp[entry].OnTextChanged;

            var evt = new EntryTextChanged() { Action = action };
            entry.TextChanged += evt.OnTextChanged;
            alreadySetUp[entry] = evt;
        }
    }
}
