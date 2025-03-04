using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.MainGame
{
    public class SRDebuggerInitializer : MonoBehaviour
    {
        void Start()
        {
            // Đăng ký SROptions để nó xuất hiện trong SRDebugger
            SRDebug.Instance.AddOptionContainer(new SROptions());
        }
    }
}
