using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{


    public Shape[] m_Shapes;
    
    Shape GetRandomShape()
    {
        int i = Random.Range(0, m_Shapes.Length);

        if(m_Shapes[i])
        {
            return m_Shapes[i];
        }
        else
        {
            Debug.Log("No shape Found!!!");
            return null;
        }
    }
    public Shape SpawnShape()
    {
        Shape shape = null;


       shape= Instantiate(GetRandomShape(), transform.position, Quaternion.identity) as Shape;
        
        if(shape)
        {
            return shape;
        }
        else
        {
            Debug.Log("No shape Found!!!");
            return null;
        }

    }
    
    
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
