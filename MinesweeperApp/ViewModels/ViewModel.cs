﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Graphics.Text;
using MinesweeperApp.Models;
using MinesweeperApp.Services;
using System.Data;
using System.Linq.Expressions;
using System.Windows.Input;
using Syncfusion.Maui.Core.Converters;

namespace MinesweeperApp.ViewModels
{
    public class ViewModel:INotifyPropertyChanged
    {
        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        protected readonly Service service;

        private bool inServerCall;
        public bool InServerCall { get { return inServerCall; } set { inServerCall = value; OnPropertyChanged(); } }

        private static bool isAdmin;
        public bool IsAdmin { get { return isAdmin; } set { isAdmin = value; OnPropertyChanged(); } }


        public ViewModel(Service service_)
        {
            this.service= service_;
            InServerCall= false;
        }
    }
}
