using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System.Collections;

public class SacarSangre : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Image fillImage;
    public TextMeshProUGUI uiText;
    public float delayBeforeFill = 1.0f;
    public float fillDuration = 1.5f;
    public string[] randomTexts;

    private bool isHolding = false;
    private bool isCompleted = false;
    private float currentDelayTimer = 0f;
    public float esconderDelay = 2.0f;
    private Coroutine esconder;

    void Update()
    {
        if (fillImage == null || isCompleted) return;

        if (isHolding)
        {
            if (currentDelayTimer < delayBeforeFill)
            {
                currentDelayTimer += Time.deltaTime;
            }
            else
            {
                fillImage.fillAmount += Time.deltaTime / fillDuration;

                if (fillImage.fillAmount >= 1f)
                {
                    fillImage.fillAmount = 1f;
                    isCompleted = true;
                    isHolding = false;
                }
            }
        }
        else
        {
            currentDelayTimer = 0f;
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!isCompleted)
        {
            isHolding = true;
            currentDelayTimer = 0f;
        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        isHolding = false;
    }
    public void Onclick()
    {
        if (randomTexts != null && randomTexts.Length > 0)
        {
            int randomIndex = Random.Range(0, randomTexts.Length);
            string textoElegido = randomTexts[randomIndex];
            if (uiText != null)
            {
                uiText.text = textoElegido;
                uiText.gameObject.SetActive(true);

                if (esconder != null)
                    StopCoroutine(esconder);

                esconder = StartCoroutine(EsconderTextAfterDelay(esconderDelay));
            }
        }
    }
    private IEnumerator EsconderTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        uiText.gameObject.SetActive(false);
        esconder = null;
    }
}