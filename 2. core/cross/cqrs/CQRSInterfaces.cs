namespace Core.Cross.Cqrs;
/// <summary>
/// Interface que representa una clase de Query en el patron CQRS. 
/// En esta clase se debe implementar la lógica de su aplicación para acciones de solo lectura de datos
/// </summary>
/// <typeparam name="QI">Clase que representa los datos de entrada para la query</typeparam>
/// <typeparam name="QR">Clase que representa los datos de resultado de la query</typeparam>
public interface IQueryHandler<in QI, QR>
{
    /// <summary>
    /// Metodo para implementar la query según los datos de entrada "TQI" luego devuelve el resultado "TQR"
    /// </summary>
    /// <param name="queryIn">Datos de entrada para la query</param>
    /// <param name="cancellation">Token de operación</param>
    /// <returns></returns>
    Task<QR> Handle(CancellationToken cancellation, QI? queryIn = default);
}

/// <summary>
/// Interface que representa una clase de Command en el patron CQRS.
/// En esta clase se debe implementar la lógica de la aplicación para operaciones que impliquen escritura de datos
/// </summary>
/// <typeparam name="CI">Clase que representa los datos de entrada para el comando</typeparam>
/// <typeparam name="CR">Clase que representa los datos de salida para el comando</typeparam>
public interface ICommandHandler<in CI, CR>
{
    /// <summary>
    /// Metodo para implementar el comando según los datos entrada "TCI" luego devuelve el resultado "TCR"
    /// </summary>
    /// <param name="commandIn">Datos de entrada</param>
    /// <param name="cancellation">Token de operación</param>
    /// <returns></returns>
    Task<CR> Handle(CancellationToken cancellation, CI commandIn);
}