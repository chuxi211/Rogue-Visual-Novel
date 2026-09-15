using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Input;
/// <summary>
/// NodeActor is a MonoBehaviour that represents a node in the game world. It binds to a Running.
/// Node instance and manages its associated click interactions through INodeClick components.
/// </summary>
public sealed class NodeActor : MonoBehaviour
{
    public Running.Node NodeRunning { get;private set; }
    private void Awake()
    {
        List<INodeClick> nodeClicks = gameObject.GetComponentsInChildren<INodeClick>().ToList();
        nodeClicks.ForEach(click =>
        {
            click.BindActor(this);
        });
    }
    public void BindNode(Running.Node node)
    {
        NodeRunning = node;
        if (NodeRunning == null)
        {
            Debug.LogError("NodeActor BindNode NodeRunning is null");
        }
    }
}
