using Standard;

namespace WpfCore
{

    /// <summary>Реализация RelayCommand для методов с обобщённым параметром.</summary>
    /// <typeparam name="T">Тип параметра методов.</typeparam>
    public class RelayCommand<T> : RelayCommand
    {
        /// <summary> Command constructor. </summary>
        /// <param name = "execute"> Command method to execute. </param>
        /// <param name = "canExecute"> Method that returns the state of the command. </param>
        /// <param name="converter">Optional converter to convert <see cref="object"/> to <typeparamref name="T"/>. <br/>
        /// It is called when the parameter
        /// <see href="https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/is">
        /// is not compatible</see> with a <typeparamref name="T"/> type.
        /// </param>
        public RelayCommand(ExecuteHandler<T?> execute, CanExecuteHandler<T?> canExecute, ConverterFromObjectHandler<T?>? converter = null)
            : base
            (
                  execute is null ? throw new ArgumentNullException(nameof(execute)) : 
                  converter is null
                  ? p =>
                    {
                        if (p is T t)
                        {
                            execute(t);
                        }
                    }
                  : p =>
                    {
                        if (p is T t || converter(p, out t))
                        {
                            execute(t);
                        }
                  },
                  canExecute is null ? throw new ArgumentNullException(nameof(canExecute)) : 
                  converter is null
                  ? p => (p is T t) && canExecute(t)
                  : p => ((p is T t) || converter(p, out t)) &&
                         canExecute(t)
            )
        { }

        /// <summary> Command constructor. </summary>
        /// <param name = "execute"> Command method to execute. </param>
        /// <param name="converter">Optional converter to convert <see cref="object"/> to <typeparamref name="T"/>.</param>
        public RelayCommand(ExecuteHandler<T?> execute, ConverterFromObjectHandler<T?>? converter = null)
            : base
            (
                  execute is null ? throw new ArgumentNullException(nameof(execute)) :
                  converter is null
                  ? p =>
                  {
                      if (p is T t)
                      {
                          execute(t);
                      }
                  }
                  : p =>
                  {
                      if (p is T t || converter(p, out t))
                      {
                          execute(t);
                      }
                  },
                  converter is null
                  ? p => p is T t
                  : p => p is T t || converter(p, out t)
            )
        { }
    }
}
