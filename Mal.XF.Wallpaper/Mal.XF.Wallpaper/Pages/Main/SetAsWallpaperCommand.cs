﻿using System;
using System.Collections.Generic;
using System.Text;
using Mal.XF.Wallpaper.Services;
using Prism.Commands;

namespace Mal.XF.Wallpaper.Pages.Main
{
    internal class SetAsWallpaperCommand:DelegateCommandBase
    {
        private readonly IBingWallpaperService bingWallpaperService;

        public SetAsWallpaperCommand(IBingWallpaperService bingWallpaperService)
        {
            this.bingWallpaperService = bingWallpaperService;
        }

        public bool CanExecute(string filePath) => this.IsActive && !string.IsNullOrWhiteSpace(filePath);

        public async void Execute(string filePath)
        {
            try
            {
                this.IsActive = false;
                this.RaiseCanExecuteChanged();
                await this.bingWallpaperService.SetImageAsWallpaperAsync(filePath);
            }
            finally
            {
                this.IsActive = true;
                this.RaiseCanExecuteChanged();
            }
        }

        protected override void Execute(object parameter) => this.Execute(parameter as string);

        protected override bool CanExecute(object parameter) => this.CanExecute(parameter as string);
    }
}