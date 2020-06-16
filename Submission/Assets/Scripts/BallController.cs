using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BallController : MonoBehaviour
{
    public int force;
    Rigidbody2D rigid;
    int scoreP1;
    int scoreP2;
    Text scoreUIP1;
    Text scoreUIP2;
    GameObject panelSelesai;
    GameObject panelPause;
    Text txPemenang;
    AudioSource audio;
    public AudioClip hitSound;
    public AudioClip hitPaddle;
    GameObject paddleBlue;
    GameObject paddleRed;

    // Use this for initialization
    void Start()
    {
        scoreP1 = 0;
        scoreP2 = 0;
        scoreUIP1 = GameObject.Find("Score1").GetComponent<Text>();
        scoreUIP2 = GameObject.Find("Score2").GetComponent<Text>();
        panelSelesai = GameObject.Find("PanelSelesai");
        panelSelesai.SetActive(false);
        panelPause = GameObject.Find("PanelPause");
        panelPause.SetActive(false);
        audio = GetComponent<AudioSource>();
        StartCoroutine(StartPlay());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!panelPause.activeInHierarchy)
            {
                GamePause();
            }
            if (panelPause.activeInHierarchy)
            {
                GameLanjut();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.name == "TepiKanan" || coll.gameObject.name == "TepiKiri" || coll.gameObject.name == "TepiAtas" || coll.gameObject.name == "TepiBawah")
        {
            audio.PlayOneShot(hitSound);
        }
        if (coll.gameObject.name == "GoalKanan")
        {
            scoreP1 += 1;
            TampilkanScore();
            if (scoreP1 == 5)
            {
                panelSelesai.SetActive(true);
                txPemenang = GameObject.Find("Pemenang").GetComponent<Text>();
                txPemenang.text = "<color=#485FFF>Player Biru Pemenang!</color>";
                Destroy(gameObject);
                return;
            }
            StartCoroutine(ResetBallBlue());
        }
        if (coll.gameObject.name == "GoalKiri")
        {
            scoreP2 += 1;
            TampilkanScore();
            if (scoreP2 == 5)
            {
                panelSelesai.SetActive(true);
                txPemenang = GameObject.Find("Pemenang").GetComponent<Text>();
                txPemenang.text = "<color=#FF4747>Player Merah Pemenang!</color>";
                Destroy(gameObject);
                return;
            }
            StartCoroutine(ResetBallRed());
        }
        if (coll.gameObject.name == "Sword" || coll.gameObject.name == "Spear")
        {
            audio.PlayOneShot(hitPaddle);
            float sudut = (transform.position.y - coll.transform.position.y) * 5f;
            Vector2 arah = new Vector2(rigid.velocity.x, sudut).normalized;
            rigid.velocity = new Vector2(0, 0);
            rigid.AddForce(arah * force * 2);
        }
    }

    IEnumerator ResetBallBlue()
    {
        ResetPaddle();
        transform.localPosition = new Vector2(1, 0);
        rigid.velocity = new Vector2(0, 0);
        yield return new WaitForSeconds(1);
        Vector2 arah = new Vector2(2, 0).normalized;
        rigid.AddForce(arah * force);
    }

    IEnumerator ResetBallRed()
    {
        ResetPaddle();
        transform.localPosition = new Vector2(-1, 0);
        rigid.velocity = new Vector2(0, 0);
        yield return new WaitForSeconds(1);
        Vector2 arah = new Vector2(-2, 0).normalized;
        rigid.AddForce(arah * force);
    }

    IEnumerator StartPlay()
    {
        yield return new WaitForSeconds(2);
        rigid = GetComponent<Rigidbody2D>();
        Vector2 arah = new Vector2(-2, 0).normalized;
        rigid.AddForce(arah * force);
    }

    void ResetPaddle()
    {
        paddleBlue = GameObject.Find("Sword");
        paddleBlue.transform.localPosition = new Vector2(-7.5f, 0);
        paddleRed = GameObject.Find("Spear");
        paddleRed.transform.localPosition = new Vector2(7.5f, 0);
    }

    void TampilkanScore()
    {
        Debug.Log("Score P1: " + scoreP1 + " Score P2: " + scoreP2);
        scoreUIP1.text = scoreP1 + "";
        scoreUIP2.text = scoreP2 + "";
    }

    public void GamePause()
    {
        Time.timeScale = 0;
        panelPause.SetActive(true);
    }

    public void GameLanjut()
    {
        Time.timeScale = 1;
        panelPause.SetActive(false);
    }

}