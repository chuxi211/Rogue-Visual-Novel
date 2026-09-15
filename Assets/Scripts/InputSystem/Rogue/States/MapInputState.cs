using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 接收地图输入的状态，如点击地图上的节点（进入节点）或点击地图上的空白区域（移动地图）
/// </summary>
namespace Input
{
    public class MapInputState : IInputState
    {
        public bool CanBuffer(InputCommand command)
        {
            return false;
        }

        public bool CanHandle(InputCommand command)
        {
            return command.Input!= null && command.Input is INodeClick;
        }

        public void Handle(InputCommand command)
        {
            Debug.Log("NodeInputState: Handle command");
            if(command?.Input is not INodeClick nodeclick)
            {
                Debug.LogError(
             "MapInputState cannot handle input because it is not INodeClick.");
                return;
            }
            nodeclick.Click();
        }
    }
}