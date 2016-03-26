using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnergyBar : MonoBehaviour {
    public string text;
    public int value;
    public Text textgameObject;
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        value -= 1;
        
        textgameObject.text = text + ": " + value;

    }
    
   
}
