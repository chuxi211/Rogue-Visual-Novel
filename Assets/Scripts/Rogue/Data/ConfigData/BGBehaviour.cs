using UnityEngine;

namespace Config
{
    [CreateAssetMenu(fileName = "BGBehaviour", menuName = "ConfigInfo/Rogue/NodeBehaviours/BGBehaviour")]
    public sealed class BGBehaviour : NodeBehaviour
    {
        public Sprite BG;
    }
}