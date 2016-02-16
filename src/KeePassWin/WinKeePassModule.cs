﻿using Autofac;
using KeePass.IO;
using KeePass.IO.Database;
using KeePass.Models;
using KeePassWin.Mvvm;
using System;

namespace KeePassWin
{
    internal class WinKeePassModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DatabaseCache>()
                .SingleInstance();

            builder.RegisterType<DatabaseTracker>()
                .SingleInstance();

            builder.RegisterType<DialogDatabaseUnlocker>()
                .Named<IDatabaseUnlocker>(nameof(DialogDatabaseUnlocker))
                .SingleInstance();

            builder.RegisterDecorator<IDatabaseUnlocker>((c, inner) => new CachedDatabseUnlocker(inner), fromKey: nameof(DialogDatabaseUnlocker))
                .SingleInstance();

            builder.RegisterType<TimedClipboard>()
                .WithParameter(TypedParameter.From(TimeSpan.FromSeconds(10)))
                .As<IClipboard>()
                .SingleInstance();

            builder.RegisterType<PrismNavigationService>()
                .As<INavigator>()
                .SingleInstance();
        }
    }
}