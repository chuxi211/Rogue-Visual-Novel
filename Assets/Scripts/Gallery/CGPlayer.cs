using Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

public class CGPlayer : MonoBehaviour
{
    [SerializeField]
    private CGContainer Template;
    private List<CGContainer> CGContainers=new List<CGContainer>();
    private CGList CGs;
    private CGLoader CGLoader;
    //采用Grid Layout Group布局
    public void InitCGLoader(CGLoader loader)
    {
        CGLoader = loader;
    }
    public void Show()
    {
        if(CGs == null)
        {
            Debug.Log("CGs is null, try to load CGs");
            TryGetCGs();
        }
        if (CGContainers.Count == 0)
        {
            CreatBox();
        }
        SetAll();
        foreach (CGContainer c in CGContainers)
        {
            c.gameObject.SetActive(true);
        }
    }
    public void Hide()
    {
        foreach(CGContainer c in CGContainers)
        {
            c.gameObject.SetActive(false);
        }
    }
    private void SetAll()
    {
        Debug.Log($"The Count of CG : {CGs.CG.Count}");
        CGContainers.ForEach(container =>
        {
            if (CGs.CG[CGContainers.IndexOf(container)] == null)
            {
                container.Set(CGs.Placeholder);
            }
            else
            {
                container.Set(CGs.CG[CGContainers.IndexOf(container)]);
            }
        });
    }

    private void CreatBox()
    {
        if (Template == null)
        {
            Debug.LogError("Template is null!");
            return;
        }
        for (short i = 0; i < CGs.CG.Count; i++)
        {
            GameObject box = Instantiate(Template.gameObject, Template.transform.parent); 
            CGContainer cgBox = box.GetComponent<CGContainer>();
            CGContainers.Add(cgBox);
            Debug.Log($"CGContainer {i} created.Position: {box.transform.position}");
        }
    }
    private void ClearBox()
    {
        if (CGContainers == null)
        {
            Debug.Log("CGContainers is null");
            return;
        }
        foreach (var box in CGContainers)
        {
            Destroy(box.gameObject);
        }
        CGContainers.Clear();
    }
    private bool TryGetCGs()
    {
        return CGLoader.TryLoad("cglist", "CGList", out CGs);
    }
}
