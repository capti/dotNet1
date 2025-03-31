using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Net_Stack_
{
    public class StackViewModel : INotifyPropertyChanged
    {
        private readonly Stack<string> _stack;

        public StackViewModel()
        {
            _stack = new Stack<string>();
            PushCommand = new RelayCommand(Push);
            PopCommand = new RelayCommand(Pop, CanPop);
            ClearCommand = new RelayCommand(Clear, CanClear);
        }

        public string CurrentItem => _stack.IsEmpty ? "Стек пуст" : _stack.CurrentItem;
        public int Count => _stack.Count;
        public bool IsEmpty => _stack.IsEmpty;

        public ICommand PushCommand { get; }
        public ICommand PopCommand { get; }
        public ICommand ClearCommand { get; }

        private void Push(object parameter)
        {
            if (parameter is string item)
            {
                _stack.Push(item);
                OnPropertyChanged(nameof(CurrentItem));
                OnPropertyChanged(nameof(Count));
                OnPropertyChanged(nameof(IsEmpty));
                ((RelayCommand)PopCommand).RaiseCanExecuteChanged();
                ((RelayCommand)ClearCommand).RaiseCanExecuteChanged();
            }
        }

        private void Pop(object parameter)
        {
            if (!_stack.IsEmpty)
            {
                _stack.Pop();
                OnPropertyChanged(nameof(CurrentItem));
                OnPropertyChanged(nameof(Count));
                OnPropertyChanged(nameof(IsEmpty));
                ((RelayCommand)PopCommand).RaiseCanExecuteChanged();
                ((RelayCommand)ClearCommand).RaiseCanExecuteChanged();
            }
        }

        private bool CanPop(object parameter)
        {
            return !_stack.IsEmpty;
        }

        private void Clear(object parameter)
        {
            _stack.Clear();
            OnPropertyChanged(nameof(CurrentItem));
            OnPropertyChanged(nameof(Count));
            OnPropertyChanged(nameof(IsEmpty));
            ((RelayCommand)PopCommand).RaiseCanExecuteChanged();
            ((RelayCommand)ClearCommand).RaiseCanExecuteChanged();
        }

        private bool CanClear(object parameter)
        {
            return !_stack.IsEmpty;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        public event EventHandler CanExecuteChanged;

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}