using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;

public class ConveyorBelt : MonoBehaviour
{
    [SerializeField] private CinemachinePathBase cinemachineTrack = null;
    [SerializeField] private CinemachineDollyCart dollyCartPrefab = null;
    [SerializeField] private GameObject samplePart = null;
    
    private List<ConveyorBeltPart> _partsOnBelt = new List<ConveyorBeltPart>();
    
    private void Update()
    {
        for (int i = _partsOnBelt.Count - 1; i >= 0; i--)
        {
            var part = _partsOnBelt[i];

            if (!(part.DollyCart.m_Position >= 1)) continue;
            
            part.Part.transform.SetParent(null);
            part.Part.GetComponent<Rigidbody>().isKinematic = false;
            Destroy(part.DollyCart.gameObject);
            _partsOnBelt.Remove(part);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            AddToBelt(samplePart);
        }
    }
    
    public void AddToBelt(GameObject part)
    {
        CinemachineDollyCart newDollycart = Instantiate(dollyCartPrefab, transform);
        newDollycart.m_Position = 0;
        newDollycart.m_Path = cinemachineTrack;
        
        GameObject newPart = Instantiate(part, newDollycart.transform);
        newPart.GetComponent<Rigidbody>().isKinematic = true;
        
        _partsOnBelt.Add(new ConveyorBeltPart(newDollycart, newPart));
    }
}
