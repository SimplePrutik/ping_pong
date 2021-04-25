using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour, ICollidable
{

    private readonly float deviation = 5f;
    public void OnCollide(GameObject obj)
    {
        var ball = obj.GetComponent<Ball>();
        if (!ball)
            return;
        var direction = ball.Direction;
        ball.Direction = Ball.GetRandomDirection(new Vector3(-direction.x, direction.y, 0), deviation);
    }
}
