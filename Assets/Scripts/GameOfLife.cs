using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOfLife : MonoBehaviour
{
    public Vector2Int Board_Size;
    public GameObject[, ] Board;
    public int[, ] Next_Board;
    public GameObject Prefab_Square;
    private float TimerMax = 0.2f;
    private float Timer;
    private bool pause = false;
    private Camera Cam;
    // Start is called before the first frame update
    void Start()
    {
        Timer = TimerMax; 
        Board = new GameObject[Board_Size.x, Board_Size.y];
        Next_Board = new int[Board_Size.x, Board_Size.y];
        Cam = GetComponent<Camera>();
        Cam.clearFlags = CameraClearFlags.SolidColor;
        Cam.backgroundColor = Color.blue;
        for (int i = 0; i < Board_Size.x; i++)
        {
            for (int j = 0; j < Board_Size.y; j++)
            {
                Vector3 Position = new Vector3(i - Board_Size.x/2f,j -Board_Size.y/2f,0);
                Board[i,j] = Instantiate( Prefab_Square, Position, Quaternion.identity);
                Board[i,j].GetComponent<DNA>().State = randomState(); 
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.touchCount > 2)
        {
            pause =! pause;
        }

        if (pause)
        {
            Cam.backgroundColor = Color.gray;
        }
        else
        {
            Cam.backgroundColor = Color.blue;
            if (Timer >= TimerMax)
            {
                for (int i = 0; i < Board_Size.x; i++)
                {
                    for (int j = 0; j < Board_Size.y; j++)
                    {
                        int Num_Neigh = CheackStateOfNeighbor(i,j);
                        if (Num_Neigh == 3 && Board[i,j].GetComponent<DNA>().State == 0)
                        {
                            Next_Board[i,j] = 1;
                        }
                        else if ((Num_Neigh < 2 || Num_Neigh > 3 ) && Board[i,j].GetComponent<DNA>().State == 1)
                        {
                            Next_Board[i,j] = 0;
                        }

                        else if ((Num_Neigh == 2 || Num_Neigh == 3 ) && Board[i,j].GetComponent<DNA>().State == 1)
                        {
                            Next_Board[i,j] = 1;
                        }
                    }
                }
                Timer = 0;
                UpdateBoard();
            }
        }
    
        Timer += Time.deltaTime;
    }

    void UpdateBoard(){
        for (int i = 0; i < Board_Size.x; i++)
        {
            for (int j = 0; j < Board_Size.y; j++)
            {
                Board[i,j].GetComponent<DNA>().State = Next_Board[i,j];
            }
        }
    }

    int randomState(){
        int Ran_Num = Random.Range(0, 10);
        if (Ran_Num < 5){return 0;}
        else {return 1;}
    }

    int CheackStateOfNeighbor(int i, int j){
        int num_Neigh =  Board[(i -1 + Board_Size.x)%Board_Size.x, (j -1 + Board_Size.y)%Board_Size.y].GetComponent<DNA>().State;
        num_Neigh = num_Neigh + Board[(i + Board_Size.x)%Board_Size.x, (j -1 + Board_Size.y)%Board_Size.y].GetComponent<DNA>().State;
        num_Neigh = num_Neigh + Board[(i + 1 + Board_Size.x)%Board_Size.x, (j -1 + Board_Size.y)%Board_Size.y].GetComponent<DNA>().State;
        num_Neigh = num_Neigh + Board[(i - 1 + Board_Size.x)%Board_Size.x, (j + Board_Size.y)%Board_Size.y].GetComponent<DNA>().State;
        num_Neigh = num_Neigh + Board[(i + 1 + Board_Size.x)%Board_Size.x, (j + Board_Size.y)%Board_Size.y].GetComponent<DNA>().State;
        num_Neigh = num_Neigh + Board[(i - 1 + Board_Size.x)%Board_Size.x, (j + 1 + Board_Size.y)%Board_Size.y].GetComponent<DNA>().State;
        num_Neigh = num_Neigh + Board[(i + Board_Size.x)%Board_Size.x, (j + 1 + Board_Size.y)%Board_Size.y].GetComponent<DNA>().State;
        num_Neigh = num_Neigh + Board[(i + 1 + Board_Size.x)%Board_Size.x, (j + 1 + Board_Size.y)%Board_Size.y].GetComponent<DNA>().State;
        return num_Neigh;
    }

}
