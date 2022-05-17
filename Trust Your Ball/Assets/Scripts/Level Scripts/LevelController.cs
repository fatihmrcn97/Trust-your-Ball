using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    public GameObject PlayUI;
    public TextMeshProUGUI highScoreTxt, scoreTxt;
    public List<GameObject> hearts;

    public GameObject player;
    public bool canTochable;

    private int scoreNow;
    private int highScore;


    #region Singleton
    public static LevelController instance { get; private set;}

    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
         
        }

        canTochable = true;
        isGameContinou = true;
    }
    #endregion

    [HideInInspector] public bool isGameContinou = true;

    private void Start()
    {
        PlayerPrefs.GetInt("HigScore");
        highScore = PlayerPrefs.GetInt("HigScore");
        highScoreTxt.text = PlayerPrefs.GetInt("HigScore").ToString();
        
    }
    public void OpenUI()
    {
        PlayUI.SetActive(true);
    }

    public void WatchAd()
    {
        AdManager.instance.RequestRewardedAd();
 
    }

    public void WatchAdSettleMent()
    {
        StartCoroutine(SpawnNotDead());
        heartActive();
        isGameContinou = true;
        PlayUI.SetActive(false);
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("SampleScene");
        isGameContinou = true;
        PlayUI.SetActive(false);
    }

    public void ScoreChange()
    {
        scoreTxt.text = ScoreHandle.instance.Get_Score().ToString();
    }

    private void heartActive()
    {
        var tempColor = hearts[0].GetComponent<Image>().color;
        tempColor.a = 1f;
        hearts[0].GetComponent<Image>().color = tempColor;
        hearts[1].GetComponent<Image>().color = tempColor;
        hearts[2].GetComponent<Image>().color = tempColor;
    }

    
    IEnumerator SpawnNotDead()
    {
        player.GetComponent<SphereCollider>().enabled = false;
        player.GetComponent<Animator>().SetBool("isdead", true);
        canTochable = false;
        yield return new WaitForSeconds(2f);
        player.GetComponent<SphereCollider>().enabled = true;
        player.GetComponent<Animator>().SetBool("isdead", false);
        canTochable = true;
    }

    public void WriteHighScore()
    {
        scoreNow = ScoreHandle.instance.Get_Score();

        if (scoreNow > highScore)
            PlayerPrefs.SetInt("HigScore", scoreNow);

        highScoreTxt.text = PlayerPrefs.GetInt("HigScore").ToString();
        ScoreChange();
    }
}
