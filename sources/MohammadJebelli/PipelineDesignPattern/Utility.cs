using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipelineDesignPattern;

public static class Utility
{
    public static string FirstLetterToUpper(this string stringToModify)
    {
        if (stringToModify is null)
            return null;

        if (stringToModify.Length > 1)
            return $"{char.ToUpper(stringToModify[0])}{stringToModify[1..]}";

        return stringToModify.ToUpper();
    }

}

