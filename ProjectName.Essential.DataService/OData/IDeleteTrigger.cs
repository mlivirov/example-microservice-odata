namespace ProjectName.Essential.DataService.OData
{
    public interface IDeleteTrigger<T>
    {
        void BeforeDelete(T model);
    }
}