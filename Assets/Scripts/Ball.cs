using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour
{
    private readonly float minSpeed = 3;
    private readonly float maxSpeed = 4;
    private readonly float startAngleRange = 45f;
    private readonly Vector3 outOfGame = new Vector3(100,100,100);



    private float speed;
    private Vector3 direction;

    private Coroutine routine;

    public Vector3 Direction
    {
        get
        {
            return direction;
        }

        set
        {
            direction = value;
        }
    }

    public GameObject spawner;

    private void Start()
    {
        GameManager.OnNewGameStarted += Spawn;
        OutBorder.OnGameOvered += GameOver;
    }

    /// <summary>
    /// Returns randomly deviated vector from given one
    /// </summary>
    /// <param name="startDir">Vector deviation is counting from</param>
    /// <param name="angleDistribution">Deviation angle</param>
    /// <param name="canBeOpposite">If vector can be set to the opposite direction</param>
    /// <returns></returns>
    public static Vector3 GetRandomDirection(Vector3 startDir, float angleDistribution, bool canBeOpposite = false)
    {
        var dir = startDir.normalized;
        var oppositeSign = canBeOpposite ? (Random.value < .5 ? -1 : 1) : 1;
        var angle = Mathf.Deg2Rad * (Vector3.SignedAngle(Vector3.right, dir, Vector3.forward) + Random.Range(-angleDistribution, angleDistribution));
        return new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * oppositeSign;
    }

    public void Spawn()
    {
        GetComponent<MeshRenderer>().material = GameSettings.ballMaterial;
        speed = Random.Range(minSpeed, maxSpeed);
        direction = GetRandomDirection(Vector3.down, startAngleRange, true);
        transform.localPosition = spawner.transform.localPosition;
        routine = StartCoroutine(Move());
    }

    public void GameOver()
    {
        transform.localPosition = outOfGame;
        speed = 0;
        if (routine == null) return;
        StopCoroutine(routine);
    }

    IEnumerator Move()
    {
        while (true)
        {
            transform.Translate(direction * speed * Time.deltaTime);
            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var collider = other.GetComponent<ICollidable>();
        if (collider != null)
        {
            collider.OnCollide(gameObject);
        }
    }

    private void OnDestroy()
    {
        GameManager.OnNewGameStarted -= Spawn;
        OutBorder.OnGameOvered -= GameOver;
    }
}
