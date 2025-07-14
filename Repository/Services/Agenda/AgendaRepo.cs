using Database.Data;
using DTO.Agenda;


namespace Repository.Services.Agenda
{
    public class AgendaRepo(AppDbContext _context)
    {

        public async Task<int> AddAsync(ReqAgendaDTO agenda)
        {
            var modello = _context.Agenda
            {

            };
            await _context.Agenda.AddAsync(modello);
            await _context.SaveChangesAsync();
            return modello.Id;
        }
    } 
}