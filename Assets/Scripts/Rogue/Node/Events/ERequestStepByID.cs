using Config;

namespace Running
{
    public class ERequestStepByID
    {
        public readonly StepID NextID;
        public ERequestStepByID(StepID nextID)
        {
            NextID = nextID;
            //if (NextID == null)
            //{
            //    Debug.LogError($"ERequestStepByID : NextID is null");
            //}
        }
    }
}