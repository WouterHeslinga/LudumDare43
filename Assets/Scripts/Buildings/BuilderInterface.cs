using UnityEngine;
using UnityEngine.UI;

public class BuilderInterface : MonoBehaviour
{
    private Builder builder;
    [SerializeField] private GameObject buttonPanel;
    [SerializeField] private GameObject buttonPrefab;

    private void Start()
    {
        builder = FindObjectOfType<Builder>();

        for (int i = 0; i < builder.buildingPrefabs.Length; i++)
        {
            var building = builder.buildingPrefabs[i].GetComponent<Building>();

            var index = i;

            var button = Instantiate(buttonPrefab, buttonPanel.transform);
            var sprite = button.GetComponent<Image>().sprite = building.interfaceSprite;
            button.GetComponent<Button>().onClick.AddListener(() => builder.SelectBuilding(index));
        }
    }
}

