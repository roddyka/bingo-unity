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
    public TextMeshProUGUI TitleText; //title Text
    public TextMeshProUGUI CreditText; //Credit Text
    public TextMeshProUGUI CounterText; //Counter Text
    public int Credits = 0; //number of credits
    public int Counter = 0; //number of credits
    public GameObject GridManager; //gridmanager to adjust the card numbers

    public int secondsToShowTheBalls; //seconds to show the balls
    public List<int> list = new List<int>(); //list to the sort numbers
    public List<int> numbersChecked = new List<int>(); //list to see how many numbers are sorted to the player
    private int TotalToSort = 30; //sum the number of coluns and lines choosed on gridmanager;
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
        //player prefs is to storage info and send between scenes, im using to send the coins and counter numbers
        Credits = PlayerPrefs.GetInt("coins");
        CreditText.text = "Credits: " + Credits.ToString();

        Counter = PlayerPrefs.GetInt("counter");
        CounterText.text = "Moves: " + Counter.ToString();

        TitleText.text = "OH GOD ITS BINGO DAY!";

        coroutine = randomNumbersToBingo();

        if (Credits > 0)
        {
            CanvasItems[1].SetActive(true);
            //verify if the credits is avaliable
            CanvasItems[3].SetActive(false); //Hide insert credit button
            TitleText.text = "Now its time to generate your card"; //change the title text sending instructions
            CanvasItems[4].SetActive(true); //active the generate card button
        }
        else
        {
            PreviousNextScene();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (list.Count >= TotalToSort)
        {
            bingoButton = true;

            if (bingoButton)
            {
                CanvasItems[6].SetActive(true);
                bingoButton = false;
            }
        }
    }

    //loading next Scene to start the game
    public void NextScene()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (SceneManager.sceneCountInBuildSettings > nextSceneIndex)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }

        if (Credits > 0 && SceneManager.GetActiveScene() == SceneManager.GetSceneByName("WinOrNot"))
        {
            PreviousNextScene();
        }
    }
    public void PreviousNextScene()
    {
        int previousSceneIndex = SceneManager.GetActiveScene().buildIndex - 1;
        if (SceneManager.sceneCountInBuildSettings > previousSceneIndex)
        {
            SceneManager.LoadScene(previousSceneIndex);
        }
    }

    private void removeCredits()
    {
        Credits--;
        PlayerPrefs.SetInt("coins", Credits);
        CreditText.text = "Credits: " + Credits.ToString(); //update the text
    }

    public void canvasItemsOnGeneratingCards()
    {
        CanvasItems[4].SetActive(false); //desactivate the generate card button
        CanvasItems[0].SetActive(false); //desactivate the title
        GridManager.SetActive(true); //set active the card game
        CanvasItems[5].SetActive(true); //set active the play button
    }

    public void PlayGame()
    {
        InsertCreditAudio.Play(0);
        CanvasItems[5].SetActive(false);
        removeCredits();
        StartCoroutine(coroutine);
    }

    IEnumerator randomNumbersToBingo()
    {
        for (int i = 0; i < 10; i++)
        {
            for (int y = 0; y < 3; y++)
            {
                yield return new WaitForSeconds(secondsToShowTheBalls);
                randomNumbers = Random.Range(GridManager.GetComponent<GridManager>().MaxNumber, GridManager.GetComponent<GridManager>().MinNumber);
                list.Add(randomNumbers);
                checkNumbers(randomNumbers);
                //instantiating the balls objects to show the numbers to the user
                GameObject sortedBall = Instantiate(sortedBallObj) as GameObject;
                sortedBall.transform.SetParent(SortedManager.transform);

                sortedBall.transform.localPosition = new Vector2(i, y);


                sortedBall.GetComponent<balls>().Value = randomNumbers;
            }

            // sortedBall.GetComponent<balls>().updateValueBall();
            //falta parar a animação e trocar o sprite das bolas para o numero correto e ajustar as bolas para nao sair da tela
        }
    }

    //check if is the same numbers and mark with X
    public void checkNumbers(int randomNumbers)
    {
        foreach (Transform item in GridManager.transform.GetChild(0))
        {
            if (randomNumbers == item.GetComponent<SquareValues>().Value)
            {
                item.GetComponent<SquareValues>().TurnChecked();
                item.GetComponent<SquareValues>().checkedInfo = true;
                numbersChecked.Add(1);
            }
        }
    }

    //check if its real bingo or not when the sortballs is finished
    public void Bingo()
    {
        CanvasItems[6].GetComponent<Button>().interactable = false;
        TitleText.text = "Checking your results";
        CanvasItems[0].SetActive(true);
        checkWinnerOrLooser();
    }

    private void checkWinnerOrLooser()
    {
        if (numbersChecked.Count >= TotalToSort)
        {
            statusOfBingoChecking = true;
            StartCoroutine(itsBingo());
        }
        else
        {
            StartCoroutine(itsBingo());
        }
    }

    //verify if its a real bingo or not
    IEnumerator itsBingo()
    {
        yield return new WaitForSeconds(1);
        if (statusOfBingoChecking)
        {
            PlayerPrefs.SetInt("StatusOfTheGame", 0); //0 to winner
            PlayerPrefs.SetString("title", "Congratulations! ITS BINGO!!!!");
            NextScene();
        }
        else
        {
            PlayerPrefs.SetInt("StatusOfTheGame", 1); //1 to loose
            PlayerPrefs.SetString("title", "Your are not the Winner!!!");
            NextScene();
        }
        Counter++;
        PlayerPrefs.SetInt("counter", Counter);
    }

    public void playAgain()
    {
        TitleText.text = "OH GOD ITS BINGO DAY!";
        CanvasItems[3].SetActive(true);

        CanvasItems[6].SetActive(false);
        GridManager.SetActive(false);
        // SortedManager.SetActive(false);
        list.Clear();
        numbersChecked.Clear();
        cleanSortedNumbersObjects();
        GridManager.GetComponent<GridManager>().destroyObjectList();
    }

    public void cleanSortedNumbersObjects()
    {
        //Clear sorted numbers objects
        foreach (Transform child in SortedManager.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void insertCoins()
    {
        Credits++;
        CreditText.text = "Credits: " + Credits.ToString();
        PlayerPrefs.SetInt("coins", Credits);

        InsertCreditAudio.Play(0);
        if (Credits > 0)
        {
            CanvasItems[3].GetComponent<Button>().interactable = true;
        }
    }

    public void WinOrNotCS()
    {
        CanvasItems[0].gameObject.GetComponent<Animator>().enabled = false;
        CanvasItems[3].GetComponent<Button>().interactable = true;
    }
}

