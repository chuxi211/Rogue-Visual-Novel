using Config;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Audio;

namespace Running
{
    public class OtherSetting:Setting
    {
        [Min(1)]
        public int CameraMovingSpeed;
        public Vector2 NodeSpaceing = new Vector2(25, 15);
        public Vector3 Offset = new Vector3(10, 0, -10);
        public int CGCount;
        public int CountPerPage;
        public string AssetBundlesRoot;
        public AudioID ButtonSE;
        public OtherSetting(DeveloperSettings config)
        {
            CameraMovingSpeed = config.CameraMovingSpeed;
            NodeSpaceing = config.NodeSpaceing;
            Offset = config.Offset;
            CGCount = config.CGCount;
            CountPerPage = config.CountPerPage;
            AssetBundlesRoot = config.AssetBundlesRoot;
            ButtonSE = config.ButtonSE;
        }
    }
}