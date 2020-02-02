using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerDestroyedManager : MonoBehaviour
{
    public static event Action<int> OnTowerDestroyed = delegate(int i) {  };
    
    private int _towersDestroyed = 0;
    public int TowerDestroyed => _towersDestroyed;

    public void AddTower()
    {
        _towersDestroyed += 1;
        OnTowerDestroyed.Invoke(_towersDestroyed);
    }
}
