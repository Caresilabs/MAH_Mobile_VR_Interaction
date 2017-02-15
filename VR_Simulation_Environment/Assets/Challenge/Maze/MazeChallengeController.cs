using UnityEngine;
using System.Collections;

public class MazeChallengeController : MazeChallenge
{
    public override void StartChallenge()
    {
        base.StartChallenge();
        Player.SetMovement(Player.MovementType.CONTROLLER_MOVEMENT);
    }

    public override void StopChallenge()
    {
        base.StopChallenge();
    }
}
