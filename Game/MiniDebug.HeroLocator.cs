using System;
using System.Collections.Generic;
using System.Text;

namespace MiniDebug
{
    public partial class MiniDebug
    {
        private void ReacquireHero()
        {
            _heroController = FindFirstObjectByType<HeroController>();
            if (_heroController != null)
                Logger.LogInfo("HeroController found.");
        }
    }
}
