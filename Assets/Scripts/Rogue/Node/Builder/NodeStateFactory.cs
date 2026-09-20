using StateMachine.Node;
using System;
using System.Collections.Generic;
using UnityEngine;
namespace Running
{
    /// <summary>
    /// 根据Component创建State的工厂类
    /// </summary>
    public class NodeStateFactory
    {
        private readonly Dictionary<Type, Func<NodeStateMachine, IComponent, NodeState>> StateCreators =
            new Dictionary<Type, Func<NodeStateMachine, IComponent, NodeState>>();
        public NodeStateFactory() { }
        public void RegisterState<TComponent>(Func<NodeStateMachine, TComponent, NodeState> creator) where TComponent : class, IComponent
        {
            StateCreators[typeof(TComponent)] = (nodeStateMachine, component) =>
            {
                TComponent typedComponent = component as TComponent;
                return creator(nodeStateMachine, typedComponent);
            };
        }
        public void BuildState(IComponent component, Node node)
        {
            if (component == null)
            {
                Debug.LogError($"BuildState failed: component is null.");
                return;
            }
            Type componentType = component.GetType();
            if (!StateCreators.TryGetValue(componentType, out Func<NodeStateMachine, IComponent, NodeState> creator))
            {
                Debug.LogError($"BuildState failed: no state creator registered for {componentType.Name}.");
                return;
            }
            NodeState state = creator(node.StateMachine, component);
            if (state != null)
            {
                //node.StateMachine.AddConsumeState(state);
            }
        }
        //public void BuildStates(Running.Node node)
        //{
        //    if(node== null)
        //    {
        //        Debug.LogError($"BuildState failed: node is null.");
        //        return;
        //    }
        //    node.ComponentsList.ForEach(component =>
        //    {
        //        if(component == null)
        //        {
        //            Debug.LogError($"BuildState failed: component is null.");
        //            return;
        //        }
        //        BuildState(component, node);
        //    });
        //}
    }
}