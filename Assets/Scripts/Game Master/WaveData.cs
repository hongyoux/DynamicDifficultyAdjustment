﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

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

  private float randomOffset;

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
        SpawnShip(s, randomOffset);
      }
    }

    CleanUp();
  }

  public void SpawnWave()
  {
    active = true;
    randomOffset = Random.value * 2f; // Between 0 and .5
  }

  private void SpawnShip(ShipData s, float weaponOffset)
  {
    GameObject newPattern = Instantiate(s.pattern);
    PatternData newPatternData = newPattern.GetComponent<PatternData>();
    Transform spawnPoint = newPatternData.waypoints[0];
    newPatternData.waypoints.RemoveAt(0);
    GameObject ship = Instantiate(s.spawnType, spawnPoint.position, spawnPoint.rotation, Gamemaster.ships.transform);

    EnemyShip es = ship.GetComponent<EnemyShip>();
    WeaponComponent wc = ship.GetComponentInChildren<WeaponComponent>();
    wc.SetInitialFireTime(weaponOffset);

    Logger.Instance.LogSpawn(es.stats, newPatternData);

    Gamemaster.Instance.totalPossiblePoints += es.stats.score; // Even if ship doesn't break, total score goes up

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
