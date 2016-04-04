using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Fungus;

public class Controller : MonoBehaviour {
    public int speed = 5;
    public Flowchart flowChart;
    public Text moneyText;
    public Flowchart dialogFlowchart;
    public Clock clock;


    private int money = 50;
    private bool inFlowChart;
    
    private bool interacting;
    private GameObject npcObject;
    private bool talking;

	void Start () {
        flowChart.SetIntegerVariable("money",money);        
        updateMoney();
    }
	
	// Update is called once per frame
	void Update () {
        if (inFlowChart)
            return;
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
                clock.paused = true;
                dialogFlowchart.SendFungusMessage("hi");

                npcObject.GetComponent<NpcController>().talkToPlayer();                        
                talking = true;
            }
        }   
    }

    public void beginInteraction(GameObject gameObject)
    {
        if (talking) {
            dialogFlowchart.enabled = false;
            return;
        }
       // 
        interacting = gameObject != null;
        npcObject = gameObject;
    }

    public void endDialogWithFungus(bool affectClock)
    {
        NpcController currentNpc = (npcObject != null) ? npcObject.GetComponent<NpcController>() : null;
        if (currentNpc != null)
            currentNpc.continueWalking();

        if (affectClock)
            clock.paused = false;
        dialogFlowchart.enabled = false;
        interacting = false;
        talking = false;
        npcObject = null;
    }

    public void endInteraction(GameObject gameObject)
    {
        NpcController newNpc = gameObject.GetComponent<NpcController>();
        NpcController currentNpc = (npcObject != null) ? npcObject.GetComponent<NpcController>() : null;
        dialogFlowchart.enabled = false;


        if (currentNpc != null && newNpc != null && newNpc.id != currentNpc.id)
            return;
        else
        {        
            if (npcObject != null)
            {
                Text text = npcObject.GetComponentInChildren<Canvas>().GetComponentInChildren<Text>();
                text.enabled = false;
                
            }
            endDialogWithFungus(false);
        }
    }

    public void boughtItem(int cost)
    {   
        money -= cost;
        updateMoney();
    }

    public void setInFlowChart(bool inFlowChart)
    {
        this.inFlowChart = inFlowChart;
    }

    private void updateMoney()
    {
        moneyText.text = "Money :" + money;
        flowChart.SetIntegerVariable("money", money);
    }

    public void collectMoney(int money,int time)
    {
        this.money += money;
        updateMoney();
        clock.updateTime(time);
        Debug.Log(time);
    }

    public void resetPlayer()
    {
        transform.position = new Vector3(-20, transform.position.y);
    }
    
}
