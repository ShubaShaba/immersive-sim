using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICarryableItem
{
    void SetCarrier(IItemCarrier carrier);
    void RemoveCarrier();
    IItemCarrier ReturnCarrier();
}
