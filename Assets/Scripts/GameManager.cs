using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{

    public SlotSymbolLibrary symbolLibrary;

    public SlotLineLibrary linesLibrary;

    public List<int[]> loadedLineIndex;

    public static GameManager Instance;

    public void Awake()
    {
        Instance = this;
        loadedLineIndex = new List<int[]>();
        foreach (var line in linesLibrary.lines)
        {
            var lineArray = line.pattern.ToCharArray().Select(p => int.Parse(p.ToString())).ToArray();
            ///Debug.Log(string.Join(",",lineArray));
            loadedLineIndex.Add(lineArray);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
