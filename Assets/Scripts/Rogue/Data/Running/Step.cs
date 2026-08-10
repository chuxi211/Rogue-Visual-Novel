using Config;
using System;
using System.Collections.Generic;

namespace Running
{
    public class Step 
    {
        public StepID ID { get; private set; }
        public Dictionary<Type, IComponent> ComponentsMap { get; private set; } = new Dictionary<Type, IComponent>();
        public List<IComponent> ComponentsList { get; private set; } = new List<IComponent>();
        public Step(StepID id)
        {
            this.ID = id;
        } 


        public void AddComponent(IComponent component)
        {
            Type type = component.GetType();
            if (!ComponentsMap.ContainsKey(type))
            {
                ComponentsMap.Add(type, component);
            }
            ComponentsList.Add(component);
        }
        public bool TryGetComponent<T>(out T component) where T : class, IComponent
        {
            if (ComponentsMap.TryGetValue(typeof(T), out IComponent value))
            {
                component = value as T;
                return component != null;
            }
            component = null;
            return false;
        }
        public IComponent GetComponent(Type type)
        {
            if (ComponentsMap.TryGetValue(type, out IComponent value))
            {
                return value;
            }
            return null;
        }
    }

}