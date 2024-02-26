using System.Windows.Input;

namespace Standard
{
    public class RelayCommand : ICommand
    {
        private readonly CanExecuteHandler<object?> canExecute;
        private readonly ExecuteHandler<object?> execute;

        /// <summary>Событие извещающее об изменении состояния команды.</summary>
        public event EventHandler? CanExecuteChanged;

        /// <summary>Конструктор команды.</summary>
        /// <param name="execute">Выполняемый метод команды.</param>
        /// <param name="canExecute">Метод, возвращающий состояние команды.</param>
        public RelayCommand(ExecuteHandler<object?> execute, CanExecuteHandler<object?>? canExecute = null)
        {
            this.execute = execute ?? throw new ArgumentNullException(nameof(execute));
            this.canExecute = canExecute ?? AlwaysTrue;
        }

        /// <inheritdoc cref="RelayCommand(ExecuteHandler, CanExecuteHandler)"/>
        public RelayCommand(ExecuteHandler execute, CanExecuteHandler? canExecute = null)
                : this
                (
                      execute is not null ? p => execute() : throw new ArgumentNullException(nameof(execute)),
                      canExecute is not null ? p => canExecute() : AlwaysTrue
                )
        { }

        public static bool AlwaysTrue(object? _) => true;


        /// <summary>Метод, подымающий событие <see cref="CanExecuteChanged"/>.</summary>
        public virtual void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);

        /// <summary>Вызов метода, возвращающего состояние команды.</summary>
        /// <param name="parameter">Параметр команды.</param>
        /// <returns><see langword="true"/> - если выполнение команды разрешено.</returns>
        public bool CanExecute(object? parameter)
        {
            return canExecute(parameter);
        }

        /// <summary>Вызов выполняющего метода команды.</summary>
        /// <param name="parameter">Параметр команды.</param>
        public void Execute(object? parameter)
        {
            execute(parameter);
        }
    }

}
