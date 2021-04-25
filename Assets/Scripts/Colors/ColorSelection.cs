using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorSelection : MonoBehaviour
{
    private Outline frame => GetComponent<Outline>();
    private int index;

    public static Action<int> OnColorChanged = delegate { };

    public void SetIndex(int _index)
    {
        index = _index;
    }

    public void SetSelection(bool select)
    {
        frame.enabled = select;
    }

    public void OnPress()
    {
        frame.enabled = true;
        OnColorChanged(index);
    }
}
