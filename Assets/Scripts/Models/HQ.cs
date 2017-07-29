using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class HQ {

  public int Samples { get; private set; }
  public World world;

  public Action SamplesChanged;

  public HQ(World world) {
    this.world = world;
    Samples = 0;
  }

  public void ReceiveSamples(int samples) {
    Samples += samples;
    SamplesChanged();
  }

  public string Status() {
    if(world.Days == 0) {
      return "OPTIMISTIC";
    }

    if(world.Days > 9 && Samples < 10) {
      return "FAILURE";
    }

    if(Samples > 10) {
      return "SUCCESSFUL";
    } else if(Samples > 5) {
      return "PROMISING";
    } else if(world.Days > 2) {
      return "EMBARRASSING";
    } else {
      return "THERE IS STILL HOPE";
    }
  }

  public string MissionStatusText() {
    return "MISSION STATUS: " + Status() + " (" + Samples.ToString() + " samples, " + world.Days.ToString() + " days)";
  }
}
