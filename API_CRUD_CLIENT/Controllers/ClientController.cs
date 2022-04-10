using APICrudClient.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_CRUD_CLIENT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly ApplicationdbContext _context;
        public ClientController(ApplicationdbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client>>> ListCLient()
        {
            try 
            { 
                var data = await _context.Client.Include(x => x.ClientAddress).ToListAsync();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Client>> GetClientById(int id)
        {
            try
            {
                var data = await _context.Client.
                                   Include(x => x.ClientAddress)
                                  .Where(x => x.Id == id)
                                  .FirstAsync();

                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        public async Task<ActionResult<Client>> PostData(Client dataModel)
        {
            try
            {
                _context.Client.Add(dataModel);

                foreach (var item in dataModel.ClientAddress)
                {
                    item.ClientId = dataModel.Id;
                    _context.ClientAddress.Add(item);
                }

                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(dataModel);
        }

        [HttpPut]
        public async Task<ActionResult<Client>> UpdateCompleteData(Client dataModel)
        {
            try
            {

                _context.Entry(dataModel).State = EntityState.Modified;

                foreach (var item in dataModel.ClientAddress)
                {
                    if(item.Id == 0)
                    {
                        item.ClientId = dataModel.Id;
                        _context.ClientAddress.Add(item);
                    }else
                    {
                        _context.Entry(item).State = EntityState.Modified;
                    }
                }

                //when user delete some address
                var listClient = dataModel.ClientAddress.Select(x => x.Id).ToList();
                var ListClientForDelete = await _context
                                                .ClientAddress
                                                .Where(x => !listClient.Contains(x.Id) && x.ClientId == dataModel.Id).ToListAsync();

                //then we delete
                _context.RemoveRange(ListClientForDelete);

                await _context.SaveChangesAsync();

                return Ok(dataModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Client>> DeleteData(int id)
        {
            try
            {
                var Client = await _context.Client.FindAsync(id);

                if (Client == null)
                {
                    return BadRequest("Client no encontrada");
                }

                _context.Client.Remove(Client);
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
