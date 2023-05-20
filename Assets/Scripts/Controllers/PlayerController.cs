using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    int OrderOnTable;

    public bool active = false;

    [SerializeField]
    TimerController timerController;

    [SerializeField]
    GameObject cards;

    [SerializeField]
    TextMeshPro username;

    [SerializeField]
    GameObject status;

    [SerializeField]
    GameObject EmptyPlayer;

    [SerializeField]
    Button SeeBtn;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(active){
            username.gameObject.SetActive(true);
            EmptyPlayer.SetActive(false);
            // status.SetActive(true);
            // cards.SetActive(true)
        }
        else{
            username.gameObject.SetActive(false);
            EmptyPlayer.SetActive(true);
        }
    }

    public void updateUserName(string name){
        username.SetText(name);
    }
    public void showTimer(int value){
        timerController.gameObject.SetActive(true);
        timerController.setTimer(value);
    }
    public void HideTimer(){
        timerController.gameObject.SetActive(false);
        timerController.setTimer(0);
    }

    public void EnableCards(){
        cards.SetActive(true);
        if(SeeBtn != null) SeeBtn.gameObject.SetActive(true);
    }    

}
