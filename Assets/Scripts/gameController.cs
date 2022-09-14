using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    public static GameController instance; //instance to call this item on anothers script
    public GameObject[] CanvasItems; //UI objects
    public TextMeshProUGUI TitleText; //Credit Text
    public TextMeshProUGUI CreditText; //Credit Text
    public TextMeshProUGUI CounterText; //Counter Text
    public int Credits = 0; //number of credits
    public GameObject GridManager;

    private int randomNumbers;
    public int secondsToShowTheBalls;
    public List<int> list = new List<int>();
    private IEnumerator coroutine;

 

    public int TotalToSort; //sum the number of coluns and lines choosed on gridmanager;

    // Start is called before the first frame update
    void Start()
    {
        Credits = 0;
        CreditText.text = "Credits: "+Credits.ToString();
        TitleText.text = "OH GOD ITS BINGO DAY!";

        coroutine = randomNumbersToBingo();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //loading next Scene to start the game
    public void NextScene(){
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if(SceneManager.sceneCountInBuildSettings > nextSceneIndex)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
    }

    public void InsertCredit(){
        //inserting credits function
        Credits++; //adding +1 to credits
        CreditText.text = "Credits: "+Credits.ToString(); //change the credits text
        if(Credits > 0){
            //verify if the credits is avaliable
            CanvasItems[3].SetActive(false); //Hide insert credit button
            TitleText.text = "Now its time to generate your card"; //change the title text sending instructions
            CanvasItems[4].SetActive(true); //active the generate card button
        }
    }

    public void removeCredits(){
        Credits--; //remove coins
        CreditText.text = "Credits: "+Credits.ToString(); //update the text
    }

    public void canvasItemsOnGeneratingCards(){
        CanvasItems[4].SetActive(false); //desactivate the generate card button
        CanvasItems[0].SetActive(false); //desactivate the title
        GridManager.SetActive(true); //set active the card game
        CanvasItems[5].SetActive(true); //set active the play button
    }

    public void PlayGame(){
        CanvasItems[5].SetActive(false);
        removeCredits();
        
        // Debug.Log(SumOfColumnsAndLines);
        StartCoroutine(coroutine);
    }

    IEnumerator randomNumbersToBingo()
    {
        for (int i = 0; i < TotalToSort; i++)
        {
            yield return new WaitForSeconds(secondsToShowTheBalls);
            randomNumbers = Random.Range(GridManager.GetComponent<GridManager>().MaxNumber, GridManager.GetComponent<GridManager>().MinNumber);
            list.Add(randomNumbers);
            teste(randomNumbers);
        }
    }

    public void teste(int randomNumbers){
        foreach (Transform item in GridManager.transform.GetChild(0))
        {
            if(randomNumbers == item.GetComponent<SquareValues>().Value){
                Debug.Log("SameNumber:"+ randomNumbers + "and "+ item.GetComponent<SquareValues>().Value);
                item.GetComponent<SquareValues>().TurnChecked();
            }
        }
    }
}
