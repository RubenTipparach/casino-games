using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SlotMachine : MonoBehaviour
{

    public List<SlotColumn> slotColumns;


    public Vector2 symbolDimensions = new Vector2(75, 115);

    public float yPadding = 20f;

    public Vector2 spinVelocityRange = new Vector2(10, 15); // spins for 10-15 cycles for 2 seconds

    public float spinDuration = 5f;

    public float totalSpinTime = 6.5f;

    public LineRenderer lineRenderer;

    public List<LineRenderer> spawnLines;
    // Start is called before the first frame update
    void Start()
    {
        spawnLines = new List<LineRenderer>();
        var lib = GameManager.Instance.symbolLibrary;
        var symbolXSpacing = symbolDimensions.y + yPadding;

        var columnHeight = symbolXSpacing * lib.symbols.Count; // we can distribute this differently later.

        for (int i = 0; i < slotColumns.Count; i++)
        {
            var slotPosition = slotColumns[i].transform.position;
            var shuffled = ShuffleArray(lib.symbols.Count);
            slotColumns[i].InitSymbols(columnHeight);

            for (int j = 0; j < lib.symbols.Count; j++)
            {
                var symbolUsed = lib.symbols[shuffled[j]];
                var symbol = Instantiate(symbolUsed, slotColumns[i].transform);
                slotColumns[i].symbols.Add(symbol);
                symbol.runtimeIndex = j;

                var position = (columnHeight / 2f - (symbolDimensions.y + yPadding) * j) / 100f;
                symbol.transform.position = new Vector2(slotColumns[i].transform.position.x, position);
            }
        }

    }


    public int[] ShuffleArray(int length)
    {
        var array = new int[length];
        for(int i = 0; i < length; i++)
        {
            array[i] = i;
        }

        for (int i = 0; i < length; i++)
        {
            int rnd = Random.Range(0, length);

            var tempGO = array[rnd];
            array[rnd] = array[i];
            array[i] = tempGO;
        }

        return array;
    }

    public void Spin()
    {
        for(int i = 0; i < slotColumns.Count; i++)
        {
            float spinRounds = Random.Range(spinVelocityRange.x, spinVelocityRange.y);

            slotColumns[i].Spin(spinRounds, spinDuration);
        }

        foreach (var lineR in spawnLines)
        {
            Destroy(lineR.gameObject);
        }

        spawnLines.Clear();

        StartCoroutine(CalculateLines());
    }

    IEnumerator CalculateLines()
    {
        yield return new WaitForSeconds(totalSpinTime);

        var lib = GameManager.Instance.loadedLineIndex;
        var linesDetected = new List<int[]>();
        foreach(var line in lib)
        {
            var c0 = slotColumns[0];
            var symbolMatcher = c0.symbolPositions[line[0]].symbolIndexKey;
            bool lineFound = true;
            Debug.Log(symbolMatcher + " " + c0.symbolPositions[line[0]].runtimeIndex);

            for (int i = 1; i < slotColumns.Count; i++)
            {
                var c = slotColumns[i];
                var key = c.symbolPositions[line[i]].symbolIndexKey;
                Debug.Log(key + "  " + c.symbolPositions[line[i]].runtimeIndex);

                if(symbolMatcher != key){
                    lineFound = false;
                    break;
                }
            }

            if (lineFound)
            {
                linesDetected.Add(line);
                Debug.Log($"{symbolMatcher} line found! {string.Join(',', line)}");
            }

            Debug.Log("xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx");
        }


        //var symbolXSpacing = symbolDimensions.y + yPadding;
        //var columnHeight = symbolXSpacing * GameManager.Instance.symbolLibrary.symbols.Count; // we can distribute this differently later.
        var positionY = (symbolDimensions.y + yPadding) / 100f;

        foreach (var line in linesDetected)
        {
            var newLineRen = Instantiate(lineRenderer);
            newLineRen.gameObject.SetActive(true);
            newLineRen.positionCount = line.Length;
            for (int i = 0; i < line.Length; i++)
            {
                newLineRen.SetPosition(i, new Vector3(
                    slotColumns[i].transform.position.x, positionY * -1f *(line[i] - 1), -1f ));
            }

            spawnLines.Add(newLineRen);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
