using UnityEngine;
using TMPro;

public class TimerScript : MonoBehaviour
{
    [SerializeField] private float timer;
    [SerializeField] private TMP_Text timerText;
    private bool moved = false;
    private bool won = false;

    // Update is called once per frame
    void Update()
    {
        if(!won)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
            {
                moved = true;
            }

            if(moved)
            {
                timer += Time.deltaTime;
                timerText.text = timer.ToString("F2"); 
            }
        }
    }
    
    public void End()
    {
        won = true;   
    }
}
