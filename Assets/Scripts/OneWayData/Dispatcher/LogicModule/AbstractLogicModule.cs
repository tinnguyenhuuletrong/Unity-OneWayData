using System;

namespace DispatcherNamespace
{
    internal abstract class AbstractLogicModule
    {
        internal abstract void LateUpdate(float dt);
        internal abstract void OnAction(Dispatcher.InternalLogicEvent ev, Action next);
    }
}