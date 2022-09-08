using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Lose : MonoBehaviour
{
    public Image background, button;
    public TextMeshProUGUI text, buttonText;
    [SerializeField] private float alpha;
    public void Close()
    {
        this.gameObject.SetActive(false);
    }
    public void In()
    {
        StartCoroutine(FadedIn());
    }
    IEnumerator FadedIn()
    {
        while (alpha < 0.9f)
        {
            alpha += Time.deltaTime;
            background.color = new Color(1, 1, 1, alpha);
            button.color = new Color(1, 82 / 255, 82 / 255, alpha);
            buttonText.color = new Color(0, 0, 0, alpha);
            text.color = new Color(0, 0, 0, alpha);
            yield return new WaitForSeconds(0.01f);
        }
    }
}
