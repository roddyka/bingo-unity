using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class gameController : MonoBehaviour
{
    public static gameController instance; //instance to call this item on anothers script
    public GameObject[] CanvasItems; //UI objects
    public TextMeshProUGUI TitleText; //Credit Text
    public TextMeshProUGUI CreditText; //Credit Text
    public TextMeshProUGUI CounterText; //Counter Text
    public int Credits = 0; //number of credits

    // Start is called before the first frame update
    void Start()
    {
        Credits = 0;
        CreditText.text = "Credits: "+Credits.ToString();
        TitleText.text = "OH GOD ITS BINGO DAY!";
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
}
