using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Running
{
    public interface IPlayerProvider
    {
        
    }
    public interface IPlayerProvider<TPlayer>:IPlayerProvider where TPlayer : class
    {
        TPlayer GetPlayer();
    }
}