using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using minimalAPIMongo.Domains;
using minimalAPIMongo.Services;
using MongoDB.Driver;

namespace minimalAPIMongo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class UserController : ControllerBase
    {
        /// <summary>
        /// Armazena os dados de acesso da collection 
        /// </summary>
        private readonly IMongoCollection<User> _user;

        /// <summary>
        /// Construtor que recebe como dependencia o objeto da classe MongoDbService
        /// </summary>
        /// <param name="mongoDbService">Objeto da classe MongoDbService</param>
        public UserController(MongoDbService mongoDbService)
        {
            // Obtém a collection "users"
            _user = mongoDbService.GetDatabase.GetCollection<User>("users");
        }

        [HttpGet]
        public async Task<ActionResult<List<User>>> Get()
        {
            try
            {
                var users = await _user.Find(FilterDefinition<User>.Empty).ToListAsync();
                return Ok(users);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Create(User user)
        {
            try
            {
                await _user.InsertOneAsync(user);
                return StatusCode(201, user);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(string id)
        {
            try
            {
                var user = await _user.Find(u => u.Id == id).FirstOrDefaultAsync();
                if (user == null)
                {
                    return NotFound("Usuário não encontrado!");
                }
                return Ok(user);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(string id, User updatedUser)
        {
            try
            {
                var filter = Builders<User>.Filter.Eq(u => u.Id, id);
                var user = await _user.Find(filter).FirstOrDefaultAsync();
                if (user == null)
                {
                    return NotFound("Usuário não encontrado!");
                }

                // Atualize as propriedades do usuário com os novos valores
                user.Name = updatedUser.Name;
                user.Email = updatedUser.Email;
                user.Password = updatedUser.Password;
                user.AdditionalAttributes = updatedUser.AdditionalAttributes;

                // Atualize o usuário no banco de dados
                var result = await _user.ReplaceOneAsync(filter, user);

                if (result.IsAcknowledged && result.ModifiedCount > 0)
                {
                    return Ok(user);
                }
                else
                {
                    return BadRequest("Erro na atualização");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            try
            {
                var filter = Builders<User>.Filter.Eq(u => u.Id, id);
                var result = await _user.DeleteOneAsync(filter);

                if (result.DeletedCount > 0)
                {
                    return Ok("Usuário deletado com sucesso!");
                }
                else
                {
                    return NotFound("Usuário não encontrado!");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}

