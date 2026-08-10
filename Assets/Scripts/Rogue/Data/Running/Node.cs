using Config;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Running
{
    public class Node
    {
        public List<Step> Steps { get; private set; } = new List<Step>();
        public Dictionary<StepID, Step> StepsMap { get; private set; } = new Dictionary<StepID, Step>();
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
            if (step != null)
            {
                if (!StepsMap.ContainsKey(step.ID))
                {
                    StepsMap.Add(step.ID, step);
                    Steps.Add(step);
                }
                else
                {
                    Debug.LogError($"Step with ID {step.ID} already exists in the node.");
                }
            }
        }
    }
}