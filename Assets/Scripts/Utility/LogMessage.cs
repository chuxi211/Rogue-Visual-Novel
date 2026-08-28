public class LogMessage
{
    public MessageType MessageType;
    public SourceType SourceType;
    public System.Object Source;
    public string Message;
}
public enum MessageType
{
    Normal,
    Waring,
    Error,
    FatalError,
}
public enum SourceType
{
    Success,
    Fail,
    Unknow
}