using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Data;

public class WaveShaper : MonoBehaviour
{
    TargetLine targetLine;
    Score scoreObject = new Score();
    Settings settingsObject = new Settings();
    public GameObject postFx;
    public GameObject thiccMode;
    public GameObject gameOverScreen;
    public Animator gameOverAnimation;
    public Animator plusAnimation;
    public ParticleSystem particle;
    float scoreProgress=0;
    float comboProgress=1;
    float score=0;
    public float timer;
    public float changeInterval=5f;
    public Text scoreText;
    public Text comboText;
    public Text timerText;
    public int scoreSpeed=10;
    public int comboMultiplier=1;
    public float fillSpeed=0.5f;
    public float sinkSpeed=0.2f;
    int size = 60;
    public float randomNumber;
    public AudioSource click;
    public AudioSource music;
    public Slider scoreSlider;
    public Slider comboSlider;
    public float swipeDistance;
    float heightToChange=0.5f;
    public float height=0.5f;
    LineRenderer line;
    void Start()
    {
        Time.timeScale = 1f;
        line = GetComponent<LineRenderer>();
        randomNumber = Random.Range(0f, 1f);

        click = GameObject.Find("bonusSound").GetComponent<AudioSource>();
        music = GameObject.Find("music").GetComponent<AudioSource>();

        targetLine = GameObject.Find("targetLine").GetComponent<TargetLine>();
        particle = GameObject.Find("Particle").GetComponent<ParticleSystem>();

        gameOverAnimation.SetBool("gameOver", false);
        StartCoroutine(RandomHeight());
    }

    void Update()
    {
        height = Mathf.Lerp(height, heightToChange, 4*Time.deltaTime);
        plusAnimation.SetBool("bonusTime", false);
        timer -= Time.deltaTime;
        Swipe();
        heightToChange = -swipeDistance/5;

        //post fx
        if(settingsObject.Performance=="true")
        {
            postFx.SetActive(false);
        }
        if(settingsObject.Quality=="true")
        {
            postFx.SetActive(true);
        }
        //thicc mode
        if(settingsObject.Thicc=="True")
        {
            thiccMode.SetActive(true);
        }
        if(settingsObject.Thicc=="False")
        {
            thiccMode.SetActive(false);
        }

        //if time runs out
        if(timer<=0.1f)
        {
            if(score>scoreObject.HighScore)
            {
                scoreObject.HighScore=score;
            }
            gameOverScreen.SetActive(true);
            gameOverAnimation.SetBool("gameOver", true);
            music.pitch = 0.5f;
            Time.timeScale = 0f;
        }

        //slider value
        scoreSlider.value = scoreProgress;
        comboSlider.value =  comboProgress;
        //text
        timerText.text=Mathf.Round(timer).ToString();
        scoreText.text=Mathf.Round(score).ToString();
        comboText.text=comboMultiplier+"X";
        //clamp
        comboMultiplier=Mathf.Clamp(comboMultiplier, 0, 128);
        changeInterval=Mathf.Clamp(changeInterval, 0.5f, 10f);
        sinkSpeed=Mathf.Clamp(sinkSpeed, 0.1f, 1f);
        timer=Mathf.Clamp(timer, 0, 10000);
        heightToChange=Mathf.Clamp(heightToChange, 0f, 1f);
        swipeDistance=Mathf.Clamp(swipeDistance, -5f, 0f);

        if(scoreProgress==0)
        {
            comboProgress-=Time.deltaTime;
            comboProgress=Mathf.Clamp(comboProgress, 0, 1);
        }
        else
        {
            comboProgress+=Time.deltaTime;
        }
        //lose combo
        if(comboProgress==0 && comboMultiplier > 1)
        {
            comboProgress=1;
            comboMultiplier=comboMultiplier/2;
            changeInterval+=0.5f;
            sinkSpeed-=0.1f;
            particle.startSpeed+=0.4f;
        }

        //Correct Shape
        if(targetLine.height-0.1f < height && height < targetLine.height+0.1f)
        {
            Rising();
        }
        else
        {
            scoreProgress-=sinkSpeed*Time.deltaTime;
            scoreProgress=Mathf.Clamp(scoreProgress, 0, 1);
        }
        //music pitch
        if(randomNumber<height)
        {
            music.pitch=Mathf.Lerp(music.pitch, 1.5f, 2*Time.deltaTime);;
        }
        else if(height<randomNumber)
        {
            music.pitch=Mathf.Lerp(music.pitch, 0.5f, 2*Time.deltaTime);
        }

        if (scoreProgress>=1)
        {
            FullBar();
        }

        for (int i=0;i<size;i++)
        {
            Vector3 pos = new Vector3(i*0.1f, Mathf.Sin(0.2f*i+Time.time)*height, 0);
            line.SetPosition(i, pos);
        }
        
    }

    void Swipe()
    {
        if(Input.touchCount>0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touch_pos = Camera.main.ScreenToWorldPoint(touch.position);
            Vector2 touchDown = Vector2.zero;
            Debug.Log(swipeDistance);

            if(touch.phase==TouchPhase.Began)
            {
                touchDown = touch_pos;      
            }

            swipeDistance = touch_pos.y-touchDown.y;            
        }
    }

    void Rising()
    {
        scoreProgress+=fillSpeed*Time.deltaTime;
        score+=scoreSpeed*comboMultiplier*Time.deltaTime;
        music.pitch=Mathf.Lerp(music.pitch, 1f, 2*Time.deltaTime);;  
    }
    void FullBar()
    {
        scoreProgress=0;
        comboMultiplier=comboMultiplier*2;
        changeInterval-=0.5f;
        sinkSpeed+=0.1f;
        particle.startSpeed-=0.4f;
        BonusTime();
        click.Play();
    }
    void BonusTime()
    {
        timer+=5f;
        plusAnimation.SetBool("bonusTime", true);
    }
    IEnumerator RandomHeight()
    {
        while(true)
        {
            randomNumber = Random.Range(0f, 1f);
            yield return new WaitForSeconds(changeInterval);
        }
    }
}
