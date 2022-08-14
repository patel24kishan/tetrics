﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{

    public Transform m_EmptySprite;
    private int m_height = 30, m_width = 10;
    public int m_header = 8;

    Transform[,] m_grid;

    private void Awake()
    {
        m_grid = new Transform[m_width, m_height];
    }

    void Start()
    {
        DrawCells();
    }

     void Update()
    {
        
    }

    private bool RowIsComplete(int y)
    {
        for(int x=0;x<m_width; ++x)
        {
            if(m_grid[x,y]==null)
            {
                return false;
            }
        }
        return true;
    }

    private void ClearRow(int y)
    {
        for (int x = 0; x < m_width; ++x)
        {
            if (m_grid[x, y] != null)
            {
                Destroy(m_grid[x, y].gameObject);
            }
            m_grid[x, y] = null;
        }
    }

    void ShiftOneRowDown(int y)
    {
        for (int x = 0; x < m_width; ++x)
        {
            if (m_grid[x, y] != null)
            {
                m_grid[x, y - 1] = m_grid[x, y];
                m_grid[x, y] = null;
                m_grid[x, y - 1].position += new Vector3(0,-1, 0);
            }
        }
    }

    void ShiftRowsDown(int StartY)
    {

        for (int i = StartY; i < m_height; ++i)
        {
            ShiftOneRowDown(i);
        }

    }

    public void ClearAllRows()
    {

        for (int y = 0; y < m_height; ++y)
        {
            if(RowIsComplete(y))
            {

                ClearRow(y);
                ShiftOneRowDown(y+1);
                ShiftRowsDown(y);
                y--;
            }
        }
    }

  public void StoreShapeInGrid(Shape shape)
  {
        if (shape == null)
        {
            return;
        }
        foreach (Transform child in shape.transform)
        {
            Vector2 pos = VectorF.Round(child.position);
            m_grid[(int)pos.x, (int)pos.y] = child;
        }
  }
    bool IsWithinBoard(int x, int y)
    {
        return (x >= 0 && x < m_width && y >= 0);
    }

    bool IsOccupied(int x, int y, Shape shape)
    {
        return (m_grid[x, y] != null && m_grid[x, y].parent != shape.transform);
   
    }

    public bool IsvalidPosition(Shape shape)
    {
        foreach (Transform child in shape.transform)
        {
            Vector2 pos = VectorF.Round(child.position);

            if (!IsWithinBoard((int)pos.x, (int)pos.y))
            {
                return false;
            }

            if(IsOccupied((int) pos.x,(int) pos.y,shape))
                {
                return false;
            }
        }
        return true;
    }
    private void DrawCells()
    {
        for(int y=0;y<m_height- m_header;y++)
        {
            for(int x=0;x<m_width;x++)
            {
                Transform clone;
                clone = Instantiate(m_EmptySprite, new Vector3(x, y, 0), Quaternion.identity) as Transform;
                clone.name = "Board Space (x=" + x.ToString() + ",y=" + y.ToString() + ")";
                clone.transform.parent = transform;  
            }
        }

        
    }

    public bool IsOverLimit(Shape shape)
    {
        foreach( Transform child in shape.transform)
        {
            if(child.transform.position.y >= m_height-m_header-1)
            {
                return true;
            }
        }
        return false;
    }
  
}
