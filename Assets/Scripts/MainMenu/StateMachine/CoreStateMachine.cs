using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;
namespace StateMachine.Core
{
    public class CoreStateMachine : StateMachine<CoreState>
    {
        public CoreStateMachine()
        {
            Context=new CoreContext();
        }

        public CoreContext Context { get; private set; }
    }
}
