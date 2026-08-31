using Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGPlayer : MonoBehaviour
{
    [SerializeField]
    private CGContainer Template;
    private List<CGContainer> CGContainers=new List<CGContainer>();
    private int Count;
    private int SlotsPerPage;
    private int CurrentPageIndex;
    private int TotalPages { get=>Count/SlotsPerPage;}
    private int VisualIndex;
    private int TrueIndex;

    public void InitAttribute(int count,int countPerPage)
    {
        Count = count;
        SlotsPerPage = countPerPage;
    }
    private void Refresh()
    {
        //for (int i = 0; i < SlotsPerPage; i++)
        //{
        //    TrueIndex = VisualIndexToTrueIndex(CurrentPageIndex, i);
        //    if (TrueIndex < CGs.CGs.Count)
        //    {
        //        CGContainers[i].SetCG(CGs.CGs[TrueIndex]);
        //        Debug.Log($"TrueIndex:{TrueIndex}");
        //    }
        //    else
        //    {
        //        CGContainers[i].SetCG(null);
        //    }
        //}
    }
    public int VisualIndexToTrueIndex(int currentPage, int Vindex)
    {
        return (currentPage * SlotsPerPage + Vindex);
    }
    public int TrueIndexToVisualIndex(int Tindex)
    {
        return (Tindex % SlotsPerPage);
    }
    public void NextPage(ERequestNextPage e)
    {
        if (CurrentPageIndex < TotalPages - 1)
        {
            CurrentPageIndex++;
        }
        Refresh();
    }
    public void PrevPage(ERequestPrevPage e)
    {
        if (CurrentPageIndex > 0)
        {
            CurrentPageIndex--;
        }
        Refresh();
    }

    private void CreatBox()
    {
        if (Template == null)
        {
            Debug.LogError("Template is null!");
            return;
        }
        for (short i = 0; i < SlotsPerPage; i++)
        {
            GameObject box = Instantiate(Template.gameObject, transform); 
            CGContainer cgBox = box.GetComponent<CGContainer>();
            CGContainers.Add(cgBox);
        }
        Refresh();
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

}
