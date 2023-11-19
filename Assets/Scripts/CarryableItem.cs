using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface CarryableItem {
    void SetParent(IItemCarrier carrier);
    void RemoveParent();
    IItemCarrier ReturnParent();
}
