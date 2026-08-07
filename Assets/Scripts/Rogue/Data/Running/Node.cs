using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Running
{
    public class Node
    {
        public List<Step> Steps { get; private set; } = new List<Step>();
        public StateMachine.Node.NodeStateMachine StateMachine { get; private set; }
        public Node()
        {
            StateMachine = new StateMachine.Node.NodeStateMachine(this);
        }
        public bool TryStart()
        {
            return StateMachine.TryStart();
        }
        public void AddStep(Step step)
        {
            if(step!=null) Steps.Add(step);
        }
    }
}