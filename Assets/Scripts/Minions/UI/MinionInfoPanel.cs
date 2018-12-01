using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class MinionInfoPanel : MonoBehaviour, IInfoPanel
{
    [SerializeField] private Text gender;
    [SerializeField] private Text age;
    [SerializeField] private Text status;
    [SerializeField] private Slider health;
    [SerializeField] private Slider hunger;
    [SerializeField] private Slider sanity;

    public GameObject GameObject => gameObject;
    public Transform Transform => transform;

    public void UpdateInfo(IStats stats)
    {
        MinionStats minionStats = (MinionStats)stats;
        gender.text = $"Gender: {minionStats.Gender}";
        age.text = $"Age: {minionStats.Age}";
        status.text = $"Status: {minionStats.Status}";

        health.value = minionStats.Health;
        hunger.value = minionStats.Hunger;
        sanity.value = minionStats.Sanity;
    }
}

