using System;
using GradesManager.db;

namespace GradesManager.service
{
    public static class SaveUser
    {
        public static void Start()
        {
            var timer = new System.Threading.Timer(
                e => DataBase.SaveUsers(),
                null,
                TimeSpan.Zero,
                TimeSpan.FromSeconds(1));
        }
    }
}