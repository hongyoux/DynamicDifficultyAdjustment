using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipFactory : MonoBehaviour
{
  public List<GameObject> waves;
  private List<GameObject> cleanThese;

  private float cooldown;
  private float timeBetweenWaves;

  private bool stopped;

  // Use this for initialization
  void Start()
  {
    cleanThese = new List<GameObject>();

    timeBetweenWaves = 5;

    cooldown = Time.time + timeBetweenWaves;
  }

  // Update is called once per frame
  void Update()
  {
    if (stopped)
    {
      return;
    }
    if (Time.time >= cooldown)
    {
      SpawnRandomWave();
      cooldown = Time.time + timeBetweenWaves;
    }
  }

  public void Reset()
  {
    cooldown = Time.time + timeBetweenWaves;
    stopped = false;
  }

  public void Stop()
  {
    stopped = true;
  }

  void SpawnRandomWave()
  {
    int index = UnityEngine.Random.Range(0, waves.Count);
    Debug.Log(index);
    GameObject newWave = Instantiate(waves[index], Gamemaster.waves.transform);
    WaveData wd = newWave.GetComponent<WaveData>();
    wd.SpawnWave();
  }
}
