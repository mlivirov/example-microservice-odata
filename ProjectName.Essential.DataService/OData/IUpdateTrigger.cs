namespace ProjectName.Essential.DataService.OData
{
    public interface IUpdateTrigger<T>
    {
        void BeforeUpdate(T model);
    }
}