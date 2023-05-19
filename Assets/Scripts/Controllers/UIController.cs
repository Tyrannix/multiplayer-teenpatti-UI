using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{

    public static UIController instance {get; private set;}

    // [SerializeField]
    // public ModalControllers modal;
    
    [SerializeField]
    GameObject loader;
    
    [SerializeField]
    NoesisView MainCamera;

    

    private void Awake(){
        if(instance != null){
            Debug.Log("More than Instance available");
        }
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        MainCamera.enabled = false;
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
}
