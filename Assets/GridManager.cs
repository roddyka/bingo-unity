using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int MaxNumber;
    public int MinNumber;
    public GameObject CardSquarePrefab;
    public int Columns, Rows;
    private GameObject square;

    private int randomNumbers;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < Columns; i++){ //for X
            for (int y = 0; y < Rows; y++){ //for Y
                randomNumbers = Random.Range(MaxNumber, MinNumber);
                Debug.Log(randomNumbers);
                
                GameObject square = Instantiate(CardSquarePrefab) as GameObject;
                square.transform.position = new Vector3(i,y,0f);
            }
        }
    }

}
