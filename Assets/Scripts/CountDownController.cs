using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDownController : MonoBehaviour
{
    public int countDownTime;
    public Text countDownText;

    public AudioClip firstBeep;
    public AudioClip secondBeep;
    public AudioClip thirdBeep;
    public AudioClip endBeep;

    public IEnumerator CountDownToStart() 
    {
        AudioSource audio = gameObject.GetComponent<AudioSource>();

        countDownText.gameObject.SetActive(false);

        yield return new WaitForSeconds(1f);
        
        countDownText.gameObject.SetActive(true);

        while(countDownTime > 0) {
            countDownText.text = countDownTime.ToString();

            if(countDownTime == 3) {
                audio.PlayOneShot(firstBeep, 0.8f);
            } else if(countDownTime == 2) {
                audio.PlayOneShot(secondBeep, 0.8f);
            } else if(countDownTime == 1) {
                audio.PlayOneShot(thirdBeep, 0.8f);
            }

            yield return new WaitForSeconds(1f);

            countDownTime--;
        }

        audio.PlayOneShot(endBeep, 1.0f);
        countDownText.text = "GO";

        yield return new WaitForSeconds(1f);
        countDownText.gameObject.SetActive(false);

        FindObjectOfType<GameManager>().StartGame();
    }
}
