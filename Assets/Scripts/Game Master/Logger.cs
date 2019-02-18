using System;
using System.IO;
using UnityEngine;

public class Logger : MonoBehaviour
{
  // Singleton pattern
  private static Logger l;
  public static Logger Instance
  {
    get { return l ?? (l = new GameObject("Logger").AddComponent<Logger>()); }
  }

  private struct LogEntry
  {
    public LogDataType ldt;
    public float timestamp;
    public string data;
  }

  private string logName;
  private StreamWriter sw;

  enum LogDataType
  {
    DAMAGE, SPAWN, KILL, SCORE
  }


  public void CreateLog()
  {
    string dateTime = DateTime.Now.ToString("MM-dd-yy_h-mm-ss-ff");
    int dirSlash = Application.dataPath.LastIndexOf("/");

    logName = Application.dataPath.Substring(0, dirSlash) + string.Format("/DDA-{0}.log", dateTime);
    Debug.Log(logName);
    sw = File.CreateText(logName);
  }

  private void Log(LogDataType ldt, string data)
  {
    LogEntry le = new LogEntry();
    le.ldt = ldt;
    le.timestamp = Time.time;
    le.data = data;
    WriteOutToLog(le);
  }

  public void LogDamage(int currHealth)
  {
    string data = string.Format("Current Health: {0}", currHealth);

    Log(LogDataType.DAMAGE, data);
  }

  public void LogScore(int score)
  {
    string data = string.Format("Score: {0}", score);

    Log(LogDataType.SCORE, data);
  }

  public void LogSpawn(ShipStats s, PatternData p)
  {
    string data = string.Format("Ship: {0}, Pattern: {1}", s.name, p.name);

    Log(LogDataType.SPAWN, data);
  }

  public void LogKill(string data)
  {
    Log(LogDataType.KILL, data);
  }

  private static string LogDataEntryToStr(LogEntry le)
  {
    string type;

    switch (le.ldt)
    {
      case LogDataType.DAMAGE:
        {
          type = "Damage";
          break;
        }
      case LogDataType.KILL:
        {
          type = "Kill";
          break;
        }
      case LogDataType.SCORE:
        {
          type = "Score";
          break;
        }
      case LogDataType.SPAWN:
        {
          type = "Spawn";
          break;
        }
      default:
        {
          type = "DEBUG";
          break;
        }
    }

    string time = le.timestamp.ToString();

    return string.Format("{0} | {1} | {2}", type, time, le.data);
  }

  private void WriteOutToLog(LogEntry le)
  {
    string output = LogDataEntryToStr(le);
    sw.WriteLine(output);
  }
}
