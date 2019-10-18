using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*holds all the information for single dialogue*/
[System.Serializable]
public class Dialogue
{
    public string name;

    [TextArea(3,12)]

    public string[] sentences;
}
