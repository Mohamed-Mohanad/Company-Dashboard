﻿using Company.BLL.Interfaces;
using Company.DAL.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.BLL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly CompanyDbContext _context;

        public GenericRepository(CompanyDbContext context)
        {
            _context = context;
        }
        public async Task<int> Create(T obj)
        {
            await _context.Set<T>().AddAsync(obj);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(T obj)
        {
            _context.Set<T>().Remove(obj);
            return await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAll() => await _context.Set<T>().ToListAsync();

        public async Task<T> Get(int? id) => await _context.Set<T>().FindAsync(id);

        public async Task<int> Update(T obj)
        {
            _context.Set<T>().Update(obj);
            return await _context.SaveChangesAsync();
        }

    }
}
