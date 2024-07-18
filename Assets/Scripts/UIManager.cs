using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    const int SPEED_TIME = 10;

    public GameObject speedIcon;
    public TextMeshProUGUI start;
    public TextMeshProUGUI gameOver;
    [SerializeField] AudioClip deathAudio, winAudio;
    private AudioSource Audio;

    private float timer;
    private bool timer_on;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0.0f;
        timer_on = false;
        start.enabled = true;
        gameOver.enabled = false;
        speedIcon.SetActive(false);
        Audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer_on)
        {
            timer += Time.deltaTime;
            if(timer >= SPEED_TIME)
            {
                timer_on = false;
                speedIcon.SetActive(false);
                timer = 0;
            }
        }
    }

    public void enable_speed_UI()
    {
        timer_on = true;
        speedIcon.SetActive(true);
    }

    public IEnumerator start_chase()
    {
        start.SetText("");
        yield return new WaitForSeconds(1);
        start.SetText("He's coming for you...");
        yield return new WaitForSeconds(2);
        start.enabled = false;
    }

    public IEnumerator game_start(int start_time)
    {
        start.enabled = true;
        for (int i = start_time; i >= 0; i--)
        {
            start.SetText(i.ToString());
            yield return new WaitForSeconds(1);
        }
    }

    public void game_over()
    {
        if (gameOver.enabled) return;
        Audio.clip = deathAudio;
        Audio.Play();
        gameOver.enabled = true;
    }

    public void game_won()
    {
        if (gameOver.enabled) return;
        gameOver.SetText("You Escaped!");
        Audio.clip = winAudio;
        Audio.Play();
        gameOver.enabled = true;
    }
}
