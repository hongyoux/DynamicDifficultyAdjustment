﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIComponent : MonoBehaviour
{
  public GameObject ui;

  private Text health;
  private Text lives;
  private Text score;
  private Text time;

  // Start is called before the first frame update
  void Start()
  {
    health = ui.transform.Find("Health").GetComponent<Text>();
    lives = ui.transform.Find("Lives").GetComponent<Text>();
    score = ui.transform.Find("Score").GetComponent<Text>();
    time = ui.transform.Find("Time").GetComponent<Text>();
  }

  // Update is called once per frame
  void Update()
  {

  }

  public void UpdateUI(int h, int l, int s, float d)
  {
    health.text = string.Format("{0}", h.ToString());
    score.text = string.Format("{0}", s.ToString());
    time.text = string.Format("{0:F2}", d);

    switch (l)
    {
      case 3:
        {
          lives.text = "First Round";
          break;
        }
      case 2:
        {
          lives.text = "Second Round";
          break;
        }
      case 1:
        {
          lives.text = "Last Round";
          break;
        }
      default:
        {
          lives.text = "Game Over";
          break;
        }
    }

  }
}
