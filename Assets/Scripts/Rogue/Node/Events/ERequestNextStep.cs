using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Running
{
    //语义上，这里是请求下一条对白的事件
    //但实际只是在DialogueState下请求下一个指令
    //而不在乎下一条是Dialouge，Choice或是别的什么

    //这就要求一个Step只能有一个阻塞Component
    //否则，多个阻塞Component会同时订阅这个事件
    public class ERequestNextStep { }
}