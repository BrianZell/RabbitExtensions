using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDT.ManualShovel
{
    public class DisposerComponent : Component
    {
        private Action<bool> _disposeAction;

        public DisposerComponent(Action<bool> disposeAction)
        {
            _disposeAction = disposeAction;
        }

        protected override void Dispose(bool disposing)
        {
            _disposeAction(disposing);
            base.Dispose(disposing);
        }
    }
}
