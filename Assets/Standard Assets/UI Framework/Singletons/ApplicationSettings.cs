using UnityEngine;

public class ApplicationSettings : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        Application.SetStackTraceLogType(LogType.Exception, StackTraceLogType.None);
    }
}
