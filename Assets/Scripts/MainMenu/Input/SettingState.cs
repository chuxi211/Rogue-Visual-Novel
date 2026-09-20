using Input;
using System;
using System.Linq;
using UnityEngine;
using System.Reflection;

namespace MainMenu.Input
{
    public class SettingState : IInputState
    {
        public bool CanBuffer(InputCommand command)
        {
            return false;
        }

        public bool CanHandle(InputCommand command)
        {
            return command?.Input is SettingInput;
        }

        public void Handle(InputCommand command)
        {
            if(command?.Input is not SettingInput input)
            {
                Debug.LogError(
             "SettingInputState cannot handle input because it is not SettingInput.");
                return;
            }
            input.Execute();
        }

        //public void Handle(InputCommand command)
        //{
        //    if(command?.Input  is not SettingInput input)
        //    {
        //        return;
        //    }
        //    //继承自SettingInput的实际运行时类型
        //    Type concreteType = input.GetType();
        //    //该类型只有一个interface
        //    //得到不是IInput同时继承自IInput(IInput可以用interfaceType赋值)的interfaces
        //    Type[] interfaces = concreteType.GetInterfaces().Where(
        //        interfaceType => interfaceType != typeof(IInput) &&
        //        typeof(IInput).IsAssignableFrom(interfaceType)).ToArray();
        //    if (interfaces.Length != 1)
        //    {
        //        Debug.LogError(
        //            $"{concreteType.Name} must implement exactly one " +
        //            $"concrete IInput interface, but found {interfaces.Length}.");

        //        return;
        //    }

        //    Type inputInterface = interfaces[0];

        //    MethodInfo[] methods = inputInterface.GetMethods().Where(
        //        method => method.ReturnType == typeof(void) &&
        //        method.GetParameters().Length == 0).ToArray();

        //    MethodInfo method = methods[0];
        //    InterfaceMapping mapping = concreteType.GetInterfaceMap(inputInterface);
        //    int methodIndex = Array.IndexOf(mapping.InterfaceMethods, method);

        //    if (methodIndex < 0)
        //    {
        //        Debug.LogError(
        //            $"Cannot resolve {inputInterface.Name}.{method.Name} " +
        //            $"on {concreteType.Name}.");
        //        return;
        //    }

        //    MethodInfo concreteMethod = mapping.TargetMethods[methodIndex];

        //    if (methods.Length != 1)
        //    {
        //        Debug.LogError(
        //            $"{inputInterface.Name} must define exactly one " +
        //            $"parameterless void method, but found {methods.Length}.");

        //        return;
        //    }

        //    try
        //    {
        //        methods[0].Invoke(input, Array.Empty<object>());
        //    }
        //    catch (TargetInvocationException exception)
        //    {
        //        Debug.LogException(exception.InnerException ?? exception);
        //    }
        //    catch (Exception exception)
        //    {
        //        Debug.LogException(exception);
        //    }
        //}
    }
}