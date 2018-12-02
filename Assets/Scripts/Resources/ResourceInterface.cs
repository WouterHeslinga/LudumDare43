using UnityEngine;
using UnityEngine.UI;

public class ResourceInterface : MonoBehaviour
{
    [SerializeField] private Text bones;
    [SerializeField] private Text meat;
    [SerializeField] private Text food;
    [SerializeField] private Text population;

    private ResourceManager resourceManager;

    private void Start()
    {
        resourceManager = FindObjectOfType<ResourceManager>();

        resourceManager.OnResourcesChanged += UpdateInterface;
        MinionManager.Instance.OnPopulationChanged += UpdateInterface;

        UpdateInterface();
    }

    private void UpdateInterface()
    {
        bones.text = $"Bones: {resourceManager.Resources[ResourceType.Bones]}"; 
        meat.text = $"Meat: {resourceManager.Resources[ResourceType.Meat]}"; 
        food.text = $"Food: {resourceManager.Resources[ResourceType.Food]}";
        population.text = $"Population: {MinionManager.Instance.Population}/{MinionManager.Instance.MaxPopulation}";
    }
}
