using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace minimalAPIMongo.Domains
{
    public class Client
    {
        // Define que esta propriedade é o Id do objeto
        [BsonId]
        // Define o nome do campo no MongoDB como _id e do tipo ObjectId
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("userId")]
        public string? UserId { get; set; }

        [BsonElement("cpf")]
        public string? CPF { get; set; }

        [BsonElement("phone")]
        public string? Phone { get; set; }

        [BsonElement("address")]
        public string? Address { get; set; }

        // Adiciona um dicionário para atributos adicionais
        public Dictionary<string, string> AdditionalAttributes { get; set; }

        /// <summary>
        /// Ao ser instanciado um objeto da classe Client,
        /// o atributo AdditionalAttributes já virá com um novo dicionário
        /// e portanto habilitado para adicionar mais atributos.
        /// </summary>
        public Client()
        {
            AdditionalAttributes = new Dictionary<string, string>();
        }
    }
}
