using UnityEngine;
using System.Collections;

public class TargetRangeController : TargetChallenge {

    public override void StartChallenge()
    {
        Player.SetControllerMode();
        base.StartChallenge();
    }

    public override void StopChallenge()
    {
        base.StopChallenge();
    }
}
