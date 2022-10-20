using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T> {
    public virtual void Awake () {
        if (Nested.instance == this) {
            // We're already loaded, brought into new scene.
            return;
        }
        if (Nested.instance != null) {
            Debug.LogError("An instance of the singleton " + typeof(T) + " already exists, attempting to create additional.");
        }
        Nested.instance = (T)this;
    }

    public virtual void OnDestroy () {
        if (Nested.instance == this) {
            Nested.instance = null;
        }
    }

    /** Returns the instance of this singleton. */

    public static T Instance {
        get {
            return Nested.instance;
        }
    }

    // This lets us end up with one for each final type...
    private class Nested
    {
        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static Nested() { }

        internal static T instance;
    }
}

