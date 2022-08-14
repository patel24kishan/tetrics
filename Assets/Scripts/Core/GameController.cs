using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameController : MonoBehaviour
{
    Board m_Board;
    Spawner m_Spawner;

     bool m_gameOver;

    public GameObject m_GaveOverPanel;

    Shape m_activeShape;

   public  float m_dropInterval = .45f;

    public float m_TimetoNextKey;

    
     float m_keyRepeatRate = .25f;

     float m_timetoNextKeyLeftRight;

    [Range(0.02f, 1f)]
    public float m_keyRepeatRateLeftRight = .25f;

     float m_timetoNextKeyDown;

    [Range(0.02f, 1f)]
    public float m_keyRepeatDown = .25f;

     float m_timetoNextKeyRotate;

    [Range(0.02f, 1f)]
    public float m_keyRepeatRotate = .25f;

    float m_TimeToDrop;
    void Start()
    {
        m_GaveOverPanel.SetActive(false);

        m_Board = GameObject.FindGameObjectWithTag("Board").GetComponent<Board>();
        m_Spawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<Spawner>();

        m_timetoNextKeyLeftRight = Time.time + m_timetoNextKeyLeftRight;
        m_timetoNextKeyRotate = Time.time + m_keyRepeatRotate;
        m_timetoNextKeyDown = Time.time + m_keyRepeatDown;
        
        if (m_activeShape==null)
        {
            m_activeShape = m_Spawner.SpawnShape();
        }
        if(m_Spawner)
        {
            m_Spawner.transform.position = VectorF.Round(m_Spawner.transform.position);
        }
    }


    public void PlayerInput()
    {
        if (!m_Board || !m_Spawner)
        {
            return;
        }

        if (Input.GetButtonDown("MoveRight") && Time.time >m_timetoNextKeyLeftRight)
        {
            m_activeShape.MoveRight();
            m_timetoNextKeyLeftRight = Time.time + m_keyRepeatRateLeftRight;
            if (m_Board.IsvalidPosition(m_activeShape))
            {
                Debug.Log("Move Right");

            }
            else
            {
                Debug.Log("You have reached boundary!!!");
                m_activeShape.MoveLeft();
            }

        }

        else if (Input.GetButtonDown("MoveLeft") && Time.time > m_timetoNextKeyLeftRight)
        {
            m_activeShape.MoveLeft();
            m_timetoNextKeyLeftRight = Time.time + m_keyRepeatRateLeftRight ;
            if (m_Board.IsvalidPosition(m_activeShape))
            {
                Debug.Log("Move Left");

            }
            else
            {
                Debug.Log("You have reached boundary!!!");
                m_activeShape.MoveRight();
            }

        }


        else if (Input.GetButtonDown("Rotate") && Time.time > m_timetoNextKeyRotate)
        {
            m_activeShape.RotateLeft();
            m_timetoNextKeyRotate = Time.time + m_keyRepeatRotate;
            if (m_Board.IsvalidPosition(m_activeShape))
            {
                Debug.Log("Rotate Left");

            }
            else
            {
                Debug.Log("You have reached boundary!!!");
                //  m_activeShape.MoveRight();
            }

        }

        else if (Input.GetButtonDown("MoveDown") && (Time.time > m_timetoNextKeyDown) ||( Time.time > m_TimeToDrop))
        {
            m_TimeToDrop = Time.time + m_dropInterval;
            m_timetoNextKeyDown = Time.time + m_keyRepeatDown;
            m_activeShape.MoveDown();


            if (!m_Board.IsvalidPosition(m_activeShape))
            {
                if (m_Board.IsOverLimit(m_activeShape))
                {
                    GameOver();
                }
                else
                {
                    LandShape();
                }
            }

        }
       
    }

    private void GameOver()
    {
        m_GaveOverPanel.SetActive(true);
        m_gameOver = true;
        m_activeShape.MoveUp();
        Debug.LogWarning(m_activeShape.name + "is over limit");
    }

    public void RestartBtn()
    {
        SceneManager.LoadScene(0);
    }
    private void LandShape()
    {
       

        m_activeShape.MoveUp();
        m_Board.StoreShapeInGrid(m_activeShape);

        m_timetoNextKeyLeftRight = Time.time ;
        m_timetoNextKeyRotate = Time.time;
        m_timetoNextKeyDown = Time.time;


        if (m_Spawner)
        {
            m_activeShape = m_Spawner.SpawnShape();
        }

        m_Board.ClearAllRows();
    }


    // Update is called once per frame
    void Update()
    {

        if(!m_Board || m_gameOver || !m_Spawner || !m_activeShape )
        {
            return;
        }

        PlayerInput();
    }
}