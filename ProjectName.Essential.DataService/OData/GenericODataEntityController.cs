using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectName.Dal.Core;
using ProjectName.Essential.Dal.Ef.Extensions;

namespace ProjectName.Essential.DataService.OData
{
    [GenericODataControllerNameConvention]
    public class GenericODataEntityController<T> : ODataController where T: class, IEntity 
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IModelQuery<T> _query;
        
        private readonly ICreateTrigger<T> _createTrigger;
        private readonly IUpdateTrigger<T> _updateTrigger;
        private readonly IDeleteTrigger<T> _deleteTrigger;

        public GenericODataEntityController(
            IUnitOfWork unitOfWork,
            IModelQueryBuilder modelQueryBuilder,
            ICreateTrigger<T> createTrigger = null,
            IUpdateTrigger<T> updateTrigger = null,
            IDeleteTrigger<T> deleteTrigger = null)
        {
            _unitOfWork = unitOfWork;
            _query = modelQueryBuilder.Build<T>();
            _createTrigger = createTrigger;
            _updateTrigger = updateTrigger;
            _deleteTrigger = deleteTrigger;
        }

        [EnableQuery]
        public IQueryable<T> Get()
        {
            return _query.AsQueryable();
        }

        [EnableQuery]
        public async Task<IActionResult> Get([FromODataUri] int key)
        {
            var result = await _query.FirstOrDefaultAsync(p => p.Id == key);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [EnableQuery]
        public async Task<IActionResult> Post([FromBody] T model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _createTrigger?.BeforeCreate(model);

            _unitOfWork.Add(model);
            await _unitOfWork.SaveAsync();
            return Created(model);
        }
        
        [EnableQuery]
        public async Task<IActionResult> Put([FromODataUri] int key, [FromBody] Delta<T> model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entity = await _query.FirstOrDefaultAsync(p => p.Id == key);
            if (entity == null)
            {
                return NotFound();
            }

            model.Patch(entity);
            _updateTrigger?.BeforeUpdate(entity);
            
            try
            {
                _unitOfWork.Update(entity);
                await _unitOfWork.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _query.FirstOrDefaultAsync(p => p.Id == key) == null)
                {
                    return NotFound();
                }
                
                throw;
            }
            
            return Updated(entity);
        }

        [EnableQuery]
        public async Task<IActionResult> Delete([FromODataUri] int key)
        {
            var entity = await _query.FirstOrDefaultAsync(p => p.Id == key);
            if (entity == null)
            {
                return NotFound();
            }

            _deleteTrigger?.BeforeDelete(entity);
            _unitOfWork.Remove(entity);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }
    }
}