using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Controller : MonoBehaviour {
    public int speed = 5;
    private bool interacting;
    private GameObject npcObject;
    private bool talking;
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0);
        
        Vector3 newPosition = transform.position + (move * speed * Time.deltaTime);
        transform.position = newPosition;
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        
        if (interacting)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                npcObject.GetComponent<NpcController>().talkToPlayer();                        
                talking = true;
            }
        }   
    }

    public void beginInteraction(GameObject gameObject)
    {
        if (talking)
            return;
        interacting = gameObject != null;
        npcObject = gameObject;
    }

    public void endInteraction(GameObject gameObject)
    {
        NpcController newNpc = gameObject.GetComponent<NpcController>();
        NpcController currentNpc = (npcObject != null) ? npcObject.GetComponent<NpcController>() : null;
        if (currentNpc != null && newNpc.id != currentNpc.id)
            return;
        else
        {
            Text text = npcObject.GetComponentInChildren<Canvas>().GetComponentInChildren<Text>();
            text.enabled = false;
            interacting = false;
            talking = false;
            npcObject = null;
        }
    }
    
}
