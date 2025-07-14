using Database.Data;
using DTO.Agenda;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Services.Agenda
{
    public class AgendaRepo
    {
        private readonly AppDbContext _context;
        public AgendaRepo(AppDbContext cont)
        {
            _context = cont;
        }

        /*public async Task<int> AddAsync(ReqAgendaDTO agenda)
        {
            var modello = new Agenda()
            {
                Id =
    
        };
            await _context.Agenda.AddAsync(modello);
            await _context.SaveChangesAsync();
            return modello.Id;
        }*/
    } 
}