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
    public class OrderController : ControllerBase
    {
        /// <summary>
        /// Armazena os dados de acesso da collection 
        /// </summary>
        private readonly IMongoCollection<Order> _order;

        /// <summary>
        /// Construtor que recebe como dependencia o objeto da classe MongoDbService
        /// </summary>
        /// <param name="mongoDbService">Objeto da classe MongoDbService</param>
        public OrderController(MongoDbService mongoDbService)
        {
            // Obtém a collection "orders"
            _order = mongoDbService.GetDatabase.GetCollection<Order>("orders");
        }

        [HttpGet]
        public async Task<ActionResult<List<Order>>> Get()
        {
            try
            {
                var orders = await _order.Find(FilterDefinition<Order>.Empty).ToListAsync();
                return Ok(orders);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Create(Order order)
        {
            try
            {
                await _order.InsertOneAsync(order);
                return StatusCode(201, order);
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
                var order = await _order.Find(o => o.Id == id).FirstOrDefaultAsync();
                if (order == null)
                {
                    return NotFound("Pedido não encontrado!");
                }
                return Ok(order);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(string id, Order updatedOrder)
        {
            try
            {
                var filter = Builders<Order>.Filter.Eq(o => o.Id, id);
                var order = await _order.Find(filter).FirstOrDefaultAsync();
                if (order == null)
                {
                    return NotFound("Pedido não encontrado!");
                }

                // Atualize as propriedades do pedido com os novos valores
                order.Date = updatedOrder.Date;
                order.Status = updatedOrder.Status;
                order.Products = updatedOrder.Products;
                order.ClientId = updatedOrder.ClientId;

                // Atualize o pedido no banco de dados
                var result = await _order.ReplaceOneAsync(filter, order);

                if (result.IsAcknowledged && result.ModifiedCount > 0)
                {
                    return Ok(order);
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
                var filter = Builders<Order>.Filter.Eq(o => o.Id, id);
                var result = await _order.DeleteOneAsync(filter);

                if (result.DeletedCount > 0)
                {
                    return Ok("Pedido deletado com sucesso!");
                }
                else
                {
                    return NotFound("Pedido não encontrado!");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}

