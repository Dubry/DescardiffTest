using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DescartesDiff.Models
{
    public class DiffResult
    {
        public string DiffResultType { get; set; }
        public List<Diff> Diffs { get; set; } = new List<Diff>();
    }

    public class Diff
    {
        public int Offset { get; set; }
        public int Length { get; set; }
    }

    public enum ResultType
    {
        ContentDoNotMatch = 0,
        SizeDoNotMatch = 1,
        Equals = 2,
        NotBase64= 3
    }
}
