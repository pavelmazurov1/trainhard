using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class IntroLevelDirector : MonoBehaviour
{
    public Text Text1;
    public Text Text2;
    public Text Text3;

    public AudioClip IntroAudio;
    public AudioSource AudioSource;

    public float AppearTextTime = 1f;

    void Start()
    {
        AudioSource.clip = IntroAudio;
        AudioSource.Play();

        StartCoroutine(Main());
    }

    IEnumerator ShowText(string message, Text CurrentText, float StayTime)
    {
        float timeSpent = 0;
        CurrentText.text = message;
        CurrentText.color = new Color(1, 1, 1, 0);
        while (timeSpent <= AppearTextTime)
        {
            var textColor = CurrentText.color;
            textColor.a = timeSpent / AppearTextTime;
            //Debug.Log(textColor.a);
            CurrentText.color = textColor;
            timeSpent += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(StayTime);
    }

    IEnumerator ClearAllText(float ClearTextTime)
    {
        float timeSpent = 0;
        while (timeSpent <= ClearTextTime)
        {
            var textColor = Text1.color;
            textColor.a = 1 - timeSpent / ClearTextTime;
            Text1.color = textColor;
            Text2.color = textColor;
            Text3.color = textColor;
            timeSpent += Time.deltaTime;
            yield return null;
        }

        Text1.text = "";
        Text2.text = "";
        Text3.text = "";
        Text1.color = new Color(1, 1, 1, 0);
        Text2.color = new Color(1, 1, 1, 0);
        Text3.color = new Color(1, 1, 1, 0);
    }

    IEnumerator Main()
    {
        //clear texts
        Text1.text = "";
        Text2.text = "";
        Text3.text = "";
        Text1.color = new Color(1, 1, 1, 0);
        Text2.color = new Color(1, 1, 1, 0);
        Text3.color = new Color(1, 1, 1, 0);
        //wait
        yield return new WaitForSeconds(5);

        yield return StartCoroutine(ShowText("10 лет назад на земле случилась эпидеми€ неизвестного вируса. ¬ирус оказалс€ совершенно новым и непохожим на другие вирусы, с которыми человечеству приходилось боротьс€ ранее."
            , Text1, 7));

        yield return StartCoroutine(ShowText("ќн обладал высокой контагеозностью и на людей действовал поразному: кого-то убивал за одну ночь, кого-то медленно и мучительно убивал мес€цами. Ћюди погибали миллионами, очень быстро эпидеми€ охватила весь мир."
            , Text2, 7));

        yield return StartCoroutine(ShowText("ѕравительства пытались боротьс€ с распространением эпидемии, закрывали границы, но сдержать распространение вируса не удалось."
            , Text3, 7));

        yield return StartCoroutine(ClearAllText(AppearTextTime));

        yield return StartCoroutine(ShowText("ћирова€ экономика не выдержала удар. «аводы и фабрики остановились, электростанции перестали давать ток, транспортное сообщение умерло. "
            , Text1, 7));

        yield return StartCoroutine(ShowText("«апасы еды, воды и ресурсов быстро истощались. ѕравительства стран очень быстро потер€ли контроль над ситуацией и были сметены толпами обезумевших голодных и больных людей. "
            , Text2, 7));

        yield return StartCoroutine(ShowText("ћир погрузилс€ в хаос."
            , Text3, 5));

        yield return StartCoroutine(ClearAllText(AppearTextTime));

        yield return StartCoroutine(ShowText("“о малое колличество людей, которым повезло выжить, собрались в поселени€. Ћюди продолжают страдать от новой болезни. ≈диницы перенесли болезнь без последствий. ≈да, вода и лекарства стали самыми ценными ресурсами..."
            , Text2, 10));

        yield return StartCoroutine(ClearAllText(AppearTextTime));

        SceneManager.LoadScene("уровень_элеватор");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            StopAllCoroutines();
            SceneManager.LoadScene("уровень_элеватор");
        }
    }
}
