using UnityEngine;
using System.Collections;

public class MazeChallengeVRRaycast : MazeChallenge {

    public override void StartChallenge()
    {
        base.StartChallenge();
        Player.SetMovement(Player.MovementType.RAYCAST_MOVEMENT);
        Player.ShowDot(false);
    }

    public override void StopChallenge()
    {
        base.StopChallenge();
    }
}
