using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipFactory : MonoBehaviour
{
  private const int chargeAmt = 4;

  public List<GameObject> ships;

  public List<GameObject> waves;

  private int[] waveCount;

  private int[] waveHits;

  private float cooldown;
  private float timeBetweenWaves;

  private bool stopped;

  private int lastWave;

  // Use this for initialization
  void Start()
  {
    stopped = true;
    timeBetweenWaves = chargeAmt;
    cooldown = Time.time + timeBetweenWaves;

    waveHits = new int[waves.Count];

    waveCount = new int[waves.Count];

    lastWave = -1;
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

      timeBetweenWaves = chargeAmt - 2 * (Time.time - Gamemaster.Instance.timeStart) / Gamemaster.Instance.targetTime; // at target time, spawning every 3 seconds
    }
  }

  public void Reset()
  {
    cooldown = Time.time + timeBetweenWaves;
    timeBetweenWaves = chargeAmt;

    waveHits = new int[waves.Count];

    waveCount = new int[waves.Count];

    lastWave = -1;

    stopped = false;
  }

  public void Stop()
  {
    stopped = true;
  }
  public int[] GetSummonedWavesSoFar()
  {
    return waveCount;
  }

  public int[] GetWaveHits()
  {
    return waveHits;
  }

  public void SpawnWave(int index)
  {
    if (lastWave != index) {
      Gamemaster.Instance.SpawnWaveReward();
    }
    waveCount[index]++;
    GameObject newWave = Instantiate(waves[index], Gamemaster.waves.transform);
    WaveData wd = newWave.GetComponent<WaveData>();
    wd.SpawnWave();
    lastWave = index;
  }
  

  public void UpdateWaveHit()
  {
    waveHits[lastWave]++;
  }
}

