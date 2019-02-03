using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveData : MonoBehaviour
{
  private float initialTime;

  private bool active;

  [Serializable]
  public struct ShipData
  {
    public float spawnTime;
    public GameObject spawnType;
    public GameObject pattern;
  }

  public List<ShipData> waveData;
  private List<ShipData> garbage;

  private void Awake()
  {
    garbage = new List<ShipData>();
    initialTime = Time.time;
  }

  public void Update()
  {
    if (!active)
    {
      return;
    }
    foreach (ShipData s in waveData)
    {
      if (Time.time >= initialTime + s.spawnTime)
      {
        garbage.Add(s);
        SpawnShip(s);
      }
    }

    CleanUp();
  }

  public void SpawnWave()
  {
    active = true;
  }

  private void SpawnShip(ShipData s)
  {
    GameObject newPattern = Instantiate(s.pattern);
    PatternData newPatternData = newPattern.GetComponent<PatternData>();
    Transform spawnPoint = newPatternData.waypoints[0];
    newPatternData.waypoints.RemoveAt(0);
    GameObject ship = Instantiate(s.spawnType, spawnPoint.position, spawnPoint.rotation, Gamemaster.ships.transform);

    EnemyShip es = ship.GetComponent<EnemyShip>();
    Logger.Instance.LogSpawn(es.stats, newPatternData);

    EnemyPatternComponent epc = ship.AddComponent<EnemyPatternComponent>();
    epc.patternData = newPattern;
    Destroy(newPattern);
  }

  private void CleanUp()
  {
    foreach (ShipData s in garbage)
    {
      waveData.Remove(s);
    }
    garbage.Clear();

    if (waveData.Count == 0)
    {
      Destroy(gameObject);
    }
  }
}
