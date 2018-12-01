using UnityEngine;

public class MinionInterface : MonoBehaviour
{
    [SerializeField] private Transform canvas;
    [SerializeField] private GameObject minionInfoPanelPrefab;
    private MinionInfoPanel minionInfoPanel;

    private Minion selectedMinion;

    private void Start()
    {
        minionInfoPanel = Instantiate(minionInfoPanelPrefab, canvas).GetComponent<MinionInfoPanel>();
        minionInfoPanel.gameObject.SetActive(false);
    }

    private void Update()
    {
        if(selectedMinion != null)
            minionInfoPanel.transform.position = Camera.main.WorldToScreenPoint(selectedMinion.transform.position + new Vector3(0, 2));
    }

    /// <summary>
    /// Select the give minion and show the info panel for that minion
    /// </summary>
    public void SelectMinion(Minion minion)
    {
        //Get rid of old minion callbacks
        if(selectedMinion != null)
            selectedMinion.minionStats.OnStatChanged -= minionInfoPanel.UpdateInfo;

        //Set new callback
        selectedMinion = minion;
        selectedMinion.minionStats.OnStatChanged += minionInfoPanel.UpdateInfo;
        minionInfoPanel.UpdateInfo(selectedMinion.minionStats);
        minionInfoPanel.gameObject.SetActive(true);
    }

    /// <summary>
    /// Deselect all minion and hide info panels
    /// </summary>
    public void DeselectMinion()
    {
        if (selectedMinion != null)
            selectedMinion.minionStats.OnStatChanged -= minionInfoPanel.UpdateInfo;

        minionInfoPanel.gameObject.SetActive(false);
        selectedMinion = null;
    }
}

