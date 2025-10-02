using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld
{
    class ViewModel : BindableBase
    {
        public ViewModel() {
            ChangeMessageCommand = new DelegateCommand<string>(
                (par)=> GreetingMessage = par);
        }

        private string _greetingMessage = "Hellow World";
        public string GreetingMessage { 
            get=>_greetingMessage;
            set {
                if (SetProperty(ref _greetingMessage, value)) {
                    CanChangeMessage = false;
                }
                
            }
        }

        private bool _canChangeMessage = true;
        public bool CanChangeMessage {
            get => _canChangeMessage;
            private set => SetProperty(ref _canChangeMessage, value);
        }

        public string NewMessage1 { get; } = "IRIS OUT";
        public string NewMessage2 { get; } = "JANE DOE";
        public DelegateCommand<string> ChangeMessageCommand { get; }
    }
}
