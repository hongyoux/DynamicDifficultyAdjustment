using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class GamemasterAgent : Agent
{
  private Player p;

  private void Start()
  {
    p = Gamemaster.Instance.GetPlayer();
  }

  public override void AgentReset()
  {
    base.AgentReset();
  }

  private void ObservePlayerCurrentPosition()
  {
    AddVectorObs(CalcRelativePos(p.stats.position.x, -5f, 5f));
    AddVectorObs(CalcRelativePos(p.stats.position.y, -10f, 10f));
  }

  private float CalcRelativePos(float val, float min, float max)
  {
    if (val < 0) return -1 + ((min - val) / min);
    return 1 - ((max - val) / max);
  }

  public override void CollectObservations()
  {
    //Get Player relative position on screen
    ObservePlayerCurrentPosition();

    //Get Player health
    AddVectorObs(p.stats.currHealth);

    //Get Player score
    AddVectorObs(p.stats.score);

    //Get time elapsed in seconds
    AddVectorObs(Time.time);

    //Get total count of each summon so far
    AddVectorObs(Gamemaster.Instance.sf.GetSummonedWavesSoFar());

    //Get all ship types on screen right now


    //Get count of all bullets that have hit the player so far


  }

  public override void AgentAction(float[] vectorAction, string textAction)
  {

  }
}
