using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BushScript : ItemScript {

	// Use this for initialization
	void Start () {
        player = FindObjectOfType<PlayerScript>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void PickUpEffect()
    {
        player.IncrementTime();
        GetComponent<Animator>().SetTrigger("touch");
        //gameObject.SetActive(false);
    }
}
