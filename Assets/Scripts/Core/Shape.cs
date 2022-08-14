﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape : MonoBehaviour
{


    public bool m_rotate=true;
    private void Move(Vector3 direction)
    {
        transform.position += direction;
    }

    public void MoveLeft()
    {
        Move(new Vector3(-1, 0, 0));
    }

    public void MoveRight()
    {
        Move(new Vector3(1, 0, 0));
    }

    public void MoveDown()
    {
        Move(new Vector3(0, -1, 0));
    }

    public void MoveUp()
    {
        Move(new Vector3(0, 1, 0));
    }

    public void RotateLeft()
    {
        if (m_rotate)
        {
            transform.Rotate(0, 0, 90);
        }
    }

    public void RotateRight()
    {
        if (m_rotate)
        {
            transform.Rotate(0, 0,-90);
        }
    }
    void Start()
    {
       // InvokeRepeating("MoveDown",0,0.5f);
    }

   
    void Update()
    {
        
    }
}
