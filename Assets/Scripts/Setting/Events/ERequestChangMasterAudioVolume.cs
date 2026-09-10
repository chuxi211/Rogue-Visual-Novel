namespace Setting.Events
{
    public class ERequestChangeMasterAudioVolume
    {
        public float ChangedVolume { get; set; }
        public ERequestChangeMasterAudioVolume(float changedVolume)
        {
            ChangedVolume = changedVolume;
        }
    }
}