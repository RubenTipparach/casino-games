using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SlotSymbol : MonoBehaviour
{
    public string symbolIndexKey = "def_1";
    public string symbolName = "default";

    public float betMultiplier = 1;

    public Vector2 generateRange;

    [Range(0f,1f)]
    public float probability = 1;

    public int runtimeIndex = -1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        var offsetX = Vector3.right  * 0.5f;
        var offsety = Vector3.up;
        //Handles.Label(transform.position + offsetX + offsety * .3f, symbolName);
        //Handles.Label(transform.position + offsetX + offsety * 0f, $"bet = {betMultiplier}x");
        //Handles.Label(transform.position + offsetX + offsety * - .3f, $"prob = {probability}");

        Handles.Label(transform.position + offsetX *0f  + offsety * -.3f, $"index= {runtimeIndex}");
    }
#endif
}
