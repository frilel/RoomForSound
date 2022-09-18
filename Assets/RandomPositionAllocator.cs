using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPositionAllocator : MonoBehaviour
{
    public List<int> indicesTaken=new List<int>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public Vector3 GenerateRandomPos()
    {
        int index=-1;
        while(true)
        {
            index=Random.Range(0,transform.childCount);
            if(!indicesTaken.Contains(index))
            {
                break;
            }
        }
        indicesTaken.Add(index);
        return transform.GetChild(index).position;
    }
}
