namespace Core.Cross.Transactions;

/// <summary>
/// Interface que especifica los metodos que debe tener las clases que implementen capacidad transaccional
/// </summary>
public interface ITransactor
{
    /// <summary>
    /// Metodo Transaccional, recibe un delegado que contiene el bloque transaccional
    /// </summary>
    /// <param name="action">Bloque transaccional</param>
    /// <returns>Async Task</returns>
    TR Begin<TR>(Func<TR> action);

    /// <summary>
    /// Metodo Transaccional para soportar operaciones asyncronas, recibe un bloque 
    /// </summary>
    /// <param name="action"></param>
    /// <returns></returns>
    Task<TR> BeginAsync<TR>(Func<Task<TR>> action);
}