using System;
using Microsoft.EntityFrameworkCore.Internal;
using ProjectName.Essential.Dal.Core.Models;

namespace ProjectName.Essential.DataService.OData.Triggers
{
    public class PersonTrigger : 
        ICreateTrigger<Person>, 
        IDeleteTrigger<Person>
    {
        public void BeforeCreate(Person model)
        {
            model.FirstName = model.FirstName + "_test";
        }

        public void BeforeDelete(Person model)
        {
            if (model.PersonAddress.Any())
            {
                throw new NotImplementedException("cannot delete person with address");
            }
        }
    }
}