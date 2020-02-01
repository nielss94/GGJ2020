using Cinemachine;
using UnityEngine;

public class ConveyorBeltPart
{
    private CinemachineDollyCart _cinemachineDollyCart = null;
    public CinemachineDollyCart DollyCart => _cinemachineDollyCart;
    
    private GameObject _part = null;
    public GameObject Part => _part;
    
    public ConveyorBeltPart(CinemachineDollyCart cinemachineDollyCart, GameObject part)
    {
        _cinemachineDollyCart = cinemachineDollyCart;
        _part = part;
    }
}