using Audio;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Running
{
    public class ComponentFactory
    {
        private readonly Dictionary<Type,Func<Config.NodeBehaviour,IComponent>> Builders = 
            new Dictionary<Type, Func<Config.NodeBehaviour,IComponent>>();
        public void Register<TBehaviour>(Func<TBehaviour, IComponent> builder) where TBehaviour : Config.NodeBehaviour
        {
            Builders[typeof(TBehaviour)] = (behaviour)=>builder((TBehaviour)behaviour);
        }
        public IComponent BuildComponent(Config.NodeBehaviour behaviour)
        {
            if (behaviour == null)
            {
                Debug.LogError("BuildComponent failed: behaviour is null.");
                return null;
            }

            Type behaviourType = behaviour.GetType();

            if (!Builders.TryGetValue( behaviourType,out Func<Config.NodeBehaviour, IComponent> builder))
            {
                Debug.LogError(
                    $"BuildComponent failed: no builder registered for " +
                    $"{behaviourType.Name}.");

                return null;
            }

            return builder(behaviour);
        }
        public List<IComponent> BuildComponents(List<Config.NodeBehaviour> behaviours)
        {
            List<IComponent> components = new List<IComponent>();
            behaviours.ForEach(bahviour =>
            {
                IComponent component = BuildComponent(bahviour);
                if (component != null)
                {
                    components.Add(component);
                }
            });
            return components;
        }
    }
}