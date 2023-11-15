using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankStorage : MonoBehaviour, IInteractable{
    [SerializeField] private ItemSO tankSO;

    void IInteractable.Interact(Transform interactor) {
        if (interactor.GetComponent<ItemCarrier>().IsEmpty()) {
            Transform tankTransform = Instantiate(tankSO.Prefab);
            tankTransform.GetComponent<Tank>().SetParent(interactor.GetComponent<ItemCarrier>());
        }
    }
    string IInteractable.getInteractHint() {
        return "Get a tank (empty)";
    }
}
