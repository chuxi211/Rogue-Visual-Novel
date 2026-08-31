using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
[CreateAssetMenu(fileName ="CGList",menuName ="ConfigInfo/CG")]
public class CGList : ScriptableObject
{
    [SerializeField]
    private List<Sprite> CG=new List<Sprite>();
}