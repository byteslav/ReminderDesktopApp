﻿using System.ComponentModel;
using Reminder.Entities;

namespace Reminder.DAL
{
    public interface IDatabaseAccess<T> where T: BaseEntity
    {
        BindingList<T> GetAll();
        T GetById(int id);
        void Add(T item);
        void Update(T item);
        void Delete(int id);
    }
}