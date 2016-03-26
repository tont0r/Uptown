﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NpcController : MonoBehaviour {
    public float speed;
    public GameObject prefabDialogWindow;

    private GameObject dialogWindow;
    private Direction pausedDirection;
    public int id;

    public enum Direction
    {
        LEFT,
        RIGHT,
        STANDING
    };
    public Direction direction;
	// Use this for initialization
	void Start () {
        dialogWindow = (GameObject)Instantiate(prefabDialogWindow, new Vector3(transform.position.x,transform.position.y+1,0), Quaternion.identity);
        dialogWindow.transform.SetParent(gameObject.transform);        
    }

    public void setId(int id)
    {
        this.id = id;
    }

    public void setDirection(Direction direction)
    {
        this.direction = direction;
    }
    
	void Update () {
        if (direction == Direction.STANDING)
            return;
        float d = (direction == Direction.LEFT) ? -1f : 1f;
        transform.position = transform.position + (new Vector3(d,0) * speed * Time.deltaTime);
        transform.localRotation = Quaternion.Euler(0, direction == Direction.LEFT ? 0 : 180, 0);
        if (transform.position.x < -20 || transform.position.x > 20)
        {
            GameObject.FindGameObjectWithTag("NPCSpawner").GetComponent<PersonSpawner>().destoryPerson(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag != "Player")
            return;
        Controller controller = other.gameObject.GetComponent<Controller>();
        if (controller != null)
        {
            controller.beginInteraction(gameObject);
            pausedDirection = direction;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag != "Player")             
            return;
        
        Controller controller = other.gameObject.GetComponent<Controller>();
        if (controller != null)
            controller.endInteraction(gameObject);

        direction = pausedDirection;
    }


    public void talkToPlayer()
    {
        Text text = gameObject.GetComponentInChildren<Canvas>().GetComponentInChildren<Text>();
        NpcController npc = gameObject.GetComponent<NpcController>();
        npc.setDirection(NpcController.Direction.STANDING);
        text.enabled = true;
    }
}
