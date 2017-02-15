using UnityEngine;
using System.Collections;

public class MazeChallengeVRMotion : MazeChallenge {

    public override void StartChallenge()
    {
        base.StartChallenge();
        Player.SetMovement(Player.MovementType.MOTION_MOVEMENT);
        Player.ShowDot(true);
    }

    public override void StopChallenge()
    {
        base.StopChallenge();
    }
}
