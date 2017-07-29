using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class Rover {

  public int CurrentPower = 0;
  public int MaxPower = 100;
  public Vector3 Position { get; private set; }

  public Action<int> CurrentPowerChanged;

  public Rover(int x, int y) {
    Position = new Vector3(x, y, 0);
    Recharge();
  }

  public void Recharge() {
    CurrentPower = MaxPower;
  }

  public bool Move(Vector3 destination) {
    if(CurrentPower < 25) {
      return false;
    }

    CurrentPower -= 25;
    Position = destination;

    CurrentPowerChanged(CurrentPower);

    return true;
  }

}
