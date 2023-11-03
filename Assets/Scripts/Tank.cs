using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour {
    [SerializeField] private ItemSO tankSO;

    public ItemSO getTankSO() {
        return tankSO;
    }
}
