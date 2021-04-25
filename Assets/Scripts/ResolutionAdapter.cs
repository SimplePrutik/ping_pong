using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolutionAdapter : MonoBehaviour
{
    private readonly float normalAspect = 0.5625f;
    private void Start()
    {
        var currentAspect = (float) Screen.width / Screen.height;
        transform.localScale = new Vector3(currentAspect / normalAspect, 1, 1);
    }
}
