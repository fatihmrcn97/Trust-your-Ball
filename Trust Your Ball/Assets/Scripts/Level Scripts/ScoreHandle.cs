using UnityEngine;
using TMPro;
public class ScoreHandle : MonoBehaviour
{
    public static ScoreHandle instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public TextMeshProUGUI scoreTxt;
    private float timer;
    private int score;

    private void Start()
    {
        timer = 0;
    }
    void Update()
    {
        if (!LevelController.instance.isGameContinou)
            return;
         timer += Time.deltaTime;
        if (timer > 2f)
        {

            score += Random.Range(5,9);

            //We only need to update the text if the score changed.
            scoreTxt.text = "SCORE "+ score.ToString();

            //Reset the timer to 0.
            timer = 0;
        }
    }


    public void AddScore(int scores)
    {
        score += scores;
        scoreTxt.text = "SCORE " + score.ToString();
    }

    public int Get_Score()
    {
        return score;
    }
}
