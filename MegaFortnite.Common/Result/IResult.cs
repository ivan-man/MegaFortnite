using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MegaFortnite.Common.Enums;

namespace MegaFortnite.Common.Result
{
    public interface IResult
    {
        ErrorCode Code { get; set; }
        bool Success { get; set; }
        string Message { get; set; }
    }
}
