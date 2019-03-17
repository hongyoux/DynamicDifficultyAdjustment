using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipFactory : MonoBehaviour
{
  private const int chargeAmt = 1;

  public List<GameObject> ships;

  public List<GameObject> waves;

  private int[] waveCount;

  private int[] waveHits;

  private int[] waveCharges;

  private float cooldown;
  private float timeBetweenWaves;

  private bool stopped;

  // Use this for initialization
  void Start()
  {
    stopped = true;
    timeBetweenWaves = 5;
    cooldown = Time.time + timeBetweenWaves;

    waveHits = new int[waves.Count];

    waveCount = new int[waves.Count];

    waveCharges = new int[waves.Count];

    for (int i = 0; i < waves.Count; i++)
    {
      waveCharges[i] = chargeAmt;
    }

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
      Gamemaster.Instance.previousHP = Gamemaster.Instance.GetPlayer().stats.currHealth;
      // Really should be agent action.
      Gamemaster.Instance.GetComponent<GamemasterAgent>().RequestDecision();
      cooldown = Time.time + timeBetweenWaves;

      timeBetweenWaves = 5 - 2 * (Time.time - Gamemaster.Instance.timeStart) / Gamemaster.Instance.targetTime; // at target time, spawning every 3 seconds

      //Debug.Log(string.Format("Time between waves: {0}", timeBetweenWaves));
    }
  }

  public void Reset()
  {
    cooldown = Time.time + timeBetweenWaves;
    timeBetweenWaves = 5;

    waveHits = new int[waves.Count];

    waveCount = new int[waves.Count];

    waveCharges = new int[waves.Count];

    for (int i = 0; i < waves.Count; i++)
    {
      waveCharges[i] = chargeAmt;
    }

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

  public int[] GetWaveCharges()
  {
    return waveCharges;
  }

  public int[] GetWaveHits()
  {
    return waveHits;
  }

  public void SpawnWave(int index)
  {
    if (waveCharges[index] != 0)
    {
      waveCount[index]++;
      GameObject newWave = Instantiate(waves[index], Gamemaster.waves.transform);
      WaveData wd = newWave.GetComponent<WaveData>();
      wd.SpawnWave();
      waveCharges[index]--;

      Gamemaster.Instance.SpawnWaveReward(index);
      TryRefillWaves();
    }
    else
    {
      Gamemaster.Instance.FailedSpawnPunish(index);
    }
  }

  private void TryRefillWaves()
  {
    foreach (int i in waveCharges) {
      if (i != 0)
      {
        return;
      }
    }
    for (int i = 0; i < waveCharges.Length; i++)
    {
      waveCharges[i] = chargeAmt;
    }
  }

  public void UpdateWaveHit()
  {
    //waveHits[Gamemaster.Instance.lastWave]++;
  }
}

