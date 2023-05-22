using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsController : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer Card1;

    [SerializeField]
    SpriteRenderer Card2;

    [SerializeField]
    SpriteRenderer Card3;

    [SerializeField]
    Sprite[] cardSpritesheet;

    Dictionary<string,Sprite> cardSprites;

    List<SpriteRenderer> Cards;
    

    // Start is called before the first frame update
    void Start()
    {
        Cards = new List<SpriteRenderer>(){Card1,Card2,Card3};
        cardSprites = new Dictionary<string, Sprite>();
        for (int i = 0; i < cardSpritesheet.Length; i++)
        {
            cardSprites.Add(cardSpritesheet[i].name, cardSpritesheet[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setCardsTexture(string[] cards){
        if(Card1.sprite.name == "00"){  
            for(int i = 0;i< cards.Length;i++)
            {
                Cards[i].sprite = GetSpriteByName(cards[i]);
            }
        }
    }

    public void resetCardsTexture(){
        Cards.ForEach((_card)=>{
            _card.sprite = GetSpriteByName("00");
        });
    }

    Sprite GetSpriteByName(string name) {
        if (cardSprites.ContainsKey(name))
            return cardSprites[name];
        else 
            return cardSprites["00"];
    }
}
