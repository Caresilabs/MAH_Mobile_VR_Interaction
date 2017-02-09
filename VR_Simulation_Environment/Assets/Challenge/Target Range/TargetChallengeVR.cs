using UnityEngine;
using System.Collections;

public class TargetChallengeVR : TargetChallenge {

    public override void StartChallenge()
    {
        Player.SetVrMode();
        base.StartChallenge();
    }

    public override void StopChallenge()
    {
        base.StopChallenge();
    }
}
