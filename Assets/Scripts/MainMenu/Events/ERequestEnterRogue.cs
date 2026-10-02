public class ERequestEnterRogue
{
    public EnterRogueMode EnterRogueMode;
    public int LoadIndex;
    public ERequestEnterRogue(EnterRogueMode enterRogueMode, int loadIndex=-1)
    {
        EnterRogueMode = enterRogueMode;
        LoadIndex = loadIndex;
    }
}
public enum EnterRogueMode
{
    New,
    Load,
}