using Audio;
using UnityEngine;

namespace Setting.Config
{
    //该类中的字段用于开发者编辑器中调整
    //游戏设置系统不向外暴露
    [CreateAssetMenu(fileName = "DeveloperSettingConfig", menuName = "ConfigInfo/Setting/DeveloperSetting")]
    public class DeveloperSettings : Setting
    {
        [Min(1)]
        public int CameraMovingSpeed;
        [SerializeField]
        public Vector2 NodeSpaceing = new Vector2(25, 15);
        [SerializeField]
        public Vector3 Offset = new Vector3(10, 0, -10);
        [Min(1)]
        public int CGCount;
        [Min(1)]
        public int CountPerPage;
        //该属性用于懒加载
        [SerializeField]
        public string AssetBundlesRoot;  //AssetBundles
        //全局服务，全局预加载
        [SerializeField]
        public string AudioDefinitionCatalogDire;  //null  所有AssetBundle均位于<platform>\ 路径下
        [SerializeField]
        public string AudioDefinitionCatalogBundle;
        [SerializeField]
        public string AudioDefinitionCatalogAsset;
        [SerializeField]
        public AudioID ButtonSE;
        [SerializeField]
        public string UserSettingDire;
        [SerializeField]
        public string UserSettingsFileName;
        [SerializeField]
        public string SavedInfoDire;
        [SerializeField]
        public string SavedInfoName;
        [SerializeField]
        public string AutoSavedInfoName;
        public int MaxRogueLayer;
        public int MaxSaveSlot;
    }
}