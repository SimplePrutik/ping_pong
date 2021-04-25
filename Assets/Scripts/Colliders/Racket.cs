using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Racket : MonoBehaviour, ICollidable
{
    private readonly float speed = 3;
    private readonly float xLimit = 2f;
    private readonly float deviation = 15f;
    public void OnCollide(GameObject obj)
    {
        var ball = obj.GetComponent<Ball>();
        if (!ball)
            return;
        var direction = ball.Direction;
        ball.Direction = Ball.GetRandomDirection(new Vector3(direction.x, -direction.y, 0), deviation);
        GameManager.Instance.IncScore();
    }

    void Update()
    {
        if (Input.touchCount == 1)
        {
            var touch = Input.GetTouch(0);
            if (touch.position.x > Screen.width / 2)
                transform.Translate(Vector3.right * Time.deltaTime * speed);
            else
                transform.Translate(Vector3.left * Time.deltaTime * speed);
        }

        if (Input.GetMouseButton(0))
            if (Input.mousePosition.x > Screen.width / 2)
                transform.Translate(Vector3.right * Time.deltaTime * speed);
            else
                transform.Translate(Vector3.left * Time.deltaTime * speed);
        var pos = transform.localPosition;

        if (pos.x > xLimit)
            transform.localPosition = new Vector3(xLimit, pos.y, pos.z);

        if (pos.x < -xLimit)
            transform.localPosition = new Vector3(-xLimit, pos.y, pos.z);
    }
}
