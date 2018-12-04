using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class MinionManager : MonoBehaviour
{
    [SerializeField] private GameObject minionPrefab;

    public static MinionManager Instance;

    public delegate void PopulationChanged();
    public PopulationChanged OnPopulationChanged;

    private List<Minion> allMinions;

    public int Population => allMinions.Count;
    public int MaxPopulation { get; private set; } = 10;
    public bool HaveMinionSpace => Population < MaxPopulation - 1; //Keep in mind that minion always produce 2 minions

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        allMinions = new List<Minion>();
    }

    private void Start()
    {
        CreateNewMinion(-1, UnityEngine.Random.insideUnitCircle, Gender.Female);
        CreateNewMinion(-1, UnityEngine.Random.insideUnitCircle, Gender.Female);
        CreateNewMinion(-1, UnityEngine.Random.insideUnitCircle, Gender.Female);
        CreateNewMinion(-1, UnityEngine.Random.insideUnitCircle, Gender.Male);
        CreateNewMinion(-1, UnityEngine.Random.insideUnitCircle, Gender.Male);
        CreateNewMinion(-1, UnityEngine.Random.insideUnitCircle, Gender.Male);
    }

    public void AddMinion(Minion minion)
    {
        allMinions.Add(minion);
        OnPopulationChanged?.Invoke();
    }

    public void RemoveMinion(Minion minion)
    {
        allMinions.Remove(minion);
        OnPopulationChanged?.Invoke();

        if(allMinions.Count <= 0)
        {
            GameManager.Instance.LostGame();
        }

        if(OnlyFemales || OnlyMales)
        {
            GameManager.Instance.KeepPlaying("Your population consists of 1 gender");
        }
    }

    private bool OnlyFemales => allMinions.FindAll(x => x.stats.Gender == Gender.Female).Count == allMinions.Count;
    private bool OnlyMales => allMinions.FindAll(x => x.stats.Gender == Gender.Male).Count == allMinions.Count;

    public List<Minion> FindAvailabePartners(Minion minion)
    {
        var output = new List<Minion>();

        foreach (Minion partner in allMinions)
        {
            //We don't allow same sex reproduction
            if (minion.stats.Gender == partner.stats.Gender || partner.stats.CanReproduce == false || minion == partner || partner.stateMachine.Status == MinionStatus.Reproducing)
                continue;

            output.Add(partner);
        }

        return output;
    }

    public Minion GetTarget(Minion owner)
    {
        float distance = Mathf.Infinity;
        Minion closestMinion = null;

        foreach (var item in allMinions)
        {
            if (item == owner)
                continue;

            if (distance > Vector2.Distance(item.transform.position, owner.transform.position))
            {
                distance = Vector2.Distance(item.transform.position, owner.transform.position);
                closestMinion = item;
            }
        }
        return closestMinion;
    }

    public void CreateNewMinion(int age, Vector2 position, Gender gender = Gender.Random)
    {
        var minion = Instantiate(minionPrefab).GetComponent<Minion>();
        minion.transform.position = position;
        var stats = new MinionStats(minion, gender, age);
        minion.Initialize(stats);

        AddMinion(minion);
    }

    public void IncreaseMaxPopulation(int amount)
    {
        MaxPopulation += amount;
        OnPopulationChanged?.Invoke();
    }
}
