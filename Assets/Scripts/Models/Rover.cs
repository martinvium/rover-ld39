using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class Rover {

  public int CurrentPower = 0;
  public int MaxPower = 100;
  public int X { get; private set; }
  public int Y { get; private set; }

  public Action<int> CurrentPowerChanged;

  public Rover(int x, int y) {
    X = x;
    Y = y;
    Recharge();
  }

  public void Recharge() {
    CurrentPower = MaxPower;
  }

  public bool Move(int x, int y) {
    if(CurrentPower < 25) {
      return false;
    }

    CurrentPower -= 25;
    X = x;
    Y = y;

    CurrentPowerChanged(CurrentPower);

    return true;
  }

  public void GetSoilSample() {
    CurrentPower += 10;

    CurrentPowerChanged(CurrentPower);
  }
}
