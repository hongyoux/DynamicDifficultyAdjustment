using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class GMHeuristicDecision_Random : Decision
{
  int count = 0;

  public override float[] Decide(List<float> vectorObs, List<Texture2D> visualObs, float reward, bool done, List<float> memory)
  {
    return new float[] { count++ % 23 };

    //int randomInt = Random.Range(0, 23);
    //return new float[] { randomInt };
  }

  public override List<float> MakeMemory(List<float> vectorObs, List<Texture2D> visualObs, float reward, bool done, List<float> memory)
  {
    return new List<float>();
  }
}
