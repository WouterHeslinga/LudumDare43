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

    private List<Minion> allMinions;

    public int Population => allMinions.Count;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        allMinions = new List<Minion>();
    }

    private void Start()
    {
        CreateNewMinion(-1, UnityEngine.Random.insideUnitCircle);
        CreateNewMinion(-1, UnityEngine.Random.insideUnitCircle);
        CreateNewMinion(-1, UnityEngine.Random.insideUnitCircle);
        CreateNewMinion(-1, UnityEngine.Random.insideUnitCircle);
        CreateNewMinion(-1, UnityEngine.Random.insideUnitCircle);
    }

    public void AddMinion(Minion minion)
    {
        this.allMinions.Add(minion);
    }

    public void RemoveMinion(Minion minion)
    {
        allMinions.Remove(minion);
    }

    public List<Minion> FindAvailabePartners(Minion minion)
    {
        var output = new List<Minion>();

        foreach (Minion partner in allMinions)
        {
            //We don't allow same sex reproduction
            if (minion.stats.Gender == partner.stats.Gender || partner.stats.CanReproduce == false && minion == partner)
                continue;

            output.Add(partner);
        }

        return output;
    }

    public void CreateNewMinion(int age, Vector2 position)
    {
        var minion = Instantiate(minionPrefab).GetComponent<Minion>();
        minion.transform.position = position;
        var stats = new MinionStats(minion, age);

        allMinions.Add(minion);

        minion.Initialize(stats);
    }
}
