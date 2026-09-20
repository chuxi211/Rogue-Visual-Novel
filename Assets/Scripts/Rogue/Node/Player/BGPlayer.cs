using UnityEngine;
using UnityEngine.UI;

namespace Running
{
    //Set一次 跨Step持续存在
    //若没有显式调用Set进行切换，或Node未Completed，则持续存在
    //NodeCompleted时清除
    public sealed class BGPlayer : MonoBehaviour
    {
        public Transform Container;
        public Image BG;
        public void Awake()
        {
            if (Container == null)
            {
                Container = GetComponentInChildren<Transform>();
            }
            if (BG == null)
            {
                BG = GetComponentInChildren<Image>();
            }
        }
        public void SetBG(Sprite sprite)
        {
            BG.sprite = sprite;
        }
        public void Clear()
        {
            BG.sprite = null;
        }
        public void Show()
        {
            Container.gameObject.SetActive(true);
        }
        public void Hide()
        {
            Container.gameObject.SetActive(false);
        }
    }
}