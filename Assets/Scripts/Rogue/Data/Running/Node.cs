using Config;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Running
{
    public class Node
    {
        public readonly NodePosition Position;
        public readonly NodeAdvanceType AdvanceType;
        public List<Step> Steps { get; private set; } = new List<Step>();
        public Dictionary<StepID, Step> StepsMap { get; private set; } = new Dictionary<StepID, Step>();
        public Queue<IComponent> EnteredComponents { get; private set; } = new Queue<IComponent>();
        private readonly List<Edge> OutgoingEdges = new List<Edge>();
        private readonly List<Edge> IncomingEdges = new List<Edge>();
        //节点的完成状态，表示该节点是否已经完成了所有的步骤和逻辑处理
        //默认为false，表示节点尚未完成。
        //为true时该节点不可重复进入

        //节点的可达性IsReachable属性表示该节点是否可以被访问或到达。它通常用于控制游戏中的路径或流程，确保玩家只能访问特定的节点。
        //该属性依赖于前一个节点的OutgoingEdge是否指向当前节点
        //因此该属性不由Node本身直接设置，而由MapProgression的TryMoveTo方法来判断。
        public bool IsCompleted { get; private set; } = false;
        public StateMachine.Node.NodeStateMachine StateMachine { get; private set; }
        public Node(NodePosition position,NodeAdvanceType advanceType)
        {
            StateMachine = new StateMachine.Node.NodeStateMachine(this);
            Position = position;
            AdvanceType = advanceType;
        }
        public IReadOnlyList<Edge> GetOutgoingEdges()
        {
            return OutgoingEdges;
        }
        public IReadOnlyList<Edge> GetIncomingEdges()
        {
            return IncomingEdges;
        }
        internal void AddOutgoingEdge(Edge edge)
        {
            if(edge!=null)
                OutgoingEdges.Add(edge);
        } 
        internal void AddIncomingEdge(Edge edge)
        {
            if(edge!=null)
                IncomingEdges.Add(edge);
        }
        public void MarkCompleted()
        {
            IsCompleted = true;
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
        public void AddEnteredComponent(IComponent component)
        {
            EnteredComponents.Enqueue(component);
        }
    }
}