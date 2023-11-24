using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItemCarrier {
    Transform GetMountingPoint();
    bool Inject(ICarryableItem item);
    void Eject();
    bool IsEmpty();
    ICarryableItem GetItem();
}
