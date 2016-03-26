using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PersonSpawner : MonoBehaviour {
    public GameObject npcPrefab;
    public Sprite[] sprites;
    public List<GameObject> npcs;
    public int respawnTimer = 1000;

    public int timeUntilRespawn;
    public int currentNPC = 0;
    private int spawnedNPCs = 0;

    void Awake()
    {
        npcs = new List<GameObject>();
        timeUntilRespawn = respawnTimer;
    }

    public void destoryPerson(GameObject person)
    {
        npcs.Remove(person);
        DestroyObject(person);

    }

	void Update () {
        
        if (npcs.Count < 3)
        {
            timeUntilRespawn -= 1;
            if (timeUntilRespawn > 0)
                return;
            float direction = (currentNPC % 2 == 0 ? -1 : 1);
            Quaternion rotation = new Quaternion(0, direction == 1 ? 0 : 180f, 0, 0);
            int startingPosition = direction == -1 ? -10 : 10;
               
            GameObject person = (GameObject)Instantiate(npcPrefab, new Vector2(startingPosition, -2.25f), rotation); 
            person.GetComponent<SpriteRenderer>().sprite = sprites[currentNPC];
            NpcController npc = person.GetComponent<NpcController>();
            npc.setId(spawnedNPCs);
            npc.setDirection(startingPosition < 0?NpcController.Direction.RIGHT :NpcController.Direction.LEFT);
            if (currentNPC == 2)
                currentNPC = 0;
            spawnedNPCs++;
            currentNPC++;

            npcs.Add(person);
            timeUntilRespawn = respawnTimer;
        }
            
	}
}
