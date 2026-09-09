using Audio;
using Config;
using UnityEngine;

namespace Running
{
    public class BGMComponent : IComponent
    {
        private readonly BGMBehaviour Behaviour;
        private readonly IMusicPlayback MusicPlayback;
        public BGMComponent(BGMBehaviour behaviour,  IMusicPlayback musicPlayback)
        {
            Behaviour = behaviour;
            MusicPlayback = musicPlayback;
        }

        public void Enter()
        {
            MusicPlayback.PlayMusic(Behaviour.AudioID);
        }

        public void ExitOnNodeEnd()
        {
            MusicPlayback.StopMusic();
        }

        public void ExitOnStepEnd()
        {
            Debug.Log("BGM Couldn't Exit On Step End.");
        }
    }
}