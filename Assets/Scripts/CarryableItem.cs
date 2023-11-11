using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface CarryableItem {
    void SetParent(ItemCarrier carrier);
    void RemoveParent();
    ItemCarrier ReturnParent();
}
