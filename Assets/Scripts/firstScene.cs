using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class firstScene : MonoBehaviour
{
    private void Awake()
    {
        //reset all playerprefs
        PlayerPrefs.DeleteAll();
    }
}
