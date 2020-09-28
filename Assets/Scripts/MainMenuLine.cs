using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuLine : MonoBehaviour
{
    int size=60;
    float height;
    LineRenderer line;
    float randomNumber;
    
    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
        height=1f;
        randomNumber = Random.Range(0f, 1f);
        StartCoroutine(RandomHeight());
    }

    // Update is called once per frame
    void Update()
    {
        float heightToChange=randomNumber;
        height=Mathf.Lerp(height, heightToChange, 2*Time.deltaTime);

        for (int i=0;i<size;i++)
        {
            Vector3 pos = new Vector3(i*0.1f, Mathf.Sin(0.2f*i+Time.time)*height, 0);
            line.SetPosition(i, pos);
        }
    }

    IEnumerator RandomHeight()
    {
        while(true)
        {
            randomNumber = Random.Range(0f, 1f);
            yield return new WaitForSeconds(2);
        }

    }

}
