using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
namespace ChildApp
{
    public class ParametersException:ApplicationException
    {
        public ParametersException(string message) : base(message) {     }

        public override string Message
        {
            get
            {
                return base.Message;
            }
        }
    }
}
