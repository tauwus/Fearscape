using System.Collections;
using UnityEngine;

public class OpenPanel : MonoBehaviour
{
    public GameObject Panel1;
    public GameObject Panel2;
    public GameObject Panel3;
    public GameObject Panel4;
    public GameObject Panel5;

    private GameObject currentActivePanel = null;

    public float animationDuration = 0.3f;

    public void OpenPanel1()
    {
        TogglePanel(Panel1);
    }

    public void OpenPanel2()
    {
        TogglePanel(Panel2);
    }

    public void OpenPanel3()
    {
        TogglePanel(Panel3);
    }

    public void OpenPanel4()
    {
        TogglePanel(Panel4);
    }

    public void OpenPanel5()
    {
        TogglePanel(Panel5);
    }

    private void TogglePanel(GameObject panel)
    {
        if (panel == null)
            return;

        if (currentActivePanel != null && currentActivePanel != panel)
        {
            StartCoroutine(ScalePanel(currentActivePanel, Vector3.one, Vector3.zero, animationDuration, false));
            currentActivePanel = null;
        }

        if (panel.activeSelf)
        {
            StartCoroutine(ScalePanel(panel, Vector3.one, Vector3.zero, animationDuration, false));
        }
        else
        {
            panel.SetActive(true);
            StartCoroutine(ScalePanel(panel, Vector3.zero, Vector3.one, animationDuration, true));
            currentActivePanel = panel;
        }
    }

    private IEnumerator ScalePanel(GameObject panel, Vector3 startScale, Vector3 endScale, float duration, bool activateOnEnd)
    {
        RectTransform panelRectTransform = panel.GetComponent<RectTransform>();

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            panelRectTransform.localScale = Vector3.Lerp(startScale, endScale, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        panelRectTransform.localScale = endScale;

        if (!activateOnEnd)
        {
            panel.SetActive(false);
        }
    }
}
