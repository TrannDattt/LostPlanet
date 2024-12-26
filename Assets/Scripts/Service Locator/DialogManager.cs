using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    public TextMeshProUGUI dialogContent;
    public TextMeshProUGUI characterName;
    public Canvas canvas;

    public List<Dialog> dialogs = new List<Dialog>();

    private Queue<string> sentences = new Queue<string>();

    public void StartDialog()
    {
        canvas.gameObject.SetActive(true);
        sentences.Clear();

        foreach (Dialog dialog in dialogs)
        {
            foreach(string sentence in dialog.sentences)
            {
                sentences.Enqueue(sentence);
            }
        }

        ShowNextLine();
    }

    public void ShowNextLine()
    {
        if (sentences.Count > 0)
        {
            StopAllCoroutines();

            string sentence = sentences.Dequeue();
            StartCoroutine(ShowWordByWord(sentence));
        }
        else
        {
            GameManager.Instance.StopDialog();
        }
    }

    private IEnumerator ShowWordByWord(string sentence)
    {
        dialogContent.text = "";

        foreach(char word in sentence.ToCharArray())
        {
            dialogContent.text += word;
            yield return null;
        }
    }

    public void SkipDialog()
    {
        GameManager.Instance.StopDialog();
    }

    public void StopDialog()
    {
        StopAllCoroutines();

        sentences.Clear();
        dialogs.Clear();
        canvas.gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        canvas.gameObject.SetActive(false);
    }
}
