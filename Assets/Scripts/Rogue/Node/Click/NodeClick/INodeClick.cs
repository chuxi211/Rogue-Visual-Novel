using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Input
{
    public interface INodeClick : IClick
    {
        void BindActor(NodeActor nodeActor);
    }
}