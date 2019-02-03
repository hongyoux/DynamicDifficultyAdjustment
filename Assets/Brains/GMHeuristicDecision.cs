using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class GMHeuristicDecision : Decision
{
  public override float[] Decide(List<float> vectorObs, List<Texture2D> visualObs, float reward, bool done, List<float> memory)
  {
    int randomInt = Random.Range(0, 20);
    return new float[] { 0, randomInt };
  }

  public override List<float> MakeMemory(List<float> vectorObs, List<Texture2D> visualObs, float reward, bool done, List<float> memory)
  {
    return new List<float>();
  }
}
