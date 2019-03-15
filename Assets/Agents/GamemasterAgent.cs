using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class GamemasterAgent : Agent
{
  public override void AgentReset()
  {
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
    Debug.Log(string.Format("Agent Action Requested! Spawning Wave {0}", index));
    Gamemaster.Instance.sf.SpawnWave(index);
  }

  private void ObservePlayerStats()
  {
    Player p = Gamemaster.Instance.GetPlayer();

    //Get Player relative position (normalized between -1 and 1)
    AddVectorObs(CalcRelativePos(p.stats.position.x, -5f, 5f));
    AddVectorObs(CalcRelativePos(p.stats.position.y, -10f, 10f));

    //Get Player health (Percentage)
    AddVectorObs(p.stats.currHealth);

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
    int total = 0;

    float[] summonedPercentage = new float[summonedSoFar.Length];

    foreach (int i in summonedSoFar)
    {
      total += i;
    }

    if (total != 0)
    {
      for (int i = 0; i < summonedPercentage.Length; i++)
      {
        summonedPercentage[i] = (float)summonedSoFar[i] / total;
      }
    }

    Debug.Log(summonedPercentage.ToString());

    AddVectorObs(summonedPercentage);
  }

  private void ObservePercentageOfShipsOnScreen()
  {
    int[] shipsCount = Gamemaster.Instance.GetCountOfAllShips();
    int total = 0;

    float[] shipsCountPercentage = new float[shipsCount.Length];

    foreach (int i in shipsCount)
    {
      total += i;
    }

    if (total != 0)
    {
      for (int i = 0; i < shipsCountPercentage.Length; i++)
      {
        shipsCountPercentage[i] = (float)shipsCount[i] / total;
      }
    }

    Debug.Log(shipsCountPercentage.ToString());

    AddVectorObs(shipsCountPercentage);
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

    Debug.Log(dmgTakenAsFloats.ToString());

    AddVectorObs(dmgTakenAsFloats);
  }

}
