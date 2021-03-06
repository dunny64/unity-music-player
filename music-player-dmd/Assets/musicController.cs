using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.UI;



public class musicController : MonoBehaviour
{
    public AudioClip[] songs;
    public int currentsong;
    public List<int> history;
    public Animator[] discAnimators;
    public Animator turntableAnimator;
    public TextMeshProUGUI songname;
    public Button playPauseButton;
    public Button shuffleButton;
    public TextMeshProUGUI speedText;
    public float speedMultiplier;
    public Sprite playSprite;
    public Sprite pauseSprite;
    public float globalVolume;

    public Image volume;

    [HideInInspector]
    public AudioSource selfsource;
    private bool isPlaying;
    private bool isShuffle;

    // Start is called before the first frame update
    void Start()
    {
        selfsource = GetComponent<AudioSource>();
        isPlaying = false;
        isShuffle = false;
        shuffleButton.image.color = Color.black;
        speedMultiplier = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (selfsource.volume <= 0)
        {
            volume.color = Color.gray;
        }
        else
        {
            volume.color = Color.white;
        }
        //selfsource.volume = globalVolume;
        switch (currentsong)
        {
            case -1:
                songname.text = "No song playing";
                break;
            case 0:
                //momento azul rojuu
                songname.text = "momenTo azul - Rojuu";
                break;

            case 1:
                //n64 alex g
                songname.text = "Nintendo 64 - Alex G";
                break;

            case 2:
                //papi gnash
                songname.text = "papi - Gnash";
                break;

            case 3:
                //dbz cia
                songname.text = "dragon ball z budokai tenkaichi 4 - Camping in Alaska";
                break;

            case 4:
                //glowing superweaks
                songname.text = "Glowing - The Superweaks";
                break;
        }
        switch (speedMultiplier)
        {
            case 1:
                speedText.text = "x1";
                break;
            case 2:
                speedText.text = "x2";
                break;
            case 0.5f:
                speedText.text = "x0.5";
                break;
        }
        if (isPlaying && currentsong != -1)
        {
            discAnimators[currentsong].speed = speedMultiplier;
        }
        selfsource.pitch = speedMultiplier;
        if (isPlaying && selfsource.isPlaying == false)
        {
            nextSong();
        }
    }
    public void PlaySong(int s)
    {
        reanimate(); //Reset animators speeds to 1 in case the disc was paused

        if (currentsong != s)
        {
            currentsong = s;
            if (history.Last() != -1)
            {
                discAnimators[history.Last()].SetTrigger("stop");
                turntableAnimator.SetTrigger("pausetrigger");
            }

            turntableAnimator.SetTrigger("playtrigger");


            switch (s)
            {
                case 0:
                    discAnimators[s].Play("rojuuplay");
                    break;
                case 1:
                    discAnimators[s].Play("alexgplay");
                    break;
                case 2:
                    discAnimators[s].Play("gnashplay");
                    break;
                case 3:
                    discAnimators[s].Play("ciaplay");
                    break;
                case 4:
                    discAnimators[s].Play("supweakplay");
                    break;

            }
            history.Add(currentsong);
            selfsource.clip = songs[currentsong];
            selfsource.Play();
            playPauseButton.image.sprite = pauseSprite;
            isPlaying = true;
        }
    }
    private void PlaySongUnrestricted(int s)
    {
        currentsong = s;
        if (history.Last() != -1)
        {
            discAnimators[history.Last()].SetTrigger("stop");
            turntableAnimator.SetTrigger("pausetrigger");
        }

        turntableAnimator.SetTrigger("playtrigger");


        switch (s)
        {
            case 0:
                discAnimators[s].Play("rojuuplay");
                break;
            case 1:
                discAnimators[s].Play("alexgplay");
                break;
            case 2:
                discAnimators[s].Play("gnashplay");
                break;
            case 3:
                discAnimators[s].Play("ciaplay");
                break;
            case 4:
                discAnimators[s].Play("supweakplay");
                break;

        }
        history.Add(currentsong);
        selfsource.clip = songs[currentsong];
        selfsource.Play();
        playPauseButton.image.sprite = pauseSprite;
        isPlaying = true;
    }
    public void PlayPause()
    {
        if (isPlaying)
        {
            playPauseButton.image.sprite = playSprite;
            selfsource.Pause();
            discAnimators[currentsong].speed = 0;
            turntableAnimator.speed = 0;
            isPlaying = false;

        }
        else
        {
            playPauseButton.image.sprite = pauseSprite;
            selfsource.UnPause();
            discAnimators[currentsong].speed = 1;
            turntableAnimator.speed = 1;
            isPlaying = true;
        }

    }
    public void StopTrack()
    {
        if (currentsong != -1)
        {
            isPlaying = false;
            reanimate();
            discAnimators[currentsong].SetTrigger("stop");
            turntableAnimator.SetTrigger("pausetrigger");
            selfsource.Stop();
            currentsong = -1;
            history.Add(currentsong);
        }
        history.Clear();
        history.Add(-1);

    }
    public void PrevSong() //Plays the previous song in the history
    {
        if (history.Any())
        {
            //reanimate();
            if (history.Last() != -1)
            {
                currentsong = history[history.Count() - 2];
            }

            if (currentsong != -1)
            {
                discAnimators[history.Last()].SetTrigger("stop");
                turntableAnimator.SetTrigger("pausetrigger");
                history.Remove(history.Last());
                selfsource.clip = songs[currentsong];
                switch (currentsong)
                {
                    case 0:
                        discAnimators[currentsong].Play("rojuuplay");
                        break;
                    case 1:
                        discAnimators[currentsong].Play("alexgplay");
                        break;
                    case 2:
                        discAnimators[currentsong].Play("gnashplay");
                        break;
                    case 3:
                        discAnimators[currentsong].Play("ciaplay");
                        break;
                    case 4:
                        discAnimators[currentsong].Play("supweakplay");
                        break;
                }
                selfsource.Play();
            }
            else
            {
                if (history.Last() != -1)
                {
                    discAnimators[history.Last()].SetTrigger("stop");
                    turntableAnimator.SetTrigger("pausetrigger");
                    selfsource.Stop();
                    history.Clear();
                    history.Add(-1);
                }
            }
        }
    }
    public void nextSong()
    {
        if (currentsong != -1)
        {
            if (isShuffle)
            {
                Debug.Log(songs.Count());
                int s = Random.Range(0, 5);
                bool done = false;
                while(!done)
                {
                    if (currentsong != s)
                    {
                        currentsong = s;
                        PlaySongUnrestricted(currentsong);
                        done = true;
                    }
                    else
                    {
                        s = Random.Range(0, 5);
                    }
                }
                
            }
            else
            {
                if (currentsong != 4)
                {
                    currentsong++;
                    PlaySongUnrestricted(currentsong);
                }
                else
                {
                    currentsong = 0;
                    PlaySongUnrestricted(currentsong);
                }
            }
        }
    }
    public void hitShuffle()
    {
        if (!isShuffle)
        {
            shuffleButton.image.color = Color.white;
            isShuffle = true;
        }
        else
        {
            shuffleButton.image.color = Color.black;
            isShuffle = false;
        }
        
    }
    public void hitSpeed()
    {
        switch(speedMultiplier)
        {
            case 0.5f:
                speedMultiplier = 1;
                break;
            case 1:
                speedMultiplier = 2;
                break;
            case 2:
                speedMultiplier = 0.5f;
                break;
        }
    }
    private void reanimate()
    {
        if (currentsong != -1)
        {
            if (discAnimators[currentsong].speed == 0)
            {
                discAnimators[currentsong].speed = 1;
            }
            if (turntableAnimator.speed == 0)
            {
                turntableAnimator.speed = 1;
            }
        }
    }
}