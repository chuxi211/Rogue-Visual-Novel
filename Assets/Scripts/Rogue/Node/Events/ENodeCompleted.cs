namespace Running
{
    public class ENodeCompleted { }
    public class ENodeFailed
    {
        public string NodeID { get; }
        public NodeErrorCode ErrorCode { get; }

        public ENodeFailed(string nodeID, NodeErrorCode errorCode)
        {
            NodeID = nodeID;
            ErrorCode = errorCode;
        }
    }
}
