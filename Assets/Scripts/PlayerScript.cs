using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour {
    [Header("Speed Controls")]
    public int originalSpeed;
    public int speedLevel;

    [Header("Text Controls")]
    public Text timerDigits;
    public Text timerModDigits;
    public Text speedDigits;
    public Text startText;

    public double time;
    private double syncTimer;
    public AudioSource audioSource;
    public AudioSource soundEffectSource;
    public AudioSource itemEffectSource;
    public AudioClip missEffect;
    public AudioClip correctEffect;
    public AudioClip finishEffect;
    public int laneNumber;
    public bool canHit;
    public float stepSize;
    public HitAreaScript currentHitArea;

    public float originalX;
    public bool synced;
    public double syncTime;
    private Vector3 speedTextScale;
    private Vector3 modTextScale;

    public bool start;
    public bool end;

    internal void StartGame()
    {
        start = true;
        startText.gameObject.SetActive(false);
        GetComponent<Animator>().SetTrigger("start");
        enabled = true;
        audioSource.Play();
    }

    internal IEnumerator EndGame()
    {
        end = true;
        itemEffectSource.clip = finishEffect;
        itemEffectSource.Play();
        GetComponent<Animator>().SetTrigger("end");
        GameFlowScript.finishTime = time;
        enabled = false;
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(2);
    }
    // Use this for initialization
    void Start () {
        start = false;
        end = false;
        enabled = false;
        originalSpeed = 1;
        originalX = transform.position.x;
        syncTime = 3;
        speedTextScale = speedDigits.transform.localScale;
        modTextScale = timerModDigits.transform.localScale;
        //originalSpeed = FindObjectOfType<GameFlowScript>().originalSpeed;
    }
	
	// Update is called once per frame
	void Update () {
        if (start)
        {
            if (audioSource.isPlaying)
            {
                time += Time.deltaTime;
                timerDigits.text = time.ToString("F3");
            }
            else
            {
                if (Application.isFocused) StartCoroutine("EndGame");
            }
        }
        
        transform.Translate(audioSource.pitch * 4 * Time.deltaTime, 0, 0);
        syncTimer += Time.deltaTime;
        if (!synced && syncTimer > syncTime)
        {
            print("sync");
            Sync(syncTimer);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            StartCoroutine("MoveDown");
            ProcessHit();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            StartCoroutine("MoveUp");
            ProcessHit();
        }

        audioSource.pitch = originalSpeed + speedLevel * 0.1f;
        GetComponent<Animator>().speed = originalSpeed + speedLevel * 0.1f;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // hit area
        print("meetHitArea");
        if (other.tag == "hitArea") { canHit = true; currentHitArea = other.GetComponent<HitAreaScript>(); }
        // item
        else if (other.tag == "item") {
            itemEffectSource.clip = other.GetComponent<ItemScript>().soundEffect;
            itemEffectSource.Play();
            other.GetComponent<ItemScript>().PickUpEffect();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "hitArea") {
            canHit = false;
        }
    }

    public void IncrementTime()
    {
        time += 1;
        timerModDigits.text = "+1s";
        StartCoroutine("ModTextJump");
    }

    IEnumerator SpeedTextJump()
    {
        float multiplier = 1.3f;
        while (Mathf.Abs(multiplier - 1f) > 1e-3)
        {
            multiplier -= 0.05f;
            speedDigits.transform.localScale = speedTextScale * multiplier;
            yield return null;
        }
    }

    IEnumerator ModTextJump()
    {
        yield return new WaitForSeconds(1.5f);
        timerModDigits.text = "";
    }

    internal void LowerSpeedLevel()
    {
        speedLevel = Mathf.Max(-5, speedLevel - 1);
        speedDigits.text = "Speed: x" + (speedLevel * 0.1 + originalSpeed).ToString("F1");
        StartCoroutine("SpeedTextJump");
    }

    internal void NeutralizeSpeedLevel()
    {
        if (speedLevel > 1e-2)
        {
            speedLevel -= 1;
        }
        else if (speedLevel < -1e-2)
        {
            speedLevel += 1;
        }
        speedDigits.text = "Speed: x" + (speedLevel * 0.1 + originalSpeed).ToString("F1");
        StartCoroutine("SpeedTextJump");
    }

    internal void RaiseSpeedLevel()
    {
        speedLevel = Mathf.Min(5, speedLevel + 1);
        speedDigits.text = "Speed: x" + (speedLevel * 0.1 + originalSpeed).ToString("F1");
        StartCoroutine("SpeedTextJump");
    }

    internal IEnumerator MoveDown()
    {
        laneNumber = 0;
        while (Mathf.Abs(transform.position.y - laneNumber + 0.5f) > 1e-3)
        {
            transform.Translate(0f, -stepSize, 0f);
            yield return null;
        }
    }

    internal IEnumerator MoveUp()
    {
        laneNumber = 1;
        while (Mathf.Abs(transform.position.y - laneNumber + 0.5f) > 1e-3)
        {
            transform.Translate(0f, stepSize, 0f);
            yield return null;
        }
    }

    internal void ProcessHit()
    {
        if (canHit)
        {
            CorrectHit();
        }
        else
        {
            print("miss becuase hit on blank");
            Miss();
        }
    }

    internal void Miss()
    {
        print("Miss");
        soundEffectSource.clip = missEffect;
        soundEffectSource.Play();
        time += 3;
        timerModDigits.text = "+3s";
        StartCoroutine("ModTextJump");
    }

    internal void CorrectHit()
    {
        print("correct hit");
        currentHitArea = null;
        soundEffectSource.clip = correctEffect;
        soundEffectSource.Play();
        time -= 0.5;
        timerModDigits.text = "-0.5s";
        StartCoroutine("ModTextJump");
    }

    internal void Sync(double time)
    {
        transform.Translate(originalX + (float) time * 4 - transform.position.x, 0, 0);
        synced = true;
    }
}
