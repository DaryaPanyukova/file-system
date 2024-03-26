namespace FileSystem.Services.Visitors;

public interface IConverter<TFrom, TTo>
{
    TTo Convert(TFrom source);
}