using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace minimalAPIMongo.Domains
{
    public class Order
    {
        // Define que esta propriedade é o Id do objeto
        [BsonId]
        // Define o nome do campo no MongoDB como _id e do tipo ObjectId
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("date")]
        public DateTime Date { get; set; }

        [BsonElement("status")]
        public string? Status { get; set; }

        // Referência aos produtos pedidos
        [BsonElement("products")]
        public List<string> Products { get; set; }

        // Referência ao cliente que fez o pedido
        [BsonElement("clientId")]
        public string? ClientId { get; set; }

        /// <summary>
        /// Ao ser instanciado um objeto da classe Order,
        /// o atributo Products já virá com uma nova lista
        /// e portanto habilitado para adicionar mais produtos.
        /// </summary>
        public Order()
        {
            Products = new List<string>();
        }
    }
}
