using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;

public class CoutdownUI : MonoBehaviour
{
    public TextMeshProUGUI countdownText;
    public GameObject countdownObject;
    public GeneralControlsPlayer Player1;
    public GeneralControlsPlayer Player2;

    public AudioPlayer audioPlayer;

    void Start()
    {
        StartCoroutine(StartCountdown());
    }

    public IEnumerator StartCountdown()
    {
        Player1.inCountdown = true;
        Player2.inCountdown = true;

        countdownObject.SetActive(true);

        countdownText.text = "3";
        countdownText.transform.DOPunchScale(Vector3.one * 0.3f, 0.3f);
        audioPlayer.PlayClip("3");
        yield return new WaitForSeconds(1f);

        countdownText.text = "2";
        countdownText.transform.DOPunchScale(Vector3.one * 0.3f, 0.3f);
        audioPlayer.PlayClip("2");
        yield return new WaitForSeconds(1f);

        countdownText.text = "1";
        countdownText.transform.DOPunchScale(Vector3.one * 0.3f, 0.3f);
        audioPlayer.PlayClip("1");
        yield return new WaitForSeconds(1f);

        countdownText.text = "FIGHT!";
        countdownText.transform.DOPunchScale(Vector3.one * 1f, 0.3f);
        audioPlayer.PlayClip("Fight");
        yield return new WaitForSeconds(0.5f);

        countdownObject.SetActive(false);

        Player1.inCountdown = false;
        Player2.inCountdown = false;
    }
}
