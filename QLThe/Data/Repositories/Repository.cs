﻿using Microsoft.EntityFrameworkCore;
using QLThe.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace QLThe.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public readonly QLTheDbContext _context;

        public Repository(QLTheDbContext context)
        {
            _context = context;
        }

        //protected void Save() => _context.SaveChanges();
        public int Count(Func<T, bool> predicate)
        {
            return _context.Set<T>().Where(predicate).Count();
        }

        public void Create(T entity)
        {
            _context.Add(entity);
            // Save();
        }

        public void Delete(T entity)
        {
            _context.Remove(entity);
            // Save();
        }

        public IEnumerable<T> Find(Func<T, bool> predicate)
        {
            return _context.Set<T>().Where(predicate);
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>();
        }

        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public void Update(T entity)
        {
            _context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

        }

        public async Task<T> GetByIdAsync(int? id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task Save() => await _context.SaveChangesAsync();

        public T GetSingleNoTracking(Func<T, bool> predicate)
        {
            return _context.Set<T>().AsNoTracking().SingleOrDefault(predicate);
        }

        public async Task<IEnumerable<T>> GetAllIncludeAsync(Expression<Func<T, object>> predicate, Expression<Func<T, object>> predicate2)
        {
            return await _context.Set<T>().Include(predicate).Include(predicate2).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllIncludeOneAsync(Expression<Func<T, object>> expression)
        {
            return await _context.Set<T>().Include(expression).ToListAsync();
        }

        public T GetByStringId(string id)
        {
            return _context.Set<T>().Find(id);
        }

        public async Task<IEnumerable<T>> FindIncludeOneAsync(Expression<Func<T, object>> expressObj, Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>().Include(expressObj).Where(expression).ToListAsync();
        }

        public IEnumerable<T> GetAllIncludeOne(Expression<Func<T, object>> expression)
        {
            return _context.Set<T>().Include(expression);
        }

        public IEnumerable<T> FindIncludeOne(Expression<Func<T, object>> expressObj, Func<T, bool> predicate)
        {
            return _context.Set<T>().Include(expressObj).Where(predicate);
        }
    }
}
