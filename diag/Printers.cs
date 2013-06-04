using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CbhLib.diag
{
    public class Printers
    {
        public static string Print(SortedList<string, object> soc)
        {
            StringBuilder sb = new StringBuilder();
            int nameLength = 0;
            for (int i = 0; i < soc.Count; i++)
                if (soc.Keys[i].Length > nameLength)
                    nameLength = soc.Keys[i].Length;

            for (int i = 0; i < soc.Count; i++)
            {                
                sb.Append(soc.Keys[i]);
                for (int s = 0; s < nameLength - soc.Keys[i].Length; s++)
                    sb.Append(" ");
                if (soc.Values[i] == null)
                {
                    sb.AppendLine(" : <NULL>");
                    continue;
                }
                    
                sb.AppendLine(" : "+soc.Values[i].ToString());
            }
            return sb.ToString();
        }
    }
}
