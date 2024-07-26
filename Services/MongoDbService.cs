using MongoDB.Driver;

namespace minimalAPIMongo.Services
{
    public class MongoDbService
    {
        /// <summary>
        /// Armazena config da aplicacao
        /// </summary>
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Armazena uma ref ao MongoDb
        /// </summary>
        private readonly IMongoDatabase _database;

        /// <summary>
        /// Recebe a config da aplicacao como parametro 
        /// </summary>
        /// <param name="configuration">objeto configuration</param>
        public MongoDbService(IConfiguration configuration)
        {
            //Atribui a config  recebida em _configuration 
            _configuration = configuration;

            //Obtem a string de conexao atraves do _configuration
            var connectionString = _configuration.GetConnectionString("DbConnection");

            //Cria um obj MongoUrl que recebe como parametro a string de conexao
            var mongoUrl = MongoUrl.Create(connectionString);

            //Cria um client MongoClient para se conectar ao MongoDb
            var mongoClient = new MongoClient(mongoUrl);

            //Obtem a ref ao bd com o nome especificado na string de conexao
            _database = mongoClient.GetDatabase(mongoUrl.DatabaseName);
        }

        /// <summary>
        /// Propriedade para acessar o banco de dados 
        /// Retorna a referencia ao bd
        /// </summary>
        public IMongoDatabase GetDatabase => _database;
    }
}
