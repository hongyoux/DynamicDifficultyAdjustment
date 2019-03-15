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
    //Get Player relative position on screen
    ObservePlayerStats();
    //Get time elapsed in seconds
    AddVectorObs(Time.time / Gamemaster.Instance.targetTime);
    //Get total count of each summon so far
    ObservePercentageOfShipsSummoned();
    //Get all ship types on screen right now
    ObservePercentageOfShipsOnScreen();
    //Get count of all bullets that have hit the player so far
    ObservePercentageOfBulletDamageTaken();
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

    Debug.Log(string.Format("Player's current Health: {0}", p.stats.currHealth));

    //Get Health Lost since previous action
    float lostHealth = previousHP - p.stats.currHealth;
    AddVectorObs(lostHealth);
    previousHP = p.stats.currHealth;

    Debug.Log(string.Format("Health Lost Between Rounds: {0}", lostHealth));

    //Get Player score (Percentage of total possible points earned)
    AddVectorObs(p.stats.score);

    Debug.Log(string.Format("Player's Current Score: {0}", p.stats.score));
  }

  private float CalcRelativePos(float val, float min, float max)
  {
    if (val < 0) return -1 + ((min - val) / min);
    return 1 - ((max - val) / max);
  }


  private void ObservePercentageOfShipsSummoned()
  {
    int[] summonedSoFar = Gamemaster.Instance.sf.GetSummonedWavesSoFar();
    int total = 0;

    float[] summonedSoFarFloat = new float[summonedSoFar.Length];

    foreach (int i in summonedSoFar)
    {
      total += i;
    }

    if (total != 0)
    {
      for (int i = 0; i < summonedSoFarFloat.Length; i++)
      {
        summonedSoFarFloat[i] = (float)summonedSoFar[i];
      }
    }

    Debug.Log(string.Format("Waves summoned so far: [{0}]", string.Join(",", summonedSoFarFloat)));

    AddVectorObs(summonedSoFarFloat);
  }

  private void ObservePercentageOfShipsOnScreen()
  {
    int[] shipsCount = Gamemaster.Instance.GetCountOfAllShips();
    int total = 0;

    float[] shipsCountAsFloat = new float[shipsCount.Length];

    foreach (int i in shipsCount)
    {
      total += i;
    }

    if (total != 0)
    {
      for (int i = 0; i < shipsCountAsFloat.Length; i++)
      {
        shipsCountAsFloat[i] = (float)shipsCount[i];
      }
    }

    Debug.Log(string.Format("Ships on Screen: [{0}]", string.Join(",", shipsCountAsFloat)));

    AddVectorObs(shipsCountAsFloat);
  }

  private void ObservePercentageOfBulletDamageTaken()
  {
    Player p = Gamemaster.Instance.GetPlayer();
    int[] dmgTaken = Gamemaster.Instance.damageDealtByBullets;

    float[] dmgTakenAsFloats = new float[dmgTaken.Length];

    for (int i = 0; i < dmgTaken.Length; i++)
    {
      dmgTakenAsFloats[i] = (float)dmgTaken[i];
    }

    Debug.Log(string.Format("Damage Taken Per Enemy Type: [{0}]", string.Join(",", dmgTakenAsFloats)));

    AddVectorObs(dmgTakenAsFloats);
  }

}
