using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 接收地图输入的状态，如点击地图上的节点（进入节点）或点击地图上的空白区域（移动地图）
/// </summary>
public class MapInputState : IInputState
{
    public bool CanBuffer(InputCommand command)
    {
        return false;
    }

    public bool CanHandle(InputCommand command)
    {
        return command.Click!= null&&command.Click is INodeClick;
    }

    public void Handle(InputCommand command)
    {
        Debug.Log("NodeInputState: Handle command");
        command.Click.Click();
    }
}