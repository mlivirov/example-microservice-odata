using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ProjectName.Dal.Core.Ef.Scaffolding
{
    public class ModelDecorator : IModel
    {
        private readonly IModel _origin;
        private readonly string[] _exclude;

        public object this[string name] => _origin[name];
        
        public ModelDecorator(IModel origin, params string[] exclude)
        {
            _origin = origin;
            _exclude = exclude;
        }
        
        public IAnnotation FindAnnotation(string name)
        {
            return _origin.FindAnnotation(name);
        }

        public IEnumerable<IAnnotation> GetAnnotations()
        {
            return _origin.GetAnnotations();
        }

        public IEnumerable<IEntityType> GetEntityTypes()
        {
            var result = _origin.GetEntityTypes().ToList();

            return result.Except(result.Where(p => _exclude.Contains(p.Name.ToLowerInvariant())));
        }

        public IEntityType FindEntityType(string name)
        {
            return _origin.FindEntityType(name);
        }

        public IEntityType FindEntityType(string name, string definingNavigationName, IEntityType definingEntityType)
        {
            return _origin.FindEntityType(name, definingNavigationName, definingEntityType);
        }
    }
}