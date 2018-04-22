using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlowScript : MonoBehaviour {
    public const string beatMap =
    /*1*/    "0000000000000000" +
    /*2*/    "1001101010001000" +
    /*3*/    "1001100010011000" +
    /*4*/    "1001100010101010" +
    /*5*/    "1000000000000000" +
    /*6*/    "1001101010001000" +
    /*7*/    "1001100010011000" +
    /*8*/    "1001100010101010" +
    /*9*/    "1000000000000000" +
    /*10*/   "1111101011111000" +
    /*11*/   "1011101111111000" +
    /*12*/   "1111101011111000" +
    /*13*/   "1011111111111000" +
    /*14*/   "1000100010111000" +
    /*15*/   "1000100011111000" +
    /*16*/   "1010101010101010" +
    /*17*/   "1001100110011000" +
    /*18*/   "1001111111111000" +
    /*19*/   "1001101110011000" +
    /*20*/   "1111100010101111" +
    /*21*/   "1000000010001000" +
    /*22*/   "1001100011111010" +
    /*23*/   "1110000000111000" +
    /*24*/   "1001100010101010" +
    /*25*/   "1";
    public GameObject hitArea;
    public PlayerScript player;
    public static double finishTime;

	// Use this for initialization
	void Start () {
        float currentOffset = 0f;
        int laneZeroCounter = 0;
        int laneOneCounter = 0;
        DontDestroyOnLoad(gameObject);
        // clear previous sessions
        foreach(HitAreaScript hitArea in FindObjectsOfType<HitAreaScript>())
        {
            Destroy(hitArea.gameObject);
        }
        foreach (ItemScript item in FindObjectsOfType<ItemScript>())
        {
            Destroy(item.gameObject);
        }
        for (int i=0;i<beatMap.Length;i++)
        {
            if (beatMap[i] == '1')
            {
                GameObject newHitArea = Instantiate(hitArea, new Vector3(currentOffset, 0),Quaternion.identity, transform);
                if (newHitArea.GetComponent<HitAreaScript>().laneNumber == 0)
                {
                    if (laneZeroCounter > 2)
                    {
                        print("switchToLaneOne");
                        newHitArea.GetComponent<HitAreaScript>().StartCoroutine("Reposition");
                        laneOneCounter = 1;
                        print(laneOneCounter);
                        laneZeroCounter = 0;
                    }
                    else
                    {
                        laneZeroCounter++;
                        laneOneCounter = 0;
                    }
                    
                }
                else {
                    if (laneOneCounter > 2)
                    {
                        print("switchToLaneZero");
                        newHitArea.GetComponent<HitAreaScript>().StartCoroutine("Reposition");
                        laneZeroCounter = 1;
                        laneOneCounter = 0;
                    }
                    else
                    {
                        laneOneCounter++;
                        laneZeroCounter = 0;
                    }
                }
            }
            currentOffset += 1;
        }
        StartCoroutine("WaitToStart");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator WaitToStart()
    {
        while (player == null)
        {
            player = FindObjectOfType<PlayerScript>();
            yield return null;
        }
        while (!Input.anyKeyDown)
        {
            yield return null;
        }
        player.StartGame();
    }

}
