using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
[CreateAssetMenu(fileName ="CGList",menuName ="ConfigInfo/CG")]
public class CGList : ScriptableObject
{
    [SerializeField]
    public Sprite Placeholder;
    [SerializeField]
    public List<Sprite> CG=new List<Sprite>();
    public void OnValidate()
    {
        if (Placeholder == null)
        {
            Debug.LogError("The ScriptObject Placeholder is null");
        }
    }
}