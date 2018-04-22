using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaturalScript : ItemScript {
    public override void PickUpEffect()
    {
        player.NeutralizeSpeedLevel();
        Destroy(gameObject);
    }

    // Use this for initialization
    void Start () {
        player = FindObjectOfType<PlayerScript>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}


}
