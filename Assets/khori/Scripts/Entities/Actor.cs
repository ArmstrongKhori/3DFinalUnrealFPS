using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An "Actor" is something which has the attributes to... Well, "act".
/// Its behavior is dynamic and does not need to be manipulated by anything else to operate.
/// </summary>
public class Actor : Entity {


    public virtual void Act() { }


}
