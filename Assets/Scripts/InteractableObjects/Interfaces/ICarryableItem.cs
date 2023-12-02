using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICarryableItem {
    void SetParent(IItemCarrier carrier);
    void RemoveParent();
    IItemCarrier ReturnParent();
}
