using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class balls : MonoBehaviour
{
    public Sprite[] ball;
    public int Value;

    public Animator anim; 
    public int SecondsToAnimationStop;

    public AudioSource audio;
    // Start is called before the first frame update
    void Start(){
        StartCoroutine(AnimationStop());
    }

    public void updateValueBall(){
        this.GetComponent<SpriteRenderer>().sprite = ball[Value - 1];
    }

    IEnumerator AnimationStop(){
        yield return new WaitForSeconds(SecondsToAnimationStop);
        anim.enabled = false;
        audio.Play(0);
        updateValueBall();
    }
}
