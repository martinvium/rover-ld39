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
    if(CurrentPower < 10) {
      return false;
    }

    CurrentPower -= 10;
    X = x;
    Y = y;

    CurrentPowerChanged(CurrentPower);

    return true;
  }

  public void GetSoilSample(World w, Tile t) {
    if(CurrentPower < 20) {
      Debug.Log("Not enough power to sample!");
      return;
    }

    if(t.Samples < 1) {
      Debug.Log("No soil left to sample!");
      return;
    }

    PendingSamples += t.Samples;
    PendingSamplesChanged(PendingSamples);

    t.Samples = 0;

    CurrentPower -= 20;
    CurrentPowerChanged(CurrentPower);

    w.ChangeTile(t, Tile.EMPTY);
  }

  public void TransmitPendingSamples(HQ hq) {
    hq.ReceiveSamples(PendingSamples);

    PendingSamples = 0;
    PendingSamplesChanged(PendingSamples);

    CurrentPower = 0;
    CurrentPowerChanged(CurrentPower);
  }

  public void ResetSamples() {
    PendingSamples = 0;
    PendingSamplesChanged(PendingSamples);
  }
}
