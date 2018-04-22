using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitAreaScript : MonoBehaviour {
    public GameObject bushObject;
    public GameObject sharpObject;
    public GameObject naturalObject;
    public GameObject flatObject;

    public GameObject laneObject;
    public GameObject obj;
    public PlayerScript player;
    public bool isHit;

    public int laneNumber;

    // Use this for initialization
    void Awake () {
        player = FindObjectOfType<PlayerScript>();
        float rn;
        rn = Random.Range(0f, 18f);
        if (rn > 3)
        {
            laneObject = bushObject;
        }
        else if (rn > 2)
        {
            laneObject = naturalObject;
        }
        else if (rn > 1)
        {
            laneObject = sharpObject;
        }
        else
        {
            laneObject = flatObject;
        }
        // decide lane
        rn = Random.Range(0f, 2f);
        if (rn > 1f)
        {
            laneNumber = 1;
            obj = Instantiate(laneObject, transform.position + new Vector3(0.5f, 0.5f),Quaternion.identity,transform);
            obj.SetActive(true);
        }
        else
        {
            laneNumber = 0;
            obj = Instantiate(laneObject, transform.position + new Vector3(0.5f, -0.5f), Quaternion.identity,transform);
            obj.SetActive(true);
        }
	}

    internal IEnumerator Reposition()
    {
        while (obj == null) yield return null;
        laneNumber = (laneNumber == 1) ? 0 : 1;
        obj.transform.Translate(0, (laneNumber * 1f - 0.5f) * 2f, 0);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
