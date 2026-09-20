using System.Collections.Generic;
using UnityEngine;

namespace Running
{
    public class StepBuilder
    {
        private ComponentFactory ComponentFactory;
        public void DependencesInject(ComponentFactory componentFactory)
        {
            this.ComponentFactory = componentFactory;
            if (ComponentFactory == null)
            {
                Debug.LogError("ComponentFactory is null");
            }
        }
        public Step BuildStep(Config.Step configStep)
        {
            if (configStep == null)
            {
                Debug.LogError("ConfigStep is null");
                return null;
            }
            Step step = new Step(configStep.ID, configStep.NextID);
            configStep.Behaviours.ForEach(bahaviour =>
            {
                IComponent component = ComponentFactory.BuildComponent(bahaviour);
                if (component != null)
                {
                    step.AddComponent(component);
                }
                else
                {
                    Debug.LogError($"StepBuilder: Failed to build component for behaviour {bahaviour.GetType().Name}");
                }
            });
            Debug.Log($"StepBuilder: Built step with {step.ComponentsList.Count} components.");
            return step;
        }
        public List<Step> BuildSteps(List<Config.Step> configSteps)
        {
            List<Step> steps = new List<Step>();
            configSteps.ForEach(configStep =>
            {
                Step step = BuildStep(configStep);
                if (step != null)
                {
                    steps.Add(step);
                }
                else
                {
                    Debug.LogError($"StepBuilder: Failed to build step for config {configStep.GetType().Name}");
                }
            });
            return steps;
        }
    }
}