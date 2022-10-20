using System;
using System.Collections.Generic;

using UnityEngine;

public class SceneContext : Singleton<SceneContext> {

    public GameController GameController {
        get; private set;
    }

    public override void Awake () {
        base.Awake();
        this.GameController = GetComponent<GameController>();
    }
}
