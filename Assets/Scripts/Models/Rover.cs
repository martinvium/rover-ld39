using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rover {

  public int CurrentPower = 0;
  public int MaxPower = 100;
  public Vector3 Position;

  public void Recharge() {
    CurrentPower = MaxPower;
  }

}
