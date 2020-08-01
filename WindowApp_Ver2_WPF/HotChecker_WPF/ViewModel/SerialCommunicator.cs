using HotChicken.Serial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotChecker_WPF.ViewModel
{
    public class SerialCommunicator
    {
       public static SerialManager serialManager = new SerialManager(9600, 8, 1, 0);
    }
}
