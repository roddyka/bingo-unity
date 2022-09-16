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
    public int Counter = 0; //number of credits
    public GameObject GridManager; //gridmanager to adjust the card numbers

    public int secondsToShowTheBalls; //seconds to show the balls
    public List<int> list = new List<int>(); //list to the sort numbers
    public List<int> numbersChecked = new List<int>(); //list to see how many numbers are sorted to the player
    public int TotalToSort; //sum the number of coluns and lines choosed on gridmanager;
    public GameObject SortedManager;
    //to instantiate the prefab balls 
    public GameObject sortedBallObj;
    // to check if its real bingo or fake
    public bool bingoButton = false;
    public bool statusOfBingoChecking = false;

    //audios to the game
    public AudioSource congratulationsSong;
    public AudioSource dirtyGame;
    public AudioSource InsertCreditAudio;
    public AudioSource bingoSong;

    private int randomNumbers; //to randomize the sort balls
    private GameObject sortedBall;
    private IEnumerator coroutine;

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
        if(list.Count >= TotalToSort){
            bingoButton = true;

            if(bingoButton){
                CanvasItems[6].SetActive(true);
                bingoButton = false;
            }
        }
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
        InsertCreditAudio.Play(0);
        //inserting credits function
        Credits++; //adding +1 to credits
        CreditText.text = "Credits: "+Credits.ToString(); //change the credits text
        if(Credits > 0){
            CanvasItems[1].SetActive(true);
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
        InsertCreditAudio.Play(0);
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
            checkNumbers(randomNumbers);
            //instantiating the balls objects to show the numbers to the user
            GameObject sortedBall = Instantiate(sortedBallObj) as GameObject;
            sortedBall.transform.SetParent(SortedManager.transform);
            sortedBall.transform.localPosition  = new Vector2(i, 0f);

            sortedBall.GetComponent<balls>().Value = randomNumbers;
            // sortedBall.GetComponent<balls>().updateValueBall();
            //falta parar a animação e trocar o sprite das bolas para o numero correto e ajustar as bolas para nao sair da tela
        }
    }

    //check if is the same numbers and mark with X
    public void checkNumbers(int randomNumbers){
        foreach (Transform item in GridManager.transform.GetChild(0))
        {
            if(randomNumbers == item.GetComponent<SquareValues>().Value){
                // Debug.Log("SameNumber:"+ randomNumbers + "and "+ item.GetComponent<SquareValues>().Value);
                item.GetComponent<SquareValues>().TurnChecked();
                item.GetComponent<SquareValues>().checkedInfo = true;
                numbersChecked.Add(1);
            }
        }
    }

    //check if its real bingo or not when the sortballs is finished
    public void Bingo(){
        CanvasItems[6].GetComponent<Button>().interactable = false;
        TitleText.text = "Checking your results";
        CanvasItems[0].SetActive(true);
        checkWinnerOrLooser();
    }

    public void checkWinnerOrLooser(){
         if(numbersChecked.Count >= TotalToSort){
            statusOfBingoChecking = true;
            StartCoroutine(itsBingo());
         }else{
            Debug.Log("Fake Bingo!");
            StartCoroutine(itsBingo());
         }
    }

    //verify if its a real bingo or not
    IEnumerator itsBingo()
    {
        yield return new WaitForSeconds(1);
        if(statusOfBingoChecking){
            TitleText.text = "Congratulations! ITS BINGO!!!!";
            congratulationsSong.Play(0);
            bingoSong.Play(0);
        }else{
            TitleText.text = "Your are not the Winner!";
            dirtyGame.Play(0);
        }
        Counter++;
        CounterText.text = "Moves: "+Counter;
    }

    public void playAgain(){
        TitleText.text = "OH GOD ITS BINGO DAY!";
        CanvasItems[3].SetActive(true);

        CanvasItems[6].SetActive(false);
        GridManager.SetActive(false);
        SortedManager.SetActive(false);
        list.Clear();
        numbersChecked.Clear();
        GridManager.GetComponent<GridManager>().clearList();
    }

    public void cleanParents(){
        //limpar parent das bolas sorteadas
    }

}
