namespace Running
{
    public class LayerProgression
    {
        public int CurrentLayer { get; private set; }
        public int MaxLayer { get; private set; }
        public LayerProgression(int maxLayer)
        {
            MaxLayer = maxLayer;
        }
        public void SetLayer(int layer)
        {
            CurrentLayer = layer;
        }
        public void AddLayer()
        {
            CurrentLayer++;
        }
    }
}