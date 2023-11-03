using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankStorage : MonoBehaviour, IInteractable {
    [SerializeField] private ItemSO tankSO;
    [SerializeField] private Transform spawnPoint;

    void IInteractable.Interact(Transform interactor) {
        Transform tankTransform = Instantiate(tankSO.Prefab, spawnPoint);
        tankTransform.localPosition = Vector3.zero;

        Debug.Log(tankTransform.GetComponent<Tank>().getTankSO().ObjectName);
    }
    string IInteractable.getInteractHint() {
        return "Get a tank (empty)";
    } 
}
