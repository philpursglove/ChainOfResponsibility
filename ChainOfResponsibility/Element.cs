using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChainOfResponsibility
{
    public class Element
    {
        public object Successor { get; set; }

        public void setSuccessor(object successor)
        {
            Successor = successor;
        }
    }
}
