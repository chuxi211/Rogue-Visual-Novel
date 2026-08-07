using Running;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;
namespace StateMachine.Node
{
    public class NodeStateMachine : StateMachine<NodeState>
    {
        public Running.Node Node { get; private set; }
        public Step CurrentStep { get; set; }
        public int CurrentStepIndex { get; set; } = 0;
        public NodeStateMachine(Running.Node node)
        {
            Node = node;
            if (Node == null)
            {
                Debug.LogError("Node is null");
            }
        }
        public bool TryStart()
        {
            if (Node.Steps.Count > 0)
            {
                CurrentStep = Node.Steps[CurrentStepIndex];
                SwitchState(new AdvanceState(this,CurrentStep));
                return true;
            }
            else
            {
                Debug.LogError("Node has no steps");
                return false;
            }
        }
        public void Advance()
        {
            int NextStepIndex = CurrentStepIndex + 1;

            if (NextStepIndex >= Node.Steps.Count)
            {
                SwitchState(new ExitState(this));
                return;
            }

            CurrentStepIndex = NextStepIndex;
            CurrentStep = Node.Steps[CurrentStepIndex];
            SwitchState(new AdvanceState(this, CurrentStep));
        }
        public void Complete()
        {
            CurrentStep = null;
            CurrentStepIndex = 0;
        }
    }
}