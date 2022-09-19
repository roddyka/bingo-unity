using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class WinOrnot : MonoBehaviour
{
    public AudioSource congratulationsSong;
    public AudioSource dirtyGame;
    public AudioSource bingoSong;
    public TextMeshProUGUI TitleText; //Credit Text

    public GameObject GameController;

    public GameObject[] CanvasItems;
    // Start is called before the first frame update
    void Start()
    {
        //enter on game controller and check if have credits or 
        GameController.GetComponent<GameController>().WinOrNotCS();

        if(PlayerPrefs.GetInt("StatusOfTheGame") <= 0){
            bingoSong.Play(0);
            TitleText.text = PlayerPrefs.GetString("title");
            congratulationsSong.Play(0);
        }else{
            TitleText.text = PlayerPrefs.GetString("title");
            dirtyGame.Play(0);
        }

        if(PlayerPrefs.GetInt("coins") <= 0){
            CanvasItems[0].GetComponent<Button>().interactable = false;
        }

    }

}
