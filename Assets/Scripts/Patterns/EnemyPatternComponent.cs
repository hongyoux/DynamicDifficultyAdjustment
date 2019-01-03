using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatternComponent : MonoBehaviour
{
  public GameObject patternData;

  private int currentWaypoint;
  private List<Transform> waypoints;
  private bool repeat;

  private Ship s;

  private void Start()
  {
    s = GetComponent<Ship>();

    waypoints = new List<Transform>(patternData.GetComponentsInChildren<Transform>());
    waypoints.RemoveAt(0);

    PatternData pd = patternData.GetComponent<PatternData>();
    repeat = pd.repeat;

    currentWaypoint = 0;
  }

  // Update is called once per frame
  void Update()
  {
    if (Vector3.Distance(transform.position, waypoints[currentWaypoint].position) <= (s.stats.movespeed * Time.deltaTime))
    {
      if (repeat)
      {
        currentWaypoint = (currentWaypoint + 1) % waypoints.Count;
      }
      else
      {
        if (currentWaypoint != waypoints.Count - 1)
        {
          currentWaypoint++;
        }
      }
    }

    Vector2 direction = waypoints[currentWaypoint].position - new Vector3(s.stats.position.x, s.stats.position.y, 0);
    direction.Normalize();

    Vector2 movement = direction * Time.deltaTime * s.stats.movespeed;
    Vector2 newPos = s.stats.position;
    newPos += movement;

    s.stats.position = newPos;
    transform.position = newPos;
  }
}
