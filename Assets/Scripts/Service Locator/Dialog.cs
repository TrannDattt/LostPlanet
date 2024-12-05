using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialog
{
    public AUnit character;
    [TextArea(minLines: 3, maxLines: 5)] public string[] sentences;
}
