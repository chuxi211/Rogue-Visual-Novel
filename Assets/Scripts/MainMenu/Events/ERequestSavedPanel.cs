using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ERequestSavedPanel
{
    public ModeOfEnterSaveMode ModeOfEnterSaveMode { get; private set; }
    public ERequestSavedPanel(ModeOfEnterSaveMode modeOfEnterSaveMode)
    {
        ModeOfEnterSaveMode = modeOfEnterSaveMode;
    }
}