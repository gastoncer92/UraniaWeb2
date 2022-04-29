using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace UraniaWeb.Models
{
    public class UraniaWebDbContext : DbContext
    {
        public UraniaWebDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Administrador> Admins { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Slider> Sliders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            modelbuilder.Entity<Administrador>().ToTable("Administrador");
            modelbuilder.Entity<Article>().ToTable("Article");
            modelbuilder.Entity<Slider>().ToTable("Slider");
        }
    }




    

    public interface IRepository<T> where T : class
    {
        T Get(int id);

        IEnumerable<T> GetAll(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = null
        );

        T GetFirstOrDefault(
            Expression<Func<T, bool>> filter = null,
            string includeProperties = null
        );

        void Add(T entity);

        void Remove(int id);

        void Remove(T entity);

        
    }


    public class Repository<T> : IRepository<T> where T : class
    {

        protected readonly DbContext _context;
        internal DbSet<T> _dbSet;

        public Repository(DbContext context)
        {
            _context = context;
            this._dbSet = context.Set<T>();
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public T Get(int id)
        {
            return _dbSet.Find(id);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = null)
        {
            IQueryable<T> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }

            return query.ToList();
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> filter = null, string includeProperties = null)
        {
            IQueryable<T> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            return query.FirstOrDefault();
        }

        public void Remove(int id)
        {
            T entityToRemove = _dbSet.Find(id);
            Remove(entityToRemove);
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }


        public interface IRepository<T> where T : class
        {
            T Get(int id);

            IEnumerable<T> GetAll(
                Expression<Func<T, bool>> filter = null,
                Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                string includeProperties = null
            );

            T GetFirstOrDefault(
                Expression<Func<T, bool>> filter = null,
                string includeProperties = null
            );

            void Add(T entity);

            void Remove(int id);

            void Remove(T entity);
        }
        public interface IArticuloRepository : IRepository<Article>
        {
            void Update(Article articulo);
        }

        public interface ISliderRepository : IRepository<Slider>
        {
            void Update(Slider slider);
        }

        public interface IUsuarioRepository : IRepository<Administrador>
        {
            void BloquearUsuario(string IdUsuario);

            void DesbloquearUsuario(string IdUsuario);
        }
        public interface IContenedorTrabajo : IDisposable
        {

            IArticuloRepository Articulo { get; }

            ISliderRepository Slider { get; }

            IUsuarioRepository Usuario { get; }

            void Save();
        }


    }










}
