using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SharedLibrary
{
   
    /// <summary>
    /// Una classe IDisposable che esegue un'azione delegata (Action) quando viene smaltita.
    /// Utile per la pulizia di risorse o stati in Blazor.
    /// </summary>
    public class DelegateDisposable : IDisposable
    {
        private Action? _disposeAction;

        public DelegateDisposable(Action disposeAction)
        {
            _disposeAction = disposeAction ?? throw new ArgumentNullException(nameof(disposeAction));
        }

        public void Dispose()
        {
            // Impedisce che il codice venga eseguito due volte.
            if (_disposeAction == null)
                return;
            
            // Esegue l'azione di pulizia.
            _disposeAction.Invoke();
            _disposeAction = null; 
            
            GC.SuppressFinalize(this);
        }
    }
}
