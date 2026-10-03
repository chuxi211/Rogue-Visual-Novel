using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESavedSlotRequestion
{
    public ModeOfEnterSaveMode Mode;
    public int SlotIndex;
    public ESavedSlotRequestion(ModeOfEnterSaveMode mode, int slotIndex)
    {
        Mode = mode;
        SlotIndex = slotIndex;
    }
}
