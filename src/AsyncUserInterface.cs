using System;
using System.Linq;
using Microsoft.AspNetCore.Blazor.Hosting;
using Wanderer;
using Wanderer.Actors;
using Wanderer.Rooms;

namespace src
{
    public class AsyncUserInterface : Wanderer.IUserinterface
    {
        public EventLog Log {get;set;} = new EventLog();

        public Action<string> ShowMessageDelegate {get;set;}

        public bool GetChoice<T>(string title, string body, out T chosen, params T[] options)
        {
            ShowMessage(title,body);

            chosen = options.First();

            return true;
        }

        public void ShowMessage(string title, string body)
        {

            WriteLine(title);

            if(!string.IsNullOrEmpty(body))
                WriteLine(body);

            
        }

        public virtual void ShowStats(IHasStats of)
        {
            WriteLine(of.ToString());
            WriteLine("Adjectives:" + string.Join(",",of.Adjectives));

            WriteLine("Stats:" + string.Join(Environment.NewLine,of.BaseStats.Select(s=>s.Key.ToString() + ':' + s.Value)));
                
            if(of is IActor a)
                WriteLine("Items:" + string.Join(",",a.Items));
            if(of is IRoom r)
                WriteLine("Items:" + string.Join(",",r.Items));
        }

        private void WriteLine(string s)
        {
            ShowMessageDelegate?.Invoke(s + Environment.NewLine);
        }
    }
}