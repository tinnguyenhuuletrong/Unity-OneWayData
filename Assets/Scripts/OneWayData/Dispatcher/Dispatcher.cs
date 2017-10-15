using System;
using System.Collections.Generic;
// using DispatcherNamespace.LogicModule;

namespace DispatcherNamespace
{
    public class Dispatcher : Singleton<Dispatcher>
    {
        internal class InternalLogicEvent
        {
            public int ErrorCode { get; set; }
            public object ResultData { get; set; }

            internal BaseActionArgument Args { get; private set; }
            internal EDispatcherEvents EventType { get; private set; }
            internal Action<int, object> OnDone;
            
            internal IEnumerator<AbstractLogicModule> Next;

            public InternalLogicEvent(EDispatcherEvents type, BaseActionArgument args = null, Action<int, object> onDone = null)
            {
                EventType = type;
                Args = args;
                OnDone = onDone;
            }

            internal void MoveNext()
            {
                if (Next.MoveNext() && Next.Current != null)
                {
                    Next.Current.OnAction(this, MoveNext);
                }
                else
                {
                    if (OnDone != null)
                        OnDone(ErrorCode, ResultData);
                }
            }
        }

        public Dispatcher()
        {
            mModules = new List<AbstractLogicModule>();
            Init();
        }
        private void Init()
        {
            mModules.Add(new LogicModuleAuthenticate());
        }

        public void LateUpdate(float dt) 
        {
            int count = mModules.Count;
            for (int i = 0; i < count; i++)
            {
                mModules[i].LateUpdate(dt);
            }
        }

        public void PostAction(EDispatcherEvents type, BaseActionArgument arg = null, Action<int, object> onDone = null)
        {
            var worker = new InternalLogicEvent(type, arg, onDone);
            worker.Next = mModules.GetEnumerator();
            worker.MoveNext();
        }

        List<AbstractLogicModule> mModules;
    }

    public class BaseActionArgument 
    {

    }

    public enum EDispatcherEvents
    {
        Login
    }
}