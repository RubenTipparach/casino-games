using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Symbol/Symbol Library")]
public class SlotSymbolLibrary : ScriptableObject
{
    public List<SlotSymbol> symbols;
}
