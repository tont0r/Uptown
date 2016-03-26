using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnergyBar : MonoBehaviour {
    
    public Slider slider;
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        slider.value -= 1;
    }
    
   
}
