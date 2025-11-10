using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
   public TextMeshProUGUI scoreText;
   private float score;

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Player") != null) 
        {
            score += 1 * Time.deltaTime;
            scoreText.text =((int)score).ToString();
        
        }
    }
}
