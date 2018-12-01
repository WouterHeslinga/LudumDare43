using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    [SerializeField] private GameObject tooltipPanel;
    [SerializeField] private Text tooltipText;

    public static Tooltip Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        //Always make sure the tooltip is last in the draw order so it's on top
        tooltipPanel.transform.SetAsLastSibling();
    }

    private void Update()
    {
        tooltipPanel.transform.position = Input.mousePosition + new Vector3(tooltipPanel.GetComponent<RectTransform>().rect.width / 2, -tooltipPanel.GetComponent<RectTransform>().rect.height);
    }

    public void Hide()
    {
        tooltipPanel.SetActive(false);
        tooltipText.text = "";
    }

    public void Show(string text, string title = "")
    {
        tooltipText.text = $"{title}\n{text}";
        tooltipPanel.SetActive(true);
    }
}
