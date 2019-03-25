using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class GM_Preprogrammed_Heuristic : Decision
{
  private List<int> waves = new List<int>() { 8, 3, 4, 0, 7, 5, 6, 0, 11, 3, 4, 3, 4, 9, 1, 3, 2, 4, 7, 11, 7, 9, 12, 9, 10, 12, 3, 4, 10, 1, 2, 1, 2, 6, 5, 6, 5, 9, 12, 12, 6, 5, 11 };

  private int counter = 0;

  public override float[] Decide(List<float> vectorObs, List<Texture2D> visualObs, float reward, bool done, List<float> memory)
  {
    if (counter >= waves.Count)
    {
      counter = Random.Range(0, waves.Count); // When done, pick a random number to start again on.
    }

    return new float[] { waves[counter++] };
  }

  public override List<float> MakeMemory(List<float> vectorObs, List<Texture2D> visualObs, float reward, bool done, List<float> memory)
  {
    return new List<float>();
  }
}
