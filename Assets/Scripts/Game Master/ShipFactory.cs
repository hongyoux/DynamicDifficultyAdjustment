using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipFactory : MonoBehaviour
{
  public List<GameObject> ships;

  public List<GameObject> waves;

  private int[] waveCount;

  private float cooldown;
  private float timeBetweenWaves;

  private bool stopped;

  // Use this for initialization
  void Start()
  {
    stopped = true;
    timeBetweenWaves = 5;
    cooldown = Time.time + timeBetweenWaves;
    waveCount = new int[waves.Count];
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
      // Really should be agent action.
      Gamemaster.Instance.GetComponent<GamemasterAgent>().RequestDecision();
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
    SpawnWave(index);
  }

  public int[] GetSummonedWavesSoFar()
  {
    return waveCount;
  }

  public void SpawnWave(int index)
  {
    Gamemaster.Instance.SpawnWaveReward(index);

    waveCount[index]++;
    GameObject newWave = Instantiate(waves[index], Gamemaster.waves.transform);
    WaveData wd = newWave.GetComponent<WaveData>();
    wd.SpawnWave();
  }
}
