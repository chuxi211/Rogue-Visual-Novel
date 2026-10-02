using Config;
using System;
using Unity.VisualScripting;

namespace Running
{
    [Serializable]
    public class SavedInfo
    {
        public MapID CurrentMapID;
        public string CurrentNodeID;
        public bool IsNodeCompleted;
        public int CurrentLayer;
        public NodePosition Position;
    }
}