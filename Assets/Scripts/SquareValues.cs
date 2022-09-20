using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SquareValues : MonoBehaviour
{
    public int Value;
    public GameObject checkedImage;

    public bool checkedInfo;
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<TextMesh>().text = Value.ToString();
    }

    public void TurnChecked(){
        checkedImage.SetActive(true);
    }
}
