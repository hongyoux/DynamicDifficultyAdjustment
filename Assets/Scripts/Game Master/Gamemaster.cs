using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;


public class Gamemaster : MonoBehaviour
{
  public Transform playerSpawnLocation;
  public GameObject player;

  public static GameObject bullets;
  public static GameObject ships;
  public static GameObject waves;

  private string logName;
  private StreamWriter sw;
  
  enum LogDataType
  {
    DAMAGE, SPAWN, KILL, SCORE
  }
  private struct LogEntry
  {
    public LogDataType ldt;
    public float timestamp;
    public string data;
  }

  private static List<LogEntry> logs;

  private Player p;
  private UIComponent uiComponent;
  
  // Use this for initialization
  void Start()
  {
    CreateLogFile();
    logs = new List<LogEntry>();

    uiComponent = transform.GetComponent<UIComponent>();

    bullets = new GameObject("bullets");
    bullets.transform.parent = transform;

    ships = new GameObject("ships");
    ships.transform.parent = transform;

    waves = new GameObject("waves");
    waves.transform.parent = transform;

    GameObject pObj = Instantiate(player, playerSpawnLocation.position, playerSpawnLocation.rotation, ships.transform);
    p = pObj.GetComponent<Player>();
  }

  // Update is called once per frame
  void Update()
  {
    uiComponent.UpdateUI(p.stats.currHealth, p.stats.lives, p.stats.score, 0.0f);
  }

  public Player GetPlayer()
  {
    return p;
  }

  public void UpdatePlayerScore(int score)
  {
    p.stats.score += score;
    LogScore(p.stats.score);
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

  private void CreateLogFile()
  {
    string dateTime = DateTime.Now.ToString("MM-dd-yy_h-mm-ss-ff");
    int dirSlash = Application.dataPath.LastIndexOf("/");

    logName = Application.dataPath.Substring(0, dirSlash) + string.Format("/DDA-{0}.log", dateTime);
    Debug.Log(logName);
    sw = File.CreateText(logName);
  }

  private void WriteOutToLog(LogEntry le)
  {
    string output = LogDataEntryToStr(le);
    Debug.Log(string.Format("Wrote {0} to file", output));
    sw.WriteLine(output);
  }
}
