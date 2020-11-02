using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DescartesDiff.Data;
using DescartesDiff.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Text.RegularExpressions;

namespace DescartesDiff.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class DiffController : ControllerBase
    {
        private readonly DiffContext _context;

        public DiffController(DiffContext context)
        {
            _context = context;
        }

        // GET: v1/Diff/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DataModel>> GetDataModel(int id)
        {
            var dataModel = await _context.DiffResults.FindAsync(id);

            if (dataModel == null || dataModel.LeftBase == null || dataModel.RightBase == null) {
                return NotFound();
            }

            var result = new DiffResult();
            if (dataModel.LeftBase.Length != dataModel.RightBase.Length) {
                result.DiffResultType = ResultType.SizeDoNotMatch.ToString();
                return Ok(result.DiffResultType);
            }

            for (int i = 0; i < dataModel.LeftBase.Length; i++) {
                if (dataModel.LeftBase[i] != dataModel.RightBase[i]) {
                    result.Diffs.Add(new Diff() { Offset = i, Length = result.Diffs.Count });
                }
            }

            if (result.Diffs != null && result.Diffs.Count > 0) {
                result.DiffResultType = ResultType.ContentDoNotMatch.ToString();
            } else {
                result.DiffResultType = ResultType.Equals.ToString();
            }

            return Ok(result);
        }

        // PUT: v1/Diff/5/left
        [HttpPut("{id}/left")]
        public async Task<IActionResult> PutDataModelLeft(int id, BaseData baseData)
        {
            if (baseData.Data == null || !IsBase64String(baseData.Data))
            {
                return BadRequest();
            }

            DataModel dataModel = new DataModel();

            if (DataModelExists(id))
            {
                dataModel = await _context.DiffResults.FindAsync(id);
                dataModel.LeftBase = baseData.Data;
                _context.Entry(dataModel).State = EntityState.Modified;
            } else {
                dataModel.LeftBase = baseData.Data;
                dataModel.DataModelId = id;
                _context.DiffResults.Add(dataModel);
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DataModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction(nameof(PutDataModelLeft), new { id = dataModel.DataModelId }, dataModel);
        }

        // PUT: v1/Diff/5/right
        [HttpPut("{id}/right")]
        public async Task<IActionResult> PutDataModelRight(int id, BaseData baseData)
        {
            if (baseData.Data == null || !IsBase64String(baseData.Data))
            {
                return BadRequest();
            }

            DataModel dataModel = new DataModel();

            if (DataModelExists(id))
            {
                dataModel = await _context.DiffResults.FindAsync(id);
                dataModel.RightBase = baseData.Data;
                _context.Entry(dataModel).State = EntityState.Modified;
            } else
            {
                dataModel.RightBase = baseData.Data;
                dataModel.DataModelId = id;
                _context.DiffResults.Add(dataModel);
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DataModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction(nameof(PutDataModelRight), new { id = dataModel.DataModelId }, dataModel);
        }

        private bool DataModelExists(int id)
        {
            return _context.DiffResults.Any(e => e.DataModelId == id);
        }

        private bool IsBase64String(string s)
        {
            s = s.Trim();
            return (s.Length % 4 == 0) && Regex.IsMatch(s, @"^[a-zA-Z0-9\+/]*={0,3}$", RegexOptions.None);
        }
    }
}
