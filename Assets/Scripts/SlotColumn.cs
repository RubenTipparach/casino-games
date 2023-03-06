using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotColumn : MonoBehaviour
{

    public List<SlotSymbol> symbols;

    public float currentVelocity = 10;
    public float currentRound = 0;

    public float rounds;

    bool spinTriggered = false;
    bool spinDrifted = true;

    public float columnHeight = 0;
    public float spinTimer = 0;

    public SlotSymbol[] symbolPositions;
    
    public void InitSymbols(float height)
    {
        symbols = new List<SlotSymbol>();
        columnHeight = height;
    }

    public void Spin(float rounds, float spinTime, bool drifting = false)
    {
        this.currentVelocity = rounds / spinTime;
        this.rounds = rounds;
        this.spinTimer = spinTime;

        this.spinTriggered = true;



        if (!drifting)
        {
            this.spinDrifted = false;
            symbolPositions = null;

        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (spinTimer <= 0)
        {
            spinTriggered = false;

            // todo, shouldn't need this if we calculate the speed better.
            if(!spinDrifted)
            {
                DetermineRoundingSpin();
            }
        }

        if(spinTriggered)
        {

            foreach(var s in symbols)
            {
                s.transform.position = new Vector2(transform.position.x, 
                    s.transform.position.y 
                    - (Time.deltaTime * currentVelocity));

                if(s.transform.position.y < -columnHeightPixels)
                {
                    s.transform.position = new Vector2(transform.position.x, s.transform.position.y + columnHeightPixels * 2);
                }
            }

            spinTimer -= Time.deltaTime;
        }
    }

    private void DetermineRoundingSpin()
    {

        spinDrifted = true;
        var minSymbol = symbols[0];

        for(int i = 1; i < symbols.Count; i++)
        {
            if (symbols[i].transform.position.y < minSymbol.transform.position.y)
            {
                minSymbol = symbols[i];
            }
        }

        float driftAmount = columnHeightPixels + minSymbol.transform.position.y;

        int lowestSymbolIndex = minSymbol.runtimeIndex;
        int topSymbol = (7 + lowestSymbolIndex) % symbols.Count;

        // Debug.Log($"slowing down {driftAmount} " + transform.name);
        int s0 = topSymbol;
        int s1 = (topSymbol + 1) % symbols.Count;
        int s2 = (topSymbol + 2) % symbols.Count;
        symbolPositions = new SlotSymbol[]
        {
            symbols[s0],
            symbols[s1],
            symbols[s2]
        };

        Debug.Log($"{transform.name} has symbols {s0}, {s1}, {s2}");

        //set drift time for 1 second?
        Spin(driftAmount, 1f, true);
    }

    float columnHeightPixels
    {
        get
        {
            return columnHeight / 2f / 100f;
        }
    }
}
