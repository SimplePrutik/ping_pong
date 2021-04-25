using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class OutBorder : MonoBehaviour, ICollidable
{
    public static Action OnGameOvered = delegate { };

    public void OnCollide(GameObject obj)
    {
        OnGameOvered();
    }
}
