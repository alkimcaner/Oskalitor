using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLine : MonoBehaviour
{
    int size=60;
    public float height;
    LineRenderer line;
    WaveShaper mainLine;
    
    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
        mainLine = GameObject.Find("currentLine").GetComponent<WaveShaper>();
        height=1f;
    }

    // Update is called once per frame
    void Update()
    {
        float heightToChange=mainLine.randomNumber;
        height=Mathf.Lerp(height, heightToChange, 2*Time.deltaTime);

        for (int i=0;i<size;i++)
        {
            Vector3 pos = new Vector3(i*0.1f, Mathf.Sin(0.2f*i+Time.time)*height, 0);
            line.SetPosition(i, pos);
        }
    }
}
