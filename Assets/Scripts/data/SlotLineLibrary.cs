using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Symbol/Line Pattern Library")]
public class SlotLineLibrary : ScriptableObject
{
    public List<SlotLine> lines;
}

public class LoadedLine
{
    public int[] lineIndex;
}

[Serializable]
public class SlotLine
{
    public string pattern;
}
