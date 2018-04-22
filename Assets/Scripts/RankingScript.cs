using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RankingScript : MonoBehaviour {
    public double timeRecord;
    public Text timeDisplay;
    public Text rankDisplay;
    public Text storyDisplay;

    public Image sTreasure;
    public Image aTreasure;
    public Image bTreasure;
    public Image cTreasure;
    public Image dTreasure;

    // Use this for initialization
    void Start () {
        timeRecord = GameFlowScript.finishTime;
        timeDisplay.text = timeRecord.ToString("F3");
        if (timeRecord < 40)
        {
            rankDisplay.text = "S";
            storyDisplay.text = "You reached the dungeon first before everyone else. You took everything you could with the crate and lived a wealthy life. Congrats!";
            sTreasure.gameObject.SetActive(true);
        }
        else if (timeRecord < 50)
        {
            rankDisplay.text = "A";
            storyDisplay.text = "You can tell that someone has been here before your arrival, but you still found a bag of shiny gems, which you decided to take.";
            aTreasure.gameObject.SetActive(true);
        }
        else if (timeRecord < 60)
        {
            rankDisplay.text = "B";
            storyDisplay.text = "As you ascended to the dungeon, you noticed that its treasure has been swept. You managed to take home some pieces of gems and coins scattering on the stairs.";
            bTreasure.gameObject.SetActive(true);
        }
        else if (timeRecord < 70)
        {
            rankDisplay.text = "C";
            storyDisplay.text = "You arrived a bit late. Most of the stuff had been taken, but you managed to grab a few gold coins that were hiding in the corners.";
            cTreasure.gameObject.SetActive(true);
        }
        else
        {
            rankDisplay.text = "D";
            storyDisplay.text = "You arrived too late. You looked around in every corner and found nothing except for a leftover sandwich brought by some other treasure hunter, and decided to call it a day.";
            dTreasure.gameObject.SetActive(true);
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.R)) SceneManager.LoadScene(0);
	}
}
