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
    TextMeshPro statusText;

    [SerializeField]
    GameObject EmptyPlayer;

    [SerializeField]
    ParticleSystem winAnimation;

    [SerializeField]
    Button SeeBtn;

    [SerializeField]
    Button showBtn;
    
    [SerializeField]
    Button sideShowBtn;
    
    [SerializeField]
    TextMeshProUGUI balance;

    [SerializeField]
    TextMeshProUGUI chaalText;

    [SerializeField]
    TextMeshProUGUI chaal2XText;

    [SerializeField]
    CardsController cardsController;
    

    private Player player;

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
        SetBtnText();
        SetStatusText();
        SwitchButton();
    }

    public void setPlayer(Player player)
    {
        this.player = player;
    }

    public void updateUserName()
    {
        username.SetText(player.playerID);
    }
    public void showTimer(int value)
    {
        timerController.gameObject.SetActive(true);
        timerController.setTimer(value);
    }
    public void HideTimer()
    {
        timerController.gameObject.SetActive(false);
        timerController.setTimer(0);
    }

    public void EnableCards()
    {
        if(player != null && !player.fold)
        {
            cards.SetActive(true);
            if(SeeBtn != null) SeeBtn.gameObject.SetActive(true);
        }
        status.SetActive(true);
    }    

    public void UpdateBalance()
    {
        if(balance != null)
        {
            balance.SetText($"Balance\n{player.balance}");
        }
    }

    public void startWinAnimation(){
        winAnimation.Play();
    }

    public void stopWinAnimation()
    {
        winAnimation.Stop();
    }
    
    void SetStatusText(){
        if(player != null && player.cards != null)
        {
            if(player.fold){
                statusText.SetText("Packed");
                cards.SetActive(false);
                if(SeeBtn != null)
                { 
                    SeeBtn.gameObject.SetActive(false);
                }
            }
            else if(player.blind)
            { 
                statusText.SetText("Blind");
                hideCards();
            }
            else
            { 
                statusText.SetText("Seen");
                if(SeeBtn != null)
                { 
                    SeeBtn.gameObject.SetActive(false);
                    showCards();
                }
            }
        }
    }

    public void showCards(){
        cardsController.setCardsTexture(player.cards);
    }
    void hideCards(){
        cardsController.resetCardsTexture();
    }

    void SetBtnText()
    {
        if(player != null && chaalText != null && player.blind)
        {
            chaalText.SetText($"Blind\n{GameController.minStake/2}");
            chaal2XText.SetText($"Blind (2x)\n{GameController.minStake}");
        }
        else if(player != null && chaalText != null && !player.blind)
        {
            chaalText.SetText($"Chaal\n{GameController.minStake}");
            chaal2XText.SetText($"Chaal (2x)\n{GameController.minStake * 2}");
        }
    }

    void SwitchButton()
    {
        if(showBtn != null && sideShowBtn != null)
        {
            
            if(GameController.CountAlivePlayers() == 2)
            {
                showBtn.gameObject.SetActive(true);
                sideShowBtn.gameObject.SetActive(false);
            }
            else
            {
                showBtn.gameObject.SetActive(false);
                sideShowBtn.gameObject.SetActive(true);
            }
        }
    }

}
