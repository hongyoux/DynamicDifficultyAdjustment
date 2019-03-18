using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class GamemasterAgent : Agent
{
  float previousHP;

  private void Start()
  {
    previousHP = Gamemaster.Instance.GetPlayer().stats.maxHealth;
  }

  public override void AgentReset()
  {
    previousHP = Gamemaster.Instance.GetPlayer().stats.maxHealth;
    base.AgentReset();
  }

  public override void CollectObservations()
  {
    ObservePlayerStats(); // 3

    //Get time elapsed in seconds
    AddVectorObs(Gamemaster.Instance.GetExpectedDamagePercent()); // 1

    //Get total count of each summon so far
    ObserveWaveCountSummoned(); // 12

    //Get average damage dealt per wave to player
    ObserveAverageDamagePerWave(); // 12
  }

  public override void AgentAction(float[] vectorAction, string textAction)
  {
    int index = Mathf.FloorToInt(vectorAction[0]);
    if (index == 0)
    {
      Debug.Log("Agent Picked SKIP");
      return;
    }

    Debug.Log(string.Format("Agent Action Requested! Spawning Wave {0}", index - 1));
    Gamemaster.Instance.sf.SpawnWave(index - 1);
  }

  private void ObservePlayerStats()
  {
    Player p = Gamemaster.Instance.GetPlayer();

    //Get Player health (Percentage)
    AddVectorObs(p.stats.currHealth / p.stats.maxHealth);

    float lostHealth = previousHP - p.stats.currHealth;
    float totalLost = p.stats.maxHealth - p.stats.currHealth;

    //Get Health Lost since previous action
    AddVectorObs(lostHealth);
    previousHP = p.stats.currHealth;

    //Get Player score (Percentage of total possible points earned)
    AddVectorObs(p.stats.score);
  }

  private float CalcRelativePos(float val, float min, float max)
  {
    if (val < 0) return -1 + ((min - val) / min);
    return 1 - ((max - val) / max);
  }

  private void ObserveWaveCountSummoned()
  {
    int[] summonedSoFar = Gamemaster.Instance.sf.GetSummonedWavesSoFar();
    for (int i = 0; i < summonedSoFar.Length; i++)
    {
      AddVectorObs(summonedSoFar[i]);
    }

    Debug.Log(string.Format("Waves summoned so far: [{0}]", string.Join(",", summonedSoFar)));
  }

  private void ObserveAverageDamagePerWave()
  {
    int[] waveHits = Gamemaster.Instance.sf.GetWaveHits();
    int[] waveCount = Gamemaster.Instance.sf.GetSummonedWavesSoFar();

    float[] avgDamage = new float[waveCount.Length];

    for (int i = 0; i < waveCount.Length; i++)
    {
      if (waveCount[i] == 0)
      {
        avgDamage[i] = 0f;
      }
      else
      {
        avgDamage[i] = (float)waveHits[i] / waveCount[i];
      }
      AddVectorObs(avgDamage[i]);
    }

    Debug.Log(string.Format("Average Dmg Per Wave: [{0}]", string.Join(",", avgDamage)));
  }
}
