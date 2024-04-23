using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankStorage : MonoBehaviour, IInteractable, ShootingTarget
{
    [SerializeField] private ItemSO tankSO;

    void IInteractable.Interact(Transform interactor)
    {
        if (interactor.GetComponent<IItemCarrier>().IsEmpty())
        {
            Transform tankTransform = Instantiate(tankSO.Prefab);
            tankTransform.GetComponent<Tank>().SetCarrier(interactor.GetComponent<IItemCarrier>());
        }
    }

    string IInteractable.getInteractHint()
    {
        return "Get a tank (empty)";
    }

    void ShootingTarget.OnHit()
    {
        Debug.Log(gameObject);
    }
}
