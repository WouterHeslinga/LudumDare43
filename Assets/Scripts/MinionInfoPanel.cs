using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class MinionInfoPanel : MonoBehaviour
{
    [SerializeField] private Text gender;
    [SerializeField] private Text age;
    [SerializeField] private Slider health;
    [SerializeField] private Slider hunger;
    [SerializeField] private Slider sanity;

    public void UpdateInfo(MinionStats stats)
    {
        gender.text = $"Gender: {stats.Gender}";
        age.text = $"Age: {stats.Age}";

        health.value = stats.Health;
        hunger.value = stats.Hunger;
        sanity.value = stats.Sanity;
    }
}

