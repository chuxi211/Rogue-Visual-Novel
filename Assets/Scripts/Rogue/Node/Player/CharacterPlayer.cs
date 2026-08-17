using Config;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;


/*     由于其他的Behaviour模型为：
 *     Behaviour-->IComponent-->Player
 *     
 *     Or
 *     
 *     Behaviour-->IComponent-->Player--|
 *                                      |--Container
 *                                      |--Container
 *     但CharacterBehaviour模型为：
 *     
 *     
 *     
 *     Behaviour-->IComponent-->Player--|
 *                                |     |--Container
 *     Behaviour-->IComponent-----|     |--Container
 *                                |     |
 *     Beahviour-->IComponent-----|     |--Container
 *     
 *     因此，从语义上，Set和Show方法的基本单位是Container而不是List<Container>，
 *     但又由于所有Container由一个Player统一管理，所以Player依旧要维护一整个Container
 *     
 *     
 *     或者直接修改Behaviour模型，使其与第二种保持一致。
 
 
 
 
 
 */
namespace Running
{
    public class CharacterPlayer : MonoBehaviour
    {
        public CharacterContainer Template;
        private Dictionary<string, CharacterContainer> Cache = new Dictionary<string, CharacterContainer>();
        public List<CharacterContainer> Containers { get; private set; } = new List<CharacterContainer>();
        
        public void SetCharacter(string characterID, Sprite visual, Vector2 normalizedPosition)
        {
            if (!Cache.TryGetValue(characterID, out var container))
            {
                container = Instantiate(Template, transform);
                Cache[characterID] = container;
                Containers.Add(container);
            }
            
            container.SetSprite(visual);
            container.SetPosition(normalizedPosition);
            container.gameObject.SetActive(true);
        }
        //Reference On NodeCompleted Or GameExit
        public void ClearAll()
        {
            Containers.ForEach(container =>
            {
                container.SetSprite(null);
                container.SetPosition(Vector2.zero);
            });
        }
        public void HideAll()
        {
            Containers.ForEach(container =>
            {
                container.gameObject.SetActive(false);
            });
        }
    }
}