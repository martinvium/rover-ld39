using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class Rover {

  public int CurrentPower = 0;
  public int MaxPower = 100;
  public int PendingSamples = 0;
  public int X { get; private set; }
  public int Y { get; private set; }

  public Action<int> CurrentPowerChanged;
  public Action<int> PendingSamplesChanged;

  public Rover(int x, int y) {
    X = x;
    Y = y;
    CurrentPower = MaxPower;
  }

  public void Recharge() {
    CurrentPower = MaxPower;
    CurrentPowerChanged(CurrentPower);
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
    PendingSamples += 1;
    PendingSamplesChanged(PendingSamples);

    CurrentPower -= 5;
    CurrentPowerChanged(CurrentPower);
  }

  public void TransmitAnalyzedSamples() {
    PendingSamples = 0;
    PendingSamplesChanged(PendingSamples);

    CurrentPower = 0;
    CurrentPowerChanged(CurrentPower);
  }
}
