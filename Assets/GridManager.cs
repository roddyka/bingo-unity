using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GridManager : MonoBehaviour
{
    public GameObject parent;
    public int MaxNumber;
    public int MinNumber;
    public GameObject CardSquarePrefab;
    public int Columns, Rows;
    private GameObject square;
    public GameObject GameController;

    private int randomNumbers;
    private int SumColumnAndRow;

    public List<int> list = new List<int>();
    // public List<int> listOrder = List.Orderby(e => e).ToList();
    
    // Start is called before the first frame update
    void Start()
    {
        SumColumnAndRow = Columns * Rows;
        Debug.Log(SumColumnAndRow);
    }

    public void generateCard(){
        // GameController.instance.Credits - for some reason i cant use this - need to be checked
        if(GameController.GetComponent<GameController>().Credits >= 1){
            GameController.GetComponent<GameController>().canvasItemsOnGeneratingCards();
            
                // Preciso verificar se a lista ja contem o numero e busar na lista or numeros em ordem para criar a minha carta de bingo

                //original and working

                for (int i = 0; i < Columns; i++){ //for X
                    for (int y = 0; y < Rows; y++){ //for Y
                        
                            randomNumbers = Random.Range(MaxNumber, MinNumber);
                            // while (!list.Contains(randomNumbers)) //verify if the list contain the number
                            // {
                                list.Add(randomNumbers);
                                GameObject square = Instantiate(CardSquarePrefab) as GameObject;
                                square.transform.SetParent(parent.transform);
                                
                                square.GetComponent<SquareValues>().Value = randomNumbers;
                                // square.GetComponent<SquareValues>().Value = randomNumbers;
                                square.transform.localPosition  = new Vector3(i,y, 0f);
                            // }
                        }
                    }

                    //order the list
                   list = list.OrderBy(x => x).ToList();

                }
        }
}

    // public void generateBalls(){
    //     for (int i = 0; i < Columns; i++){ //for X
    //         for (int y = 0; y < Rows; y++){ //for Y
                
    //             randomNumbers = Random.Range(MaxNumber, MinNumber);
    //             list.Add(randomNumbers);
                
    //             GameObject square = Instantiate(CardSquarePrefab) as GameObject;
    //             square.transform.SetParent(parent.transform);
                
    //             square.GetComponent<SquareValues>().Value = randomNumbers;
    //             square.transform.localPosition  = new Vector3(i,y, 0f);
    //         }
    //     }
    // }

