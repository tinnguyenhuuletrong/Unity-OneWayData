using System;
using DataStoreNamespace;

namespace DispatcherNamespace
{
    public class ActionLoginArgument : BaseActionArgument
    {
        public string Username { get; set; }
        public string Pass { get; set; }
    }

    internal class LogicModuleAuthenticate : AbstractLogicModule
    {
        internal LogicModuleAuthenticate()
        {
        }

        internal override void LateUpdate(float dt)
        {

        }

        internal override void OnAction(Dispatcher.InternalLogicEvent ev, Action next)
        {
            switch (ev.EventType)
            {
                case EDispatcherEvents.Login:
                    {
                        ActionLoginArgument arg = ev.Args as ActionLoginArgument;
                        if(arg == null)
                        {
                            ev.ErrorCode = 400;
                            ev.ResultData = "Missing arguments";
                            break;
                        }
                        
                        if(arg.Username == "admin" && arg.Pass == "admin") 
                        {
                            ev.ErrorCode = 0;
                            ev.ResultData = "Ok!";
                        }
                        else 
                        {
                            ev.ErrorCode = 404;
                            ev.ResultData = "Wrong Username or Pass";
                        }
                    }
                    break;
            }
            next();
        }
    }
}