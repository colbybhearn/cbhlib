using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CbhLib
{
    public class Validator
    {
        public static bool isYear(string y)
        {
            try
            {
                int res = -1;
                if (!int.TryParse(y, out res))
                    return false;
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
