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

        yield return StartCoroutine(ShowText("10 ��� ����� �� ����� ��������� �������� ������������ ������. ����� �������� ���������� ����� � ��������� �� ������ ������, � �������� ������������ ����������� �������� �����."
            , Text1, 7));

        yield return StartCoroutine(ShowText("�� ������� ������� ��������������� � �� ����� ���������� ���������: ����-�� ������ �� ���� ����, ����-�� �������� � ���������� ������ ��������. ���� �������� ����������, ����� ������ �������� �������� ���� ���."
            , Text2, 7));

        yield return StartCoroutine(ShowText("������������� �������� �������� � ���������������� ��������, ��������� �������, �� �������� ��������������� ������ �� �������."
            , Text3, 7));

        yield return StartCoroutine(ClearAllText(AppearTextTime));

        yield return StartCoroutine(ShowText("������� ��������� �� ��������� ����. ������ � ������� ������������, �������������� ��������� ������ ���, ������������ ��������� ������. "
            , Text1, 7));

        yield return StartCoroutine(ShowText("������ ���, ���� � �������� ������ ����������. ������������� ����� ����� ������ �������� �������� ��� ��������� � ���� ������� ������� ����������� �������� � ������� �����. "
            , Text2, 7));

        yield return StartCoroutine(ShowText("��� ���������� � ����."
            , Text3, 5));

        yield return StartCoroutine(ClearAllText(AppearTextTime));

        yield return StartCoroutine(ShowText("�� ����� ����������� �����, ������� ������� ������, ��������� � ���������. ���� ���������� �������� �� ����� �������. ������� ��������� ������� ��� �����������. ���, ���� � ��������� ����� ������ ������� ���������..."
            , Text2, 10));

        yield return StartCoroutine(ClearAllText(AppearTextTime));

        SceneManager.LoadScene("�������_��������");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            StopAllCoroutines();
            SceneManager.LoadScene("�������_��������");
        }
    }
}
