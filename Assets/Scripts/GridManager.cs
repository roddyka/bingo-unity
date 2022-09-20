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
    }

    public void generateCard()
    {
        // GameController.instance.Credits - for some reason i cant use this - need to be checked
        if (GameController.GetComponent<GameController>().Credits >= 1)
        {
            GameController.GetComponent<GameController>().canvasItemsOnGeneratingCards();

            // Preciso verificar se a lista ja contem o numero e busar na lista or numeros em ordem para criar a minha carta de bingo
            while (list.Count < 15)
            {
                randomNumbers = Random.Range(MaxNumber, MinNumber);
                if (!list.Contains(randomNumbers))
                {
                    list.Add(randomNumbers);
                }
            }

            //order the list
            list = list.OrderBy(x => x).ToList();

            int count = 0;
            //original and working
            for (int y = 0; y < Rows; y++)
            { //for Y
                for (int x = 0; x < Columns; x++)
                { //for X
                    var listInfo = list[count];
                    count++;

                    GameObject square = Instantiate(CardSquarePrefab) as GameObject; //instantiate my object
                    square.transform.SetParent(parent.transform); //set as parent of my parent object

                    square.GetComponent<SquareValues>().Value = listInfo; //add value to my card
                    square.transform.localPosition = new Vector3(x, -y, 0f); //create the numbers position

                }
            }

        }
    }

    //Clear the card list and destroy parent objects
    public void destroyObjectList()
    {
        list.Clear();
        foreach (Transform child in parent.transform)
        {
            Destroy(child.gameObject);
        }
    }
}


