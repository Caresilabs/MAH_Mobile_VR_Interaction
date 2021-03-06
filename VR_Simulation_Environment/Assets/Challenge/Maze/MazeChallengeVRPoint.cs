﻿using UnityEngine;
using System.Collections;

public class MazeChallengeVRPoint : MazeChallenge
{
    public override void StartChallenge()
    {
        base.StartChallenge();
        Player.SetMovement(Player.MovementType.POINT_MOVEMENT);
        Player.ShowDot(true);
    }

    public override void StopChallenge()
    {
        base.StopChallenge();
    }
}
