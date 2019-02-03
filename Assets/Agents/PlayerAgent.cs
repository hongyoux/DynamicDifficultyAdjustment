using UnityEngine;
using MLAgents;

[RequireComponent(typeof(Player))]
public class PlayerAgent : Agent
{
  private Player p;

  void Start()
  {
    p = Gamemaster.Instance.GetPlayer();
  }

  public override void AgentReset()
  {
    GetComponent<Renderer>().enabled = true;
    p.stats.currHealth = p.stats.maxHealth;
    p.stats.lives = 1;
    p.stats.score = 0;
    p.stats.position = Gamemaster.Instance.playerSpawnLocation.position;
    transform.position = Gamemaster.Instance.playerSpawnLocation.position;
    Gamemaster.Instance.Reset();
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
    ObservePlayerCurrentPosition();

    int totalBStats = 100;
    int totalSStats = 10;

    foreach (BulletComponent bs in Gamemaster.Instance.bulletsNearPlayer)
    {
      //Relative Dist from Player
      Vector2 distFromPlayer = p.stats.position - bs.stats.position;

      //Relative X to Player
      AddVectorObs(distFromPlayer.x / 10f);
      //Relative Y to Player
      AddVectorObs(distFromPlayer.y / 20f);
      //Direction of Bullet
      AddVectorObs(bs.stats.direction);
      //Speed of Bullet
      AddVectorObs(bs.stats.velocity);
    }

    for (int i = 0; i < totalBStats - Gamemaster.Instance.bulletsNearPlayer.Count; i++)
    {
      AddVectorObs(1); //Relative X
      AddVectorObs(1); //Relative Y
      AddVectorObs(new Vector2(0, 0)); //Direction
      AddVectorObs(0); //Velocity
    }

    foreach (Ship ss in Gamemaster.Instance.enemiesByValue)
    {
      //Relative Dist from Player
      Vector2 distFromPlayer = p.stats.position - ss.stats.position;

      //Relative X to Player
      AddVectorObs(distFromPlayer.x / 10f);
      //Relative X to Player
      AddVectorObs(distFromPlayer.y / 20f);
      //Score of Ship
      AddVectorObs(ss.stats.score);
    }

    for (int i = 0; i < totalSStats - Gamemaster.Instance.enemiesByValue.Count; i++)
    {
      AddVectorObs(1); //Relative X
      AddVectorObs(1); //Relative Y
      AddVectorObs(0); //Score
    }
  }

  public override void AgentAction(float[] vectorAction, string textAction)
  {
    //Actions
    int action = Mathf.FloorToInt(vectorAction[0]);

    float speed = p.stats.movespeed * Time.deltaTime;
    Vector2 newPos = p.stats.position;

    switch (action)
    {
      case 1:
        {
          //Up
          newPos.y += speed;
          break;
        }
      case 2:
        {
          //Down
          newPos.y -= speed;
          break;
        }
      case 3:
        {
          //Left
          newPos.x -= speed;
          SetReward(.0001f);
          break;
        }
      case 4:
        {
          //Right
          newPos.x += speed;
          SetReward(.0001f);
          break;
        }
    }

    float boundX = 5f - transform.localScale.x / 2;
    float boundY = 10f - transform.localScale.y / 2;

    newPos.x = Mathf.Clamp(newPos.x, -boundX, boundX);
    newPos.y = Mathf.Clamp(newPos.y, -boundY, boundY);

    p.stats.position = newPos;
    transform.position = newPos;
  }
}
