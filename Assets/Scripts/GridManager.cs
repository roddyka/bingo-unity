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
            
                Debug.Log(list.Count);
                // Preciso verificar se a lista ja contem o numero e busar na lista or numeros em ordem para criar a minha carta de bingo
                while (list.Count < 15)
                {
                    randomNumbers = Random.Range(MaxNumber, MinNumber);
                    if(!list.Contains(randomNumbers)){
                        list.Add(randomNumbers);
                        Debug.Log(list.Count);
                    }
                }

                //order the list
                list = list.OrderBy(x => x).ToList();

                
                //original and working
                for (int x = 0; x < Columns; x++){ //for X
                    for (int y = 0; y < Rows; y++){ //for Y
                        
                            // randomNumbers = Random.Range(MaxNumber, MinNumber);

                            //     list.Add(randomNumbers);
                                GameObject square = Instantiate(CardSquarePrefab) as GameObject; //instantiate my object
                                square.transform.SetParent(parent.transform); //set as parent of my parent object
                                
                                square.GetComponent<SquareValues>().Value = list[x*Rows+y]; //add value to my card
                                square.transform.localPosition  = new Vector2(x,-y); //create the numbers position

                        }
                    }

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

