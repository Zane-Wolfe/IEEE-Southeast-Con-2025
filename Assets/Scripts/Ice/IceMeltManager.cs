using System.Collections.Generic;
using UnityEngine;

public class IceMeltManager : MonoBehaviour
{
    public static IceMeltManager Instance { get; private set; }

    private List<Ice> ices = new List<Ice>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        for (int i = ices.Count - 1; i >= 0; i--)
        {
            ices[i].Melt(Time.deltaTime);

            if (ices[i].IsMelted())
            {
                Destroy(ices[i].gameObject);
                ices.RemoveAt(i);
            }
        }
    }

    public void AddIce(Ice ice)
    {
        if (!ices.Contains(ice))
        {
            ices.Add(ice);
        }
    }

    public void RemoveIce(Ice ice)
    {
        ices.Remove(ice);
    }
}