namespace Business.Entity.adapter;

public interface IAdapter<T, D>
{
    D Convert(T t);
}