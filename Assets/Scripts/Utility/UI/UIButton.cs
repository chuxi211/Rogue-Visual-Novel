using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Utility
{
    public class UIButton<T> : MonoBehaviour where T: new()
    {
        public void OnClick()
        {
            EventBus.Publish(new T());
        }
    }

}