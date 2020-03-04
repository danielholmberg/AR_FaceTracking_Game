using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDownController : MonoBehaviour
{
    public int countDownTime;
    public Text countDownText;

    public IEnumerator CountDownToStart() 
    {
        while(countDownTime > 0) {
            countDownText.text = countDownTime.ToString();

            yield return new WaitForSeconds(1f);

            countDownTime--;
        }

        countDownText.text = "GO";

        yield return new WaitForSeconds(1f);
        countDownText.gameObject.SetActive(false);

        StartCoroutine(FindObjectOfType<GameManager>().LaunchBulletWave());
        StartCoroutine(FindObjectOfType<GameManager>().LaunchBombWave());
    }
}
