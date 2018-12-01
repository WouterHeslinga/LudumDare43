using System;
using System.Collections.Generic;
using UnityEngine;

public class InfoPanelManager : MonoBehaviour
{
    [SerializeField] private Transform canvas;
    [SerializeField] private GameObject minionInfoPanelPrefab;

    private Dictionary<Type, IInfoPanel> infoPanelDictionary;
    private Dictionary<Type, GameObject> prefabDictionary;
    private IInfoPanel currentInfoPanel;
    private IHasInfoPanel selectedEntity;

    private void Start()
    {
        prefabDictionary = new Dictionary<Type, GameObject>()
        {
            { typeof(Minion), minionInfoPanelPrefab }
        };
    }

    private void Update()
    {
        if(selectedEntity != null)
            currentInfoPanel.Transform.position = Camera.main.WorldToScreenPoint(selectedEntity.Transform.position + new Vector3(0, 2));
    }

    /// <summary>
    /// Select the give minion and show the info panel for that minion
    /// </summary>
    public void SelectMinion(IHasInfoPanel entity)
    {
        //Get rid of old minion callbacks
        if (selectedEntity != null)
            selectedEntity.Stats.OnStatChanged -= currentInfoPanel.UpdateInfo;

        //Destroy old panel
        Destroy(currentInfoPanel?.GameObject);

        //Create new info panel
        currentInfoPanel = Instantiate(prefabDictionary[entity.GetType()], canvas).GetComponent<IInfoPanel>();

        //Set new callback
        selectedEntity = entity;
        selectedEntity.Stats.OnStatChanged += currentInfoPanel.UpdateInfo;
        currentInfoPanel.UpdateInfo(selectedEntity.Stats);

        //Set position and show
        currentInfoPanel.Transform.position = Camera.main.WorldToScreenPoint(selectedEntity.Transform.position + new Vector3(0, 2));
        currentInfoPanel.GameObject.SetActive(true);
    }

    /// <summary>
    /// Deselect all minion and hide info panels
    /// </summary>
    public void DeselectMinion()
    {
        if (selectedEntity == null)
            return;

        selectedEntity.Stats.OnStatChanged -= currentInfoPanel.UpdateInfo;

        currentInfoPanel.GameObject.SetActive(false);
        selectedEntity = null;
    }
}

