using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dialog_Tips_Manager : MonoBehaviour
{

    [SerializeField]
    TextMeshProUGUI m_TextMesh;

    [SerializeField]
    string dialogText;

    [SerializeField]
    List<string> dialogues = new List<string>();

    int index = 0;


    // Start is called before the first frame update
    void Start()
    {
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (index < dialogues.Count)
            {
                UpdateIntroDialog(index);
                index++;
            }
            else
            {
                OnDialogueFinish();
            }
        }

    }

    private void OnDialogueFinish()
    {

    }

    private void UpdateIntroDialog(int index)
    {
        ShowText(dialogues[index]);
    }

    void ShowText(float duration, string message)
    {
        dialogText = message;
        m_TextMesh.text = message;
        StopCoroutine(HideText(duration));
    }

    void ShowText(string message)
    {
        dialogText = message;
        m_TextMesh.text = message;
    }

    IEnumerator HideText(float duration)
    {
        yield return new WaitForSeconds(duration);
        dialogText = null;
        m_TextMesh.text = null;
    }
}
