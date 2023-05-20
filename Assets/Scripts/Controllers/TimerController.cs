using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerController : MonoBehaviour
{
    // Start is called before the first frame update

    private Material material;

    [SerializeField]
    [Range(0,30)]
    int timer;

    public int maxTimer = 30;

    void Start()
    {
        material = GetComponent<SpriteRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        material.SetInt("_Arc1",timer);
    }

    public void setTimer(int value){
        timer = 360 - (value * (360/maxTimer));  
    }
}
