using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Running
{
    public interface IComponent
    {
        void Enter(Node node);
        void Exit(Node node);
    }
}