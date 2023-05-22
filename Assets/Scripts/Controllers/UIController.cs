using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    public static UIController instance {get; private set;}

    [SerializeField]
    public ModalControllers modal;
    
    [SerializeField]
    GameObject loader;
    
    [SerializeField]
    NoesisView MainCamera;

    [SerializeField]
    public Text txt;
    

    private void Awake(){
        if(instance != null){
            Debug.Log("More than Instance available");
        }
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        // showToast("Hello",2);
        if(MainCamera !=null) MainCamera.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void toggleLoader(bool value){
        // loader.SetActive(value);
        MainCamera.enabled = value;
        GetComponent<Canvas>().enabled = !value;
    }

    public void showToast(string text,int duration = 2)
    {
        StartCoroutine(showToastCOR(text, duration));
    }

    private IEnumerator showToastCOR(string text,int duration)
    {
        Color orginalColor = txt.color;

        txt.text = text;
        txt.enabled = true;

        //Fade in
        yield return fadeInAndOut(txt, true, 0.5f);

        //Wait for the duration
        float counter = 0;
        while (counter < duration)
        {
            counter += Time.deltaTime;
            yield return null;
        }

        //Fade out
        yield return fadeInAndOut(txt, false, 0.5f);

        txt.enabled = false;
        txt.color = orginalColor;
    }

    IEnumerator fadeInAndOut(Text targetText, bool fadeIn, float duration)
    {
        //Set Values depending on if fadeIn or fadeOut
        float a, b;
        if (fadeIn)
        {
            a = 0f;
            b = 1f;
        }
        else
        {
            a = 1f;
            b = 0f;
        }

        Color currentColor = txt.color;
        float counter = 0f;

        while (counter < duration)
        {
            counter += Time.deltaTime;
            float alpha = Mathf.Lerp(a, b, counter / duration);

            targetText.color = new Color(currentColor.r, currentColor.g, currentColor.b, alpha);
            yield return null;
        }
    }

}
