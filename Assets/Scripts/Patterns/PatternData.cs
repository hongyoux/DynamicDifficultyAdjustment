using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PatternData : MonoBehaviour
{
  public bool repeat;
  public bool dieAtEnd;
  public bool reverse;
  public List<Transform> waypoints;
}
