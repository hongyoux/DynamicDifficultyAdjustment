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
    ////Get Player relative position on screen
    //ObservePlayerStats();
    ////Get time elapsed in seconds

    //AddVectorObs(Gamemaster.Instance.getGameTime());

    //AddVectorObs(Gamemaster.Instance.GetExpectedDamage());

    //////Get total count of each summon so far
    ////ObservePercentageOfShipsSummoned();

    //////Get all ship types on screen right now
    ////ObservePercentageOfShipsOnScreen();

    ////Get average damage dealt per wave to player
    //ObserveAverageDamagePerWave();

    ////Get count of all bullets that have hit the player so far
    //ObservePercentageOfBulletDamageTaken();

    //Get if can summon wave this round
    ObserveWaveAvailable();
  }

  public override void AgentAction(float[] vectorAction, string textAction)
  {
    int index = Mathf.FloorToInt(vectorAction[0]);
    if (index == 0)
    {
      // This is the do nothing action
      return;
    }

    Debug.Log(string.Format("Agent Action Requested! Spawning Wave {0}", index - 1));
    Gamemaster.Instance.sf.SpawnWave(index - 1);
  }

  private void ObservePlayerStats()
  {
    Player p = Gamemaster.Instance.GetPlayer();

    //Get Player health (Percentage)
    AddVectorObs(p.stats.currHealth);

    //Get Health Lost since previous action
    float lostHealth = previousHP - p.stats.currHealth;
    float totalLost = p.stats.maxHealth - p.stats.currHealth;

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

  private void ObservePercentageOfShipsSummoned()
  {
    int[] summonedSoFar = Gamemaster.Instance.sf.GetSummonedWavesSoFar();
    for (int i = 0; i < summonedSoFar.Length; i++)
    {
      AddVectorObs(summonedSoFar[i]);
    }

    Debug.Log(string.Format("Waves summoned so far: [{0}]", string.Join(",", summonedSoFar)));
  }

  private void ObservePercentageOfShipsOnScreen()
  {
    int[] shipsCount = Gamemaster.Instance.GetCountOfAllShips();

    for (int i = 0; i < shipsCount.Length; i++)
    {
      AddVectorObs(shipsCount[i]);
    }

    Debug.Log(string.Format("Ships on Screen: [{0}]", string.Join(",", shipsCount)));
  }

  private void ObservePercentageOfBulletDamageTaken()
  {
    Player p = Gamemaster.Instance.GetPlayer();
    int[] dmgTaken = Gamemaster.Instance.damageDealtByBullets;

    for (int i = 0; i < dmgTaken.Length; i++)
    {
      AddVectorObs(dmgTaken[i]);
    }

    Debug.Log(string.Format("Damage Taken Per Enemy Type: [{0}]", string.Join(",", dmgTaken)));
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

  private void ObserveWaveAvailable()
  {
    int[] waveCharges = Gamemaster.Instance.sf.GetWaveCharges();

    float[] waveAvailable = new float[waveCharges.Length];

    for (int i = 0; i < waveCharges.Length; i++)
    {
      if (waveCharges[i] != 0) {
        waveAvailable[i] = 1.0f;
      }
      else
      {
        waveAvailable[i] = 0.0f;
      }
      AddVectorObs(waveAvailable[i]);
    }

    Debug.Log(string.Format("Wave availability: [{0}]", string.Join(",", waveAvailable)));
  }

  public override void AgentOnDone()
  {
    base.AgentOnDone();
  }
}
