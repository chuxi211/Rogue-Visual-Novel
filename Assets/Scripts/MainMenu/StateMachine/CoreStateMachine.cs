using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;
namespace StateMachine.Core
{
    public class CoreStateMachine : StateMachine<CoreState>
    {
        public CoreStateMachine(CoreContext context)
        {
            if (context == null)
            {
                Debug.LogError(
                    "CoreStateMachine initialization failed: " +
                    "CoreContext is null.");
                return;
            }

            Context = context;
        }

        public CoreContext Context { get; private set; }
    }
}
