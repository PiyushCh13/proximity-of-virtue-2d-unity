using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Dialog_Tips_Manager : MonoBehaviour
{

    [SerializeField]
    TextMeshProUGUI m_TextMesh;

    public RawImage fadeImage;

    public Sprite[] spriteBackgroundImages;
    public RawImage dialogueImages;

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
            if (index < dialogues.Count && index < spriteBackgroundImages.Length)
            {
                UpdateIntroDialog(index);
                dialogueImages.texture = spriteBackgroundImages[index].texture;
                index++;              
                SFXManager.Instance.PlaySound(SFXManager.Instance.clickSound);
            }
            else
            {
                OnDialogueFinish();
            }
        }

    }

    private void OnDialogueFinish()
    {
        SceneManagement.Instance.LoadScene(fadeImage, SceneList.Level1.ToString());
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
