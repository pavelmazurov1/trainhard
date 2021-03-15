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

    public List<string> Messages;
    public int CurrentMessage = 0;

    public float AppearTextTime = 1f;

    void Start()
    {
        Messages.Add("10 ��� ����� �� ����� ��������� �������� ������������ ������. ����� �������� ���������� ����� � ��������� �� ������ ������, � �������� ������������ ����������� �������� �����.");
        Messages.Add("�� ������� ������� ��������������� � �� ����� ���������� ���������: ����-�� ������ �� ���� ����, ����-�� �������� � ���������� ������ ��������. ���� �������� ����������, ����� ������ �������� �������� ���� ���.");
        Messages.Add("������������� �������� �������� � ���������������� ��������, ��������� �������, �� �������� ��������������� ������ �� �������.");
        Messages.Add("������� ��������� �� ��������� ����. ������ � ������� ������������, �������������� ��������� ������ ���, ������������ ��������� ������. ");
        Messages.Add("������ ���, ���� � �������� ������ ����������. ������������� ����� ����� ������ �������� �������� ��� ��������� � ���� ������� ������� ����������� �������� � ������� �����. ");
        Messages.Add("��� ���������� � ����.");

        AudioSource.clip = IntroAudio;
        AudioSource.Play();

        StartCoroutine(ShowText());
    }

    IEnumerator ShowText()
    {
        Text1.text = "";
        Text2.text = "";
        Text3.text = "";

        Text1.color = new Color(1, 1, 1, 0);
        Text2.color = new Color(1, 1, 1, 0);
        Text3.color = new Color(1, 1, 1, 0);

        yield return new WaitForSeconds(5);

        Text[] texts = { Text1, Text2, Text3 };

        float timeSpent = 0;

        while (true)
        {
            if (CurrentMessage == Messages.Count) break;

            if(CurrentMessage > 0 && CurrentMessage % 3 == 0)
            {
                timeSpent = 0;
                while (timeSpent <= AppearTextTime)
                {
                    var textColor = Text1.color;
                    textColor.a = 1 - timeSpent / AppearTextTime;
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

            Text CurrentText = texts[CurrentMessage % 3];
            CurrentText.text = Messages[CurrentMessage];

            timeSpent = 0;
            while (timeSpent <= AppearTextTime)
            {
                var textColor = CurrentText.color;
                textColor.a = timeSpent / AppearTextTime;
                //Debug.Log(textColor.a);
                CurrentText.color = textColor;
                timeSpent += Time.deltaTime;
                yield return null;
            }

            CurrentMessage += 1;
            yield return new WaitForSeconds(10);
        }

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
