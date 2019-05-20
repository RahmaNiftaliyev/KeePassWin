﻿using KeePass.Win.AppModel;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace KeePass.Win.Services
{
    public class SimpleDatabaseCache : IDatabaseCache, IBackgroundEnteredAware
    {
        private readonly Dictionary<KeePassId, IKeePassDatabase> _idCache = new Dictionary<KeePassId, IKeePassDatabase>();
        private readonly IDatabaseCache _cache;
        private readonly INavigator _navigator;

        public SimpleDatabaseCache(IDatabaseCache cache, INavigator navigator)
        {
            _cache = cache;
            _navigator = navigator;
        }

        public Task<IFile> AddDatabaseAsync(IFilePicker filePicker, bool autoOpen) => _cache.AddDatabaseAsync(filePicker, autoOpen);

        public Task<IEnumerable<IFile>> GetDatabaseFilesAsync() => _cache.GetDatabaseFilesAsync();

        public IObservable<(DatabaseAction action, IFile file)> Databases => _cache.Databases;

        public Task<IFile> AddKeyFileAsync(IFile db, IFilePicker filePicker) => _cache.AddKeyFileAsync(db, filePicker);

        public async Task RemoveDatabaseAsync(IFile dbFile)
        {
            _idCache.Remove(dbFile.IdFromPath());

            await _cache.RemoveDatabaseAsync(dbFile);
        }

        public async Task<IKeePassDatabase> UnlockAsync(KeePassId id, ICredentialProvider provider)
        {
            IKeePassDatabase db;
            if (_idCache.TryGetValue(id, out db))
            {
                return db;
            }

            var unlocked = await _cache.UnlockAsync(id, provider);

            if (unlocked != null)
            {
                _idCache.Add(id, unlocked);
            }

            return unlocked;
        }

        public Task BackgroundEnteredAsync()
        {
            _idCache.Clear();

            return Task.CompletedTask;
        }
    }
}
