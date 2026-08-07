using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface INodeClick:IClick
{
    void BindActor(NodeActor nodeActor);
}