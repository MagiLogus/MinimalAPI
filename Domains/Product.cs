using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace minimalAPIMongo.Domains
{
    public class Product
    {
        //Define que esta prop es Id do objeto
        [BsonId]
        //Define o nome do campo no mongodb como _id e do tipo objectId
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("name")]
        public string? Name { get; set; }

        [BsonElement("price")]
        public decimal? Price { get; set; }

        //Adiciona um dicionario para atributos adicionais
        public Dictionary<string, string> AdditionalAttributes { get; set; }

        /// <summary>
        /// Ao ser instanciado um obj da classe produto, o atributo AdditionalAtrtrinutes, 
        /// ja vira com um novo dicionario e portanto habilitado para adc mais atributos
        /// </summary>
        public Product()
        {
            AdditionalAttributes = new Dictionary<string, string>();
        }
    }
}
