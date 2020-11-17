using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using tarea6y7.Data;

namespace tarea6y7.Services
{
        public interface ITesoroService
        {
            Task<List<Treasure>> GetAllTreasure(Guid Uid);
            Task<Treasure> GetTreasure(Guid Tid);
            Task<bool> SetTreasure(Treasure T);
            Task<Coordinate> GetCoordinate(Guid Tid);
            Task<List<Coordinate>> GetAllCoordinate(Guid Uid);
        }

        public class TesoroService : ITesoroService
        {
            private readonly ApplicationDbContext db;
            public TesoroService(ApplicationDbContext _dbContext)
            {
                db = _dbContext;
            }
            public Task<List<Coordinate>> GetAllCoordinate(Guid Uid)
            {
                List<Coordinate> coordinates = db.Coordinates
                .Where(c => c.Uid.Equals(Uid)).ToList<Coordinate>();
                return Task.FromResult(coordinates);
            }

            public async Task<List<Treasure>> GetAllTreasure(Guid _Uid)
            {
                List<Treasure> treasures = await db.Treasures
                .Where(t => t.Uid.Equals(_Uid))
                .ToListAsync<Treasure>();
                return await Task.FromResult(treasures);
            }

            public Task<Coordinate> GetCoordinate(Guid Tid)
            {
                Coordinate coordinate = db.Coordinates
                .Where(c => c.Tid.Equals(Tid)).FirstOrDefault();
                return Task.FromResult(coordinate);
            }

            public Task<Treasure> GetTreasure(Guid Tid)
            {
                Treasure treasure = db.Treasures
                .Where(t => t.Id.Equals(Tid)).FirstOrDefault();
                return Task.FromResult(treasure);
            }

            public Task<bool> SetTreasure(Treasure T)
            {
                bool status = false;
                try
                {
                    db.Treasures.Add(T);
                    db.SaveChanges();
                    status = true;
                }
                catch (Exception e)
                {
                    status = false;
                    Console.WriteLine(e.Message);
                }
                return Task.FromResult(status);
            }
        }
}
