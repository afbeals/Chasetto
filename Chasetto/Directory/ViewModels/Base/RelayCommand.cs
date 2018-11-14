﻿using System;
using System.Windows.Input;

namespace Chasetto
{
    /// <summary>
    /// A basic command that runs an Action
    /// </summary>
    public class RelayCommand : ICommand
    {

        #region Private Members

        /// <summary>
        /// The action to run
        /// </summary>
        private Action mAction;

        #endregion Private Members

        #region Public Events
        /// <summary>
        /// The event that's fired when the <see cref="CanExecute(objectJ)"/> value has changed
        /// </summary>
        public event EventHandler CanExecuteChanged = (sender, e) => { };

        #endregion Public Events

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public RelayCommand(Action action)
        {
            mAction = action;
        }

        #endregion Constructor

        #region Command Methods

        /// <summary>
        /// A relay command can always execute
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Exectutes the commands Action
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            mAction();
        }

        #endregion Command Methods
    }
}