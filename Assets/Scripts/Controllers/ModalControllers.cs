using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class ModalControllers : MonoBehaviour
{
    [SerializeField]
    public TextMeshProUGUI HeaderText;

    [SerializeField]
    private Button yesBtn;

    [SerializeField]
    private Button noBtn;
    
    [SerializeField]
    public Image backdrop;

    private Action onYes;
    private Action onNo;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CloseModal(){
        gameObject.SetActive(false);
    }

    public void Confirm(){
        onYes?.Invoke();
        CloseModal();
    }

    public void Decline(){
        onNo?.Invoke();
        CloseModal();
    }

    public void ShowModal(string title,Action okAction = null,Action noAction = null){
        gameObject.SetActive(true);
        HeaderText.SetText(title);
        if(okAction != null){
            yesBtn.gameObject.SetActive(true);
            onYes = okAction;
        }
        if(noAction != null){
            noBtn.gameObject.SetActive(true);
            onNo = noAction;
        }
        else{
            noBtn.gameObject.SetActive(false);
        }
        
    }
}
