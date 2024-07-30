using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace minimalAPIMongo.Domains
{
    public class User
    {
        //Define que esta prop es Id do objeto
        [BsonId]
        //Define o nome do campo no mongodb como _id e do tipo objectId
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("name")]
        public string? Name { get; set; }

        [BsonElement("email")]
        public string? Email { get; set; }

        [BsonElement("password")]
        public string? Password { get; set; }

        //Adiciona um dicionario para atributos adicionais
        public Dictionary<string, string> AdditionalAttributes { get; set; }

        /// <summary>
        /// Ao ser instanciado um obj da classe usuario, o atributo AdditionalAtrtrinutes, 
        /// ja vira com um novo dicionario e portanto habilitado para adc mais atributos
        /// </summary>
        public User()
        {
            AdditionalAttributes = new Dictionary<string, string>();
        }
    }
}
