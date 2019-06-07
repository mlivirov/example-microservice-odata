namespace ProjectName.Essential.DataService.OData
{
    public interface ICreateTrigger<T>
    {
        void BeforeCreate(T model);
    }
}