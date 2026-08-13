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
        private readonly Dictionary<CharacterPlaybackHandle , CharacterContainer> ActiveContainers=
            new Dictionary<CharacterPlaybackHandle, CharacterContainer> ();
        //显式维护
        private int NextHandleValue = 1;
        public List<CharacterContainer> Containers = new List<CharacterContainer>();
        
        public bool TryShow(Sprite visual,Vector2 normalizedpos,out CharacterPlaybackHandle handle)
        {
            handle = default;
            CharacterContainer Container = GetOrCreateContainer();
            if (Container == null)
            {
                Debug.LogError(
                    "CharacterPlayer: Failed to get CharacterContainer.");
                return false;
            }
            handle = new CharacterPlaybackHandle( NextHandleValue);
            NextHandleValue++;
            ActiveContainers.Add(handle,Container);
            Container.SetSprite(visual);
            Container.SetPosition(normalizedpos);
            Container.gameObject.SetActive(true);
            return true;
        }
        public bool TryUpdate(CharacterPlaybackHandle Handle,Sprite visual,Vector2 normalizedpos)
        {
            if (!Handle.IsValid)
            {
                Debug.LogError(
                    "CharacterPlayer: Handle is invalid.");
                return false;
            }

            if (!ActiveContainers.TryGetValue(
                    Handle,
                    out CharacterContainer Container))
            {
                Debug.LogError(
                    $"CharacterPlayer: Handle {Handle.Value} was not found.");
                return false;
            }

            if (Container == null)
            {
                Debug.LogError(
                    "CharacterPlayer: CharacterContainer is null.");

                ActiveContainers.Remove(Handle);
                return false;
            }

            Container.SetSprite(visual);
            Container.SetPosition(normalizedpos);
            return true;
        }
        public bool TryHide(CharacterPlaybackHandle handle)
        {
            if (!ActiveContainers.TryGetValue(handle, out CharacterContainer Container))
            {
                return false;
            }

            Container.gameObject.SetActive(false);
            return true;
        }
        public bool TryRelease(CharacterPlaybackHandle handle)
        {
            if(!ActiveContainers.TryGetValue(handle,out CharacterContainer container))
            {
                return false;
            }
            ActiveContainers.Remove(handle);
            if(container != null)
            {
                container.SetSprite(null);
                container.gameObject.SetActive(false);
            }
            return true;
        }
        private CharacterContainer GetOrCreateContainer()
        {
            foreach(var container in ActiveContainers.Values)
            {
                if (container == null) continue;
                return container;
            }
            if (Template == null)
            {
                Debug.LogError( "CharacterPlayer: Template is null.");
                return null;
            }
            CharacterContainer NewContainer = Instantiate(Template, transform);
            Containers.Add(NewContainer);
            return NewContainer;
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